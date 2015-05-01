using UnityEngine;
using System.Collections;
using Soomla.Store;



public class GameAssets:IStoreAssets {
	//Update the 0 if you add more avaialbe items later, or else you will get errors.
	
	public int GetVersion() {
		return 0;
	}
	
	public VirtualCurrency[] GetCurrencies ()
	{
		return new VirtualCurrency[]{};
	}
	public VirtualGood[] GetGoods ()
	{
		return new VirtualGood[] {};
	}
	public VirtualCurrencyPack[] GetCurrencyPacks ()
	{
		return new VirtualCurrencyPack[] {};
	}
	public VirtualCategory[] GetCategories ()
	{
		return new VirtualCategory[] {};
	}
	public LifetimeVG[] GetNonConsumableItems ()
	{
		return new LifetimeVG[]{NO_ADS};
	}
	
	
	#if UNITY_ANDROID
	public const string NO_ADDS_PRODUCT_ID = "se.dixum.tut.soomla.android.removeads";
	#elif UNITY_IPHONE
	public const string NO_ADDS_PRODUCT_ID = "se.dixum.tut.soomla.ios.removeads";
	#endif
	
	public const string NO_ADDS_ITEM_ID = "no_ads";
	
	public static LifetimeVG NO_ADS = new LifetimeVG (
		"No Ads",
		"Removes ads from the game",
		NO_ADDS_ITEM_ID,
		new PurchaseWithMarket (new MarketItem (NO_ADDS_PRODUCT_ID, MarketItem.Consumable.NONCONSUMABLE, 0.99))
		);
	

	
	
	
	
}