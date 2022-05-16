#nullable enable
#if PACKAGE_COM_NEUECC_UNIRX
using UniRx;
#endif
using System;
using TeamZero.Core.Logging;

namespace TeamZero.InAppPurchases
{
    public class NonConsumablePurchase : Purchase, IRestorable
    {
        internal NonConsumablePurchase(string id, IPurchaseHub hub, Log log) : base(id, hub, log)
        {
        }
        
        public event Action? Restored;
        
#if PACKAGE_COM_NEUECC_UNIRX
        private readonly Subject<Unit> _restoredSubject = new();
        public IObservable<Unit> RestoredAsObservable() => _restoredSubject;
#endif
        
        public void RestoreComplete()
        {
            ChangeStatus();
            Restored?.Invoke();
            _restoredSubject.OnNext(Unit.Default);
        }
    }
}
