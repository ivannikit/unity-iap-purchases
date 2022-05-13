using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TeamZero.InAppPurchases
{
    public class UnityIAPHub : MonoBehaviour, IStoreHub, IPurchaseHub//, IStoreListener
    {
        public static UnityIAPHub Create() => new ();
        
        public void Init(IEnumerable<string> consumableIds, IEnumerable<string> nonConsumableIds, IEnumerable<string> subscriptionIds)
        {
            throw new System.NotImplementedException();
        }

        public UniTask<bool> RestoreAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public UniTask<bool> PurchaseAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
