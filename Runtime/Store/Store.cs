#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;
using TeamZero.InAppPurchases.UnityIAP;

namespace TeamZero.InAppPurchases
{
    public class Store : IPurchaseFactory<ConsumablePurchase>, IPurchaseFactory<NonConsumablePurchase>, IPurchaseFactory<ISubscription>
    {
        private readonly Library<ConsumablePurchase> _consumableLibrary;
        private readonly RestoredLibrary<NonConsumablePurchase> _nonConsumableLibrary;
        private readonly RestoredLibrary<ISubscription> _subscriptionLibrary;

        private readonly IStoreHub _hub;
        private readonly Log _log;

        public static Store Create(Log log)
        {
            // ReSharper disable once JoinDeclarationAndInitializer
            IPlatformHub hub;
#if UNITY_EDITOR
            hub = NotSupportedPlatformHub.Create(log);
#elif UNITY_ANDROID || UNITY_IOS
            hub = UnityIAP.UnityHub.Create(log);
#else
            hub = NotSupportedPlatformHub.Create(log);
#endif
            return new Store(hub, hub, log);
        }

        private Store(IStoreHub storeHub, IPurchaseHub purchaseHub, Log log)
        {
            _consumableLibrary = Library<ConsumablePurchase>.Create(this, purchaseHub);
            _nonConsumableLibrary = RestoredLibrary<NonConsumablePurchase>.Create(this, purchaseHub);
            _subscriptionLibrary = RestoredLibrary<ISubscription>.Create(this, purchaseHub);
            _hub = storeHub;
            _log = log;
        }
        
        ConsumablePurchase IPurchaseFactory<ConsumablePurchase>.Create(string id, IPurchaseHub hub) => new (id, hub, _log);
        public IPurchase RegisterNewConsumable(string id) => _consumableLibrary.Register(id);
        
        NonConsumablePurchase IPurchaseFactory<NonConsumablePurchase>.Create(string id, IPurchaseHub hub) => new (id, hub, _log);
        public IRestorablePurchase RegisterNewNonConsumable(string id) => _nonConsumableLibrary.Register(id);

        ISubscription IPurchaseFactory<ISubscription>.Create(string id, IPurchaseHub hub) => throw new System.NotImplementedException();
        public ISubscription RegisterNewSubscription(string id) => throw new System.NotImplementedException();

        public async UniTask InitAsync()
        {
            IEnumerable<string> consumableIds = _consumableLibrary.Ids();
            IEnumerable<string > nonConsumableIds = _nonConsumableLibrary.Ids();
            IEnumerable<string> subscriptionIds = _subscriptionLibrary.Ids();
            await _hub.InitAsync(consumableIds, nonConsumableIds, subscriptionIds);
            
            _consumableLibrary.ChangeStatus();
            _nonConsumableLibrary.ChangeStatus();
            _subscriptionLibrary.ChangeStatus();
        }
        
        public async UniTask RestoreAllAsync()
        {
            bool succeeded = await _hub.RestoreAllAsync();
            if (succeeded)
            {
                _nonConsumableLibrary.RestorePurchasesComplete();
                _subscriptionLibrary.RestorePurchasesComplete();
            }
        }
    }
}
