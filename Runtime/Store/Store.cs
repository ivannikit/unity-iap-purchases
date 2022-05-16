#nullable enable
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;

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
            var hub = UnityIAP.UnityIAPHub.Create(log);
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
        public ConsumablePurchase RegisterNewConsumable(string id) => _consumableLibrary.Register(id);


        NonConsumablePurchase IPurchaseFactory<NonConsumablePurchase>.Create(string id, IPurchaseHub hub) => new (id, hub, _log);
        public NonConsumablePurchase RegisterNewNonConsumable(string id) => _nonConsumableLibrary.Register(id);

        ISubscription IPurchaseFactory<ISubscription>.Create(string id, IPurchaseHub hub) => throw new System.NotImplementedException();
        public ISubscription RegisterNewSubscription(string id) => throw new System.NotImplementedException();

        public async UniTask RestoreAll()
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
