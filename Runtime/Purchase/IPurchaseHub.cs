#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal interface IPurchaseHub
    {
        UniTask<bool> PurchaseAsync(string id);
    }
}
