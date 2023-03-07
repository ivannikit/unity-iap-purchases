#nullable enable
using System;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class GooglePlayExtension : PlatformExtension
    {
        private readonly IGooglePlayStoreExtensions _googlePlayExtensions;
        
        internal GooglePlayExtension(IExtensionProvider provider, Log log) : base(provider, log)
        {
            _googlePlayExtensions = provider.GetExtension<IGooglePlayStoreExtensions>();
        }

        protected override void RestoreTransactions(Action<bool> result)
        {
            void Callback(bool success, string? error) => result.Invoke(success);
            _googlePlayExtensions.RestoreTransactions((Action<bool, string?>) Callback);
        }
    }
}
