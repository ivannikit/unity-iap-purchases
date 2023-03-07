#nullable enable
using TeamZero.Core.Logging;

namespace TeamZero.InAppPurchases.UnityIAP
{
    internal class NotSupportedValidator : IPurchaseValidator
    {
        private readonly Log _log;
        
        internal NotSupportedValidator(Log log)
        {
            _log = log;
        }

        public bool IsPurchaseValid(string receipt)
        {
            _log.Info("This platform isn`t supported purchase validation (always true)");
            return true;
        }
    }
}
