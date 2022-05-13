#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public interface IPurchase
    {
        UniTask<bool> ConsumeAsync();
    }
}
