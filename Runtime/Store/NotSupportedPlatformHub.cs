#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TeamZero.Core.Logging;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases.UnityIAP
{
    public class NotSupportedPlatformHub : IPlatformHub
    {
        private readonly Log _log;
        private IStoreController? _store;
        private IPurchaseValidator? _validator;
        private IStoreExtension? _extension;
        
        public static NotSupportedPlatformHub Create(Log log) => new (log);

        private NotSupportedPlatformHub(Log log)
        { 
            _log = log;
            _log.Error("This platform isn`t supported purchases");
        }

#pragma warning disable 1998
        public async UniTask InitAsync(IEnumerable<string>? consumableIds, IEnumerable<string>? nonConsumableIds, IEnumerable<string>? subscriptionIds)
#pragma warning restore 1998
        {
        }
        
#pragma warning disable 1998
        public async UniTask<bool> RestoreAllAsync() => false;
#pragma warning restore 1998
        
#pragma warning disable 1998
        public async UniTask<bool> PurchaseAsync(string id) => false;
#pragma warning restore 1998
        
        
        bool IPurchaseHub.Initialized() => false;
        
        private Product? ProductWithId(string id) => null;
        
        PurchaseMetadata? IPurchaseHub.PurchaseMetadata(string id) => null;
        
        bool IPurchaseHub.IsAvailableToPurchase(string id) => false;
        
        bool IPurchaseHub.IsConsumed(string id) => false;
    }
}
