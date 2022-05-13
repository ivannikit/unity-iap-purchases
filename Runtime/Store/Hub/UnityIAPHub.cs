using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;

namespace TeamZero.InAppPurchases
{
    public class UnityIAPHub : MonoBehaviour, IStoreHub, IPurchaseHub, IStoreListener
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

        void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
        {
            throw new System.NotImplementedException();
        }

        PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new System.NotImplementedException();
        }

        void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new System.NotImplementedException();
        }

        void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            throw new System.NotImplementedException();
        }
    }
}
