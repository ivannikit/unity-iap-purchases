#nullable enable

namespace TeamZero.InAppPurchases
{
    public static class FakePurchaseFactory
    {
        public static IPurchase EmulateConsumable() => FakeConsumablePurchase.Create("Fake 9.99 $");
        public static IPurchase EmulateNonConsumable() => FakeNonConsumablePurchase.Create("Fake 9.99 $");
        public static IPurchase NotSupported() => NotSupportedPurchase.Create("Fake 9.99 $");
    }
}
