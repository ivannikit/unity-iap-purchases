#nullable enable
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal static class ExtensionFactory
    {
        internal static IStoreExtension CreateDefault(IExtensionProvider provider, Log log)
        {
#if UNITY_EDITOR
            return new NotSupportedExtension(log);
#elif UNITY_ANDROID
            return new GooglePlayExtension(provider, log);
#elif UNITY_IOS
            return new AppleExtension(provider, log);
#else
            return new NotSupportedExtension(log);
#endif
        }
    }
}
