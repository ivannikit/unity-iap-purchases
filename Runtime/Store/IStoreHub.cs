#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal interface IStoreHub
    {
        void Init(IEnumerable<string>? consumableIds, IEnumerable<string>? nonConsumableIds, 
            IEnumerable<string>? subscriptionIds);
        
        UniTask<bool> RestoreAllAsync();
    }
}
