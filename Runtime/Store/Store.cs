#nullable enable

namespace TeamZero.InAppPurchases
{
    public class Store : IPurchaseFactory<ConsumablePurchase>, IPurchaseFactory<NonConsumablePurchase>, IPurchaseFactory<ISubscription>
    {
        private readonly Library<ConsumablePurchase> _consumableLibrary;
        private readonly Library<NonConsumablePurchase> _nonConsumableLibrary;
        private readonly Library<ISubscription> _subscriptionLibrary;

        public static Store Create() => new ();
        
        private Store()
        {
            _consumableLibrary = Library<ConsumablePurchase>.Create(this);
            _nonConsumableLibrary = Library<NonConsumablePurchase>.Create(this);
            _subscriptionLibrary = Library<ISubscription>.Create(this);
        }
        
        ConsumablePurchase IPurchaseFactory<ConsumablePurchase>.Create(string id) => new (id);
        public ConsumablePurchase RegisterNewConsumable(string id) => _consumableLibrary.Register(id);


        NonConsumablePurchase IPurchaseFactory<NonConsumablePurchase>.Create(string id) => new (id);
        public NonConsumablePurchase RegisterNewNonConsumable(string id) => _nonConsumableLibrary.Register(id);

        ISubscription IPurchaseFactory<ISubscription>.Create(string id) => throw new System.NotImplementedException();
        public ISubscription RegisterNewSubscription(string id) => throw new System.NotImplementedException();

        public void RestoreAll()
        {
        }
    }
}
