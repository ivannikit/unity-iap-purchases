#nullable enable
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    public abstract class Purchase : IPurchase
    {
        private readonly string _id;

        internal Purchase(string id)
        {
            _id = id;
        }

        public UniTask<bool> ConsumeAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
