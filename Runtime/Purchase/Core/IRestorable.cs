#nullable enable
using System;

namespace TeamZero.InAppPurchases
{
    public interface IRestorable
    {
        event Action Restored;
        
#if PACKAGE_COM_NEUECC_UNIRX
        IObservable<UniRx.Unit> RestoredAsObservable();
#endif
        void RestoreComplete();
    }
}
