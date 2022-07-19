#nullable enable

namespace TeamZero.InAppPurchases
{
    public interface IStorePurchase : IPurchase
    {
        void ChangeStatus();
    }
}
