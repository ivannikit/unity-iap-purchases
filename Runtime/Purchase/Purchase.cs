#nullable enable
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
    }
}
