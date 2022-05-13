#nullable enable

namespace TeamZero.InAppPurchases
{
    public class ConsumablePurchase : Purchase
    {
        internal ConsumablePurchase(string id, IPurchaseHub hub) : base(id, hub)
        {
        }
    }
}
