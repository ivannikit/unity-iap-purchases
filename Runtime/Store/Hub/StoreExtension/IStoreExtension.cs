#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal interface IStoreExtension
    {
        UniTask<bool> RestoreTransactions();
    }
}
