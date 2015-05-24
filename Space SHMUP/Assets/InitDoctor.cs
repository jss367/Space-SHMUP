//using UnityEngine;
//using System.Collections;
//
//public class InitDoctor : BaseController {
//
//	protected override string StatusName
//	{
//		get { return "IsStoreInitialized"; }
//	}
//
//	protected override void Init()
//	{
//		MenuView.SetLoadingOverlayVisiblity(true);
//		
//		//		SoomlaStore.Initialize(new ExampleAssets());
////		SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets());
//	}
//
//	protected override void RegisterEvents()
//	{
////		StoreEvents.OnItemPurchaseStarted += item => MenuView.SetLoadingOverlayVisiblity(true);
////		StoreEvents.OnItemPurchased += (item, payload) => MenuView.SetLoadingOverlayVisiblity(false);
////		StoreEvents.OnMarketItemsRefreshStarted += () => MenuView.SetLoadingOverlayVisiblity(true);
////		StoreEvents.OnMarketItemsRefreshFinished += OnMarketItemsRefreshFinished;
////		StoreEvents.OnRestoreTransactionsStarted += () => MenuView.SetLoadingOverlayVisiblity(true);
////		StoreEvents.OnMarketPurchaseStarted += item => MenuView.SetLoadingOverlayVisiblity(true);
////		StoreEvents.OnMarketPurchaseCancelled += item => MenuView.SetLoadingOverlayVisiblity(false);
////		StoreEvents.OnMarketPurchase += (item, payload, extra) => MenuView.SetLoadingOverlayVisiblity(false);
////		StoreEvents.OnCurrencyBalanceChanged += (currency, newBalance, amountAdded) => UpdateCoinBalanceLabel(currency, newBalance);
////		StoreEvents.OnUnexpectedErrorInStore += OnUnexpectedErrorInStore;
////		StoreEvents.OnGoodBalanceChanged += (item, newBalance, amountAdded) => UpdateItemView(item);
////		StoreEvents.OnSoomlaStoreInitialized += OnSoomlaStoreInitialized;
////		StoreEvents.OnGoodEquipped += UpdateItemView;
////		StoreEvents.OnGoodUnEquipped += UpdateItemView;
////		StoreEvents.OnGoodUpgrade += (item, upgrade) => UpdateItemView(item);
//	}
//
//	private void OnSoomlaStoreInitialized()
//	{
//		IsStatusOK = true;
////		
////		UpdateCoinBalanceLabel();
////		
////		InitStoreItems();
//		
//		//        GiveStarterItems();
//	}
//
//	// Use this for initialization
//	void Start () {
////		Soomla.Store.SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets()); 
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//	
//	}
//}
