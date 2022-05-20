#nullable enable

#if PACKAGE_COM_NEUECC_UNIRX
using UniRx;
#endif

using System;
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;

namespace TeamZero.InAppPurchases
{
    public abstract class Purchase : IPurchase
    {
        private readonly string _id;
        private readonly IPurchaseHub _hub;
        private readonly Log _log;

        internal Purchase(string id, IPurchaseHub hub, Log log)
        {
            _id = id;
            _hub = hub;
            _log = log;
        }

        public async UniTask<bool> ConsumeAsync() => await _hub.PurchaseAsync(_id);
        public event Action? StatusChanged;


        private PurchaseMetadata? _meta;
        private PurchaseMetadata? Meta
        {
            get
            {
                if (!_meta.HasValue && _hub.Initialized())
                    _meta = _hub.PurchaseMetadata(_id);

                return _meta;
            }
        }

#if PACKAGE_COM_NEUECC_UNIRX
        private readonly Subject<Unit> _statusSubject = new();
        public IObservable<Unit> StatusAsObservable() => _statusSubject;
#endif

        public void ChangeStatus()
        {
            StatusChanged?.Invoke();
#if PACKAGE_COM_NEUECC_UNIRX
            _statusSubject.OnNext(Unit.Default);
#endif
        }

        public string LocalizedPriceText() => Meta?.LocalizedPriceText ?? String.Empty;

        public bool IsConsumed() => _hub.IsConsumed(_id);
        
        public bool IsAvailableToPurchase() => _hub.IsAvailableToPurchase(_id);
    }
}
