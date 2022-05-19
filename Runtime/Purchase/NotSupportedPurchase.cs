#nullable enable
using System;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal class NotSupportedPurchase : IPurchase
    {
        internal static NotSupportedPurchase Create(string price) => new(price);
        internal static NotSupportedPurchase Create() => new(string.Empty);
        
        private readonly string _price;
        
        private NotSupportedPurchase(string price)
        {
            _price = price;
        }

        public string LocalizedPriceText() => _price;

        public bool IsConsumed() => false;
        
        public bool IsAvailableToPurchase() => false;
        
#pragma warning disable 1998
        public async UniTask<bool> ConsumeAsync() => false;
        public event Action? StatusChanged;
#pragma warning restore 1998

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
