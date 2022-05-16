#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    public class UnityHub : IStoreHub, IPurchaseHub, IStoreListener
    {
        private readonly Log _log;
        private IStoreController? _store;
        private IPurchaseValidator? _validator;
        private IStoreExtension? _extension;
        
        public static UnityHub Create(Log log) => new (log);

        private UnityHub(Log log)
        {
            _log = log;
        }

        private UniTaskCompletionSource? _initSource;
        public async UniTask InitAsync(IEnumerable<string>? consumableIds, IEnumerable<string>? nonConsumableIds, IEnumerable<string>? subscriptionIds)
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            foreach (string id in FilterMissing(consumableIds))
                builder.AddProduct(id, ProductType.NonConsumable);

            foreach (string id in FilterMissing(nonConsumableIds))
                builder.AddProduct(id, ProductType.NonConsumable);
            
            foreach (string id in FilterMissing(subscriptionIds))
                builder.AddProduct(id, ProductType.Subscription);

            _initSource = new UniTaskCompletionSource();
            UnityPurchasing.Initialize(this, builder);
            await _initSource.Task;
        }

        private IEnumerable<string> FilterMissing(IEnumerable<string>? ids)
        {
            if(ids != null)
                foreach (string id in ids)
                {
                    if (string.IsNullOrEmpty(id))
                    {
                        _log.Error("Id is null or empty");
                        continue;
                    }

                    yield return id;
                }
        }

        private bool AssertInitialized()
        {
            if (_store == null)
            {
                _log.Error($"In-App Purchasing isn't initialized");
                return false;
            }

            return true;
        }
        
        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _log.Info("In-App Purchasing successfully initialized");
            _store = controller;
            _validator = ValidatorFactory.CreateDefault(_log);
            _extension = ExtensionFactory.CreateDefault(extensions, _log);
            
            _initSource?.TrySetResult();
            _initSource = null;
        }
        
        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            _log.Error($"In-App Purchasing initialize failed: {error}");
            
            _initSource?.TrySetResult();
            _initSource = null;
        }

        
        public async UniTask<bool> RestoreAllAsync()
        {
            if (AssertInitialized() && _extension != null)
                return await _extension.RestoreTransactions();

            return false;
        }

        
        private readonly Dictionary<string, UniTaskCompletionSource<bool>> _purchaseTaskCollection = new ();
        public async UniTask<bool> PurchaseAsync(string id)
        {
            if (AssertInitialized() && _store != null)
            {
                if (_purchaseTaskCollection.ContainsKey(id))
                {
                    _log.Error($"Purchase already in process - Product: {id}");
                    return false;
                }
                
                _log.Info($"Starting purchase - Product: {id}");
                var source = new UniTaskCompletionSource<bool>();
                _purchaseTaskCollection.Add(id, source);
                _store.InitiatePurchase(id);

                return await source.Task;
            }

            return false;
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs args)
        {
            var product = args.purchasedProduct;
            string id = product.definition.id;

            bool isPurchaseValid = _validator?.IsPurchaseValid(product.receipt) ?? false;
            SendPurchaseResult(id, isPurchaseValid);

            return PurchaseProcessingResult.Complete;
        }

        void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            string id = product.definition.id;
            SendPurchaseResult(id, false);
        }

        private void SendPurchaseResult(string id, bool completed)
        {
            if (_purchaseTaskCollection.TryGetValue(id, out UniTaskCompletionSource<bool> source))
            {
                if (_log.InfoEnabled())
                {
                    string result = completed ? "Completed" : "Failed";
                    _log.Info($"Purchase result: {result} Product: {id}");
                }
                
                _purchaseTaskCollection.Remove(id);
                source.TrySetResult(completed);
            }
            else
            {
                _log.Error($"Process purchase not found - Product: {id}");
            }
        }
        
        
        bool IPurchaseHub.Initialized() => _store != null;
        private Product? ProductWithId(string id) => _store?.products.WithID(id);
        PurchaseMetadata? IPurchaseHub.PurchaseMetadata(string id) => ProductWithId(id)?.ToPurchaseMetadata();
        bool IPurchaseHub.IsAvailableToPurchase(string id) => ProductWithId(id)?.availableToPurchase ?? false;
        bool IPurchaseHub.IsConsumed(string id) => ProductWithId(id)?.hasReceipt ?? false;
    }
}
