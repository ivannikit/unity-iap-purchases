#nullable enable

namespace TeamZero.InAppPurchases
{
    public interface IStore
    {
        ConsumablePurchase RegisterNewConsumable(string id);
        NonConsumablePurchase RegisterNewNonConsumable(string id);
        ISubscription RegisterNewSubscription(string id);

        void RestoreAll();
    }
}
