#nullable enable
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace TeamZero.InAppPurchases
{
    internal interface IPlatformHub : IStoreHub, IPurchaseHub
    {
    }
}
