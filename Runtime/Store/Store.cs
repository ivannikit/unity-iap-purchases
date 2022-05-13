#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public class Store : IPurchaseFactory<ConsumablePurchase>, IPurchaseFactory<NonConsumablePurchase>, IPurchaseFactory<ISubscription>
    {
        private readonly Library<ConsumablePurchase> _consumableLibrary;
        private readonly Library<NonConsumablePurchase> _nonConsumableLibrary;
        private readonly Library<ISubscription> _subscriptionLibrary;

        private readonly IStoreHub _hub;

        public static Store Create()
        {
            UnityIAPHub hub = UnityIAPHub.Create();
            return new Store(hub, hub);
        }

        private Store(IStoreHub storeHub, IPurchaseHub purchaseHub)
        {
            _consumableLibrary = Library<ConsumablePurchase>.Create(this, purchaseHub);
            _nonConsumableLibrary = Library<NonConsumablePurchase>.Create(this, purchaseHub);
            _subscriptionLibrary = Library<ISubscription>.Create(this, purchaseHub);
            _hub = storeHub;
        }
        
        ConsumablePurchase IPurchaseFactory<ConsumablePurchase>.Create(string id, IPurchaseHub hub) => new (id, hub);
        public ConsumablePurchase RegisterNewConsumable(string id) => _consumableLibrary.Register(id);


        NonConsumablePurchase IPurchaseFactory<NonConsumablePurchase>.Create(string id, IPurchaseHub hub) => new (id, hub);
        public NonConsumablePurchase RegisterNewNonConsumable(string id) => _nonConsumableLibrary.Register(id);

        ISubscription IPurchaseFactory<ISubscription>.Create(string id, IPurchaseHub hub) => throw new System.NotImplementedException();
        public ISubscription RegisterNewSubscription(string id) => throw new System.NotImplementedException();

        public async UniTask RestoreAll()
        {
            bool succeeded = await _hub.RestoreAllAsync();
            if (succeeded)
            {
                //TODO restore product events
            }
        }
    }
}
