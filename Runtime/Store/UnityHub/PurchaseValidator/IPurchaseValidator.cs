#nullable enable

namespace TeamZero.InAppPurchases.UnityIAP
{
    public interface IPurchaseValidator
    {
        public bool IsPurchaseValid(string receipt);
    }
}
