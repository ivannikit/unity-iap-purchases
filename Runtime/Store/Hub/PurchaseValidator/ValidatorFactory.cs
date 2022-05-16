#nullable enable
using TeamZero.Core.Logging;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal static class ValidatorFactory
    {
        internal static IPurchaseValidator CreateDefault(Log log)
        {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
            return new PlatformValidator(log);
#else
            return new NotSupportedValidator(log);
#endif
        }
    }
}
