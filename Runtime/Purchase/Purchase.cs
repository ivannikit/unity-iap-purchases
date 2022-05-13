#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public abstract class Purchase : IPurchase
    {
        private readonly string _id;
        private readonly IPurchaseHub _hub;

        internal Purchase(string id, IPurchaseHub hub)
        {
            _id = id;
            _hub = hub;
        }

        public async UniTask<bool> ConsumeAsync() => await _hub.PurchaseAsync(_id);
    }
}
