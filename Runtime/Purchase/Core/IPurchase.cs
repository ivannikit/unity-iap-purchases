#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public interface IPurchase
    {
        UniTask<bool> ConsumeAsync();
        
        
        event Action StatusChanged;
        
#if PACKAGE_COM_NEUECC_UNIRX
        IObservable<UniRx.Unit> StatusAsObservable();
#endif
        
        string LocalizedPriceText();
        bool IsAvailableToPurchase();
        bool IsConsumed();
    }
}
