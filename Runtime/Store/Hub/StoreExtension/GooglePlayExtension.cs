#nullable enable
using System;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class GooglePlayExtension : PlatformExtension
    {
        private IGooglePlayStoreExtensions _googlePlayExtensions;
        
        internal GooglePlayExtension(IExtensionProvider provider, Log log) : base(provider, log)
        {
            _googlePlayExtensions = provider.GetExtension<IGooglePlayStoreExtensions>();
        }

        protected override void RestoreTransactions(Action<bool> result) =>
            _googlePlayExtensions.RestoreTransactions(result);
    }
}
