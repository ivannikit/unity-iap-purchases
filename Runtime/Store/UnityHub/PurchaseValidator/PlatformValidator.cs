#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
#nullable enable
using TeamZero.Core.Logging;
using UnityEngine;
using UnityEngine.Purchasing.Security;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class PlatformValidator : IPurchaseValidator
    {
        private readonly Log _log;
        private readonly CrossPlatformValidator _validator;

        internal PlatformValidator(Log log)
        {
            _log = log;
            _validator = new CrossPlatformValidator(GooglePlayTangle.Data(), 
                AppleTangle.Data(), Application.identifier);
        }

        public bool IsPurchaseValid(string receipt)
        {
            try
            {
                _validator.Validate(receipt);
            }
            //If the purchase is deemed invalid, the validator throws an IAPSecurityException.
            catch (IAPSecurityException reason)
            {
                _log.Error($"Invalid receipt: {reason}");
                return false;
            }

            return true;
        }
    }
}
#endif
