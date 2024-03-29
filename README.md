# TeamZero Unity IAP Purchases

Hi level wrapper for Unity IAP package.

```csharp
IPurchase fakePurchase = FakeNonConsumablePurchase.Create(price: "9.99 $", responseDelay: TimeSpan.FromSeconds(5d));
...

Log log = Log.Default();
Store store = Store.Create(log);

IPurchase nonConsumablePurchase = store.RegisterNewConsumable("CONSUMABLE_ID");
IRestorablePurchase nonConsumablePurchase = store.RegisterNewNonConsumable("NON_CONSUMABLE_ID");
ISubscription subscription = RegisterNewSubscription("SUBSCRIPTION_ID")
...

await store.InitAsync();
...

nonConsumablePurchase.RestoredAsObservable()
    .Subscribe(_ => 
    {
        bool isConsumed = nonConsumablePurchase.IsConsumed();
        ...
    });
await store.RestoreAllAsync();
...

string price = nonConsumablePurchase.LocalizedPriceText();
bool isConsumed = await nonConsumablePurchase.ConsumeAsync();
```