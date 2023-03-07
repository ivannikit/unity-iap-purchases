#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal interface IPurchaseHub
    {
        UniTask<bool> PurchaseAsync(string id);
        bool Initialized();
        PurchaseMetadata? PurchaseMetadata(string id);
        bool IsAvailableToPurchase(string id);
        bool IsConsumed(string id);
    }
}
