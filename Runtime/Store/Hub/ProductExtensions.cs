using UnityEngine.Purchasing;

#nullable enable

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal static class ProductExtensions
    {
        internal static PurchaseMetadata ToPurchaseMetadata(this Product product)
        {
            ProductMetadata metadata = product.metadata;
            string localizedPriceText = metadata.localizedPriceString;
            return new PurchaseMetadata(localizedPriceText);
        }
    }
}
