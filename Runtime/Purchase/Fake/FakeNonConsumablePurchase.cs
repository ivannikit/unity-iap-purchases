#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal class FakeNonConsumablePurchase : IRestorablePurchase
    {
        public static FakeNonConsumablePurchase Create() => Create("Fake 9.99 $");
        public static FakeNonConsumablePurchase Create(string price) => Create(price, TimeSpan.FromSeconds(5));
        public static FakeNonConsumablePurchase Create(string price, TimeSpan responseDelay) => new(price, responseDelay);
        
        private readonly string _price;
        private readonly TimeSpan _responseDelay;
        private bool _initialized = false;
        private bool _consumed = false;
        
        private FakeNonConsumablePurchase(string price, TimeSpan responseDelay)
        {
            _price = price;
            _responseDelay = responseDelay;
            InitAsync().Forget();
        }

        private async UniTaskVoid InitAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(5), DelayType.Realtime);
            _initialized = true;
            ChangeStatus();
        }
        
        public string LocalizedPriceText() => _initialized ? _price : string.Empty;

        public bool IsConsumed() => _consumed;
        
        public bool IsAvailableToPurchase() => _initialized && !_consumed;
        
        public async UniTask<bool> ConsumeAsync()
        {
            if (IsAvailableToPurchase())
            {
                await UniTask.Delay(TimeSpan.FromSeconds(5), DelayType.Realtime);
                
                _consumed = true;
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

        private void ChangeStatus()
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
