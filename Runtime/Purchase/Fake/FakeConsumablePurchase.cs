#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal class FakeConsumablePurchase : IPurchase
    {
        public static FakeConsumablePurchase Create(string price) => new(price);
        
        private readonly string _price;
        private bool _initialized = false;
        private bool _consumed = false;
        
        private FakeConsumablePurchase(string price)
        {
            _price = price;
            
#pragma warning disable 4014
            InitAsync();
#pragma warning restore 4014
        }

        private async UniTask InitAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(10), DelayType.Realtime);
            _initialized = true;
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

        public void ChangeStatus()
        {
            StatusChanged?.Invoke();
#if PACKAGE_COM_NEUECC_UNIRX
            _statusSubject.OnNext(UniRx.Unit.Default);
#endif
        }
    }
}
