#nullable enable
using System.Collections.Generic;

namespace TeamZero.InAppPurchases
{
    internal class RestoredLibrary<T> : Library<T> where T : IStorePurchase, IRestorable
    {
        internal new static RestoredLibrary<T> Create(IPurchaseFactory<T> factory, IPurchaseHub hub, int capacity = 0)
        {
            Dictionary<string, T> items = new (capacity);
            return new RestoredLibrary<T>(factory, hub, items);
        }
        
        protected RestoredLibrary(IPurchaseFactory<T> factory, IPurchaseHub hub, Dictionary<string, T> items) 
            : base(factory, hub, items)
        {
        }
        
        public void RestorePurchasesComplete()
        {
            foreach (T purchase in _items.Values)
                purchase.RestoreComplete();
        }
    }
}
