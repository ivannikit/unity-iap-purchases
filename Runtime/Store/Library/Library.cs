#nullable enable
using System;
using System.Collections.Generic;

namespace TeamZero.InAppPurchases
{
    internal class Library<T> where T : IStorePurchase
    {
        protected readonly Dictionary<string, T> _items;
        private readonly IPurchaseFactory<T> _factory;
        private readonly IPurchaseHub _hub;

        internal static Library<T> Create(IPurchaseFactory<T> factory, IPurchaseHub hub, int capacity = 0)
        {
            Dictionary<string, T> items = new (capacity);
            return new Library<T>(factory, hub, items);
        }

        protected Library(IPurchaseFactory<T> factory, IPurchaseHub hub, Dictionary<string, T> items)
        {
            _factory = factory;
            _items = items;
            _hub = hub;
        }

        internal T Register(string id)
        {
            if (_items.ContainsKey(id))
                throw new ArgumentException(id);
                
            T instance = _factory.Create(id, _hub);
            _items.Add(id, instance);
            
            return instance;
        }
        
        public void ChangeStatus()
        {
            foreach (T purchase in _items.Values)
                purchase.ChangeStatus();
        }

        public ICollection<string> Ids() => _items.Keys;
    }
}
