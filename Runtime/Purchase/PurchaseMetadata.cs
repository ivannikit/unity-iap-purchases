#nullable enable

namespace TeamZero.InAppPurchases
{
    internal readonly struct PurchaseMetadata
    {
        public readonly string LocalizedPriceText;
        
        internal PurchaseMetadata(string localizedPriceText)
        {
            LocalizedPriceText = localizedPriceText;
        }
    }
}
