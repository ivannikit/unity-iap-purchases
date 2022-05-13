using TeamZero.Core.Logging;

#nullable enable

namespace TeamZero.InAppPurchases
{
    public class ConsumablePurchase : Purchase
    {
        internal ConsumablePurchase(string id, IPurchaseHub hub, Log log) : base(id, hub, log)
        {
        }
    }
}
