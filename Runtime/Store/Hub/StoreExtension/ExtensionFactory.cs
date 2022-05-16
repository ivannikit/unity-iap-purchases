#nullable enable
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal static class ExtensionFactory
    {
        internal static IStoreExtension CreateDefault(IExtensionProvider provider, Log log)
        {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            return new PlatformExtension(provider, log);
#else
            return new NotSupportedExtension(log);
#endif
        }
    }
}
