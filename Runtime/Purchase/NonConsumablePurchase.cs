#nullable enable
#if PACKAGE_COM_NEUECC_UNIRX
using UniRx;
#endif
using System;

namespace TeamZero.InAppPurchases
{
    public class NonConsumablePurchase : Purchase, IRestorable
    {
        public event Action? Restored;
        
#if PACKAGE_COM_NEUECC_UNIRX
        private readonly Subject<Unit> _restoredSubject = new();
        public IObservable<Unit> RestoredAsObservable() => _restoredSubject;
#endif
    }
}
