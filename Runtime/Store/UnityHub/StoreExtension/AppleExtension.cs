#nullable enable
using System;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class AppleExtension : PlatformExtension
    {
        private readonly IAppleExtensions _appleExtensions;
        
        internal AppleExtension(IExtensionProvider provider, Log log) : base(provider, log)
        {
            _appleExtensions = provider.GetExtension<IAppleExtensions>();
        }
        
        protected override void RestoreTransactions(Action<bool> result)
        {
            void Callback(bool success, string? error) => result.Invoke(success);
            _appleExtensions.RestoreTransactions(Callback);
        }
    }
}
