using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public abstract class Purchase : IPurchase
    {
        public UniTask<bool> ConsumeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
