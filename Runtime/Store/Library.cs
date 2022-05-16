using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Purchasing;

#nullable enable

namespace TeamZero.InAppPurchases
{
    internal interface IPurchaseFactory<out T> where T : IPurchase
    {
        T Create(string id, IPurchaseHub hub);
    }
    
    internal class Library<T> where T : IPurchase
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

    internal class RestoredLibrary<T> : Library<T> where T : IPurchase, IRestorable
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
