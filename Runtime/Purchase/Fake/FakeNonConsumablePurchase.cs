#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal class FakeNonConsumablePurchase : IRestorablePurchase
    {
        public static FakeNonConsumablePurchase Create(string price) => new(price);

        private readonly string _price;
        private bool _initialized = false;
        
        private FakeNonConsumablePurchase(string price)
        {
            _price = price;
            InitAsync().Forget();
        }

        private async UniTaskVoid InitAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5), DelayType.Realtime);
            _initialized = true;
            ChangeStatus();
        }
        
        public string LocalizedPriceText() => _initialized ? _price : string.Empty;

        public bool IsConsumed() => false;
        
        public bool IsAvailableToPurchase() => _initialized;
        
        public async UniTask<bool> ConsumeAsync()
        {
            if (IsAvailableToPurchase())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(5), DelayType.Realtime);

                ChangeStatus();
                return true;
            }

            return false;
        }


        public event Action? StatusChanged;
        

#if PACKAGE_COM_NEUECC_UNIRX
        private readonly UniRx.Subject<UniRx.Unit> _statusSubject = new ();
        public IObservable<UniRx.Unit> StatusAsObservable() => _statusSubject;
#endif

        public void ChangeStatus()
        {
            StatusChanged?.Invoke();
#if PACKAGE_COM_NEUECC_UNIRX
            _statusSubject.OnNext(UniRx.Unit.Default);
#endif
        }

        
        public event Action? Restored;
        
#if PACKAGE_COM_NEUECC_UNIRX
        private readonly UniRx.Subject<UniRx.Unit> _restoredSubject = new();
        public IObservable<UniRx.Unit> RestoredAsObservable() => _restoredSubject;
#endif
        
        public void RestoreComplete()
        {
            ChangeStatus();
            Restored?.Invoke();
#if PACKAGE_COM_NEUECC_UNIRX
            _restoredSubject.OnNext(UniRx.Unit.Default);
#endif
        }
    }
}
