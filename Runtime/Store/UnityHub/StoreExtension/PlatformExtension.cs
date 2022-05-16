#nullable enable
using System;
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal abstract class PlatformExtension : IStoreExtension
    {
        private readonly Log _log;
        private readonly IExtensionProvider _provider;

        internal PlatformExtension(IExtensionProvider provider, Log log)
        {
            _provider = provider;
            _log = log;
        }

        private UniTaskCompletionSource<bool>? _restoringSource;
        async UniTask<bool> IStoreExtension.RestoreTransactions()
        {
            if (_restoringSource != null)
            {
                _log.Warning("The restoring of purchases is already in progress");
                return false;
            }
            
            _restoringSource = new UniTaskCompletionSource<bool>();
            RestoreTransactions(succeeded =>
            {
                _log.Warning($"The restoring of purchases result: {succeeded}");
                _restoringSource.TrySetResult(succeeded);
                _restoringSource = null;
            });
            
            return await _restoringSource.Task;
        }

        protected abstract void RestoreTransactions(Action<bool> result);
    }
}
