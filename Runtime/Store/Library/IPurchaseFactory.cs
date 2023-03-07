#nullable enable

namespace TeamZero.InAppPurchases
{
    internal interface IPurchaseFactory<out T> where T : IPurchase
    {
        T Create(string id, IPurchaseHub hub);
    }
}
