#nullable enable
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class NotSupportedExtension : IStoreExtension
    {
        private readonly Log _log;
        
        internal NotSupportedExtension(Log log)
        {
            _log = log;
        }

#pragma warning disable 1998
        public async UniTask<bool> RestoreTransactions()
#pragma warning restore 1998
        {
            _log.Info("The restoring of purchases isn't supported");
            return false;
        }
    }
}
