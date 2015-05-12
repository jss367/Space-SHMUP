using System.Collections.Generic;
using System.Linq;
using Soomla.Store;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : BaseController
{
    #region Editor Properties
    public Text BalanceLabel;
    public RectTransform CurrencyPacksContainer;
    public RectTransform GoodsContainer;
    #endregion

    #region Override Properties
    protected override string StatusName
    {
        get { return "IsStoreInitialized"; }
    }
    #endregion

    #region Override Functions
    protected override void Init()
    {
        MenuView.SetLoadingOverlayVisiblity(true);

		SoomlaStore.Initialize(new ExampleAssets());
//		SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets());
    }

    protected override void RegisterEvents()
    {
        StoreEvents.OnItemPurchaseStarted += item => MenuView.SetLoadingOverlayVisiblity(true);
        StoreEvents.OnItemPurchased += (item, payload) => MenuView.SetLoadingOverlayVisiblity(false);
        StoreEvents.OnMarketItemsRefreshStarted += () => MenuView.SetLoadingOverlayVisiblity(true);
        StoreEvents.OnMarketItemsRefreshFinished += OnMarketItemsRefreshFinished;
        StoreEvents.OnRestoreTransactionsStarted += () => MenuView.SetLoadingOverlayVisiblity(true);
        StoreEvents.OnMarketPurchaseStarted += item => MenuView.SetLoadingOverlayVisiblity(true);
        StoreEvents.OnMarketPurchaseCancelled += item => MenuView.SetLoadingOverlayVisiblity(false);
        StoreEvents.OnMarketPurchase += (item, payload, extra) => MenuView.SetLoadingOverlayVisiblity(false);
        StoreEvents.OnCurrencyBalanceChanged += (currency, newBalance, amountAdded) => UpdateCoinBalanceLabel(currency, newBalance);
        StoreEvents.OnUnexpectedErrorInStore += OnUnexpectedErrorInStore;
        StoreEvents.OnGoodBalanceChanged += (item, newBalance, amountAdded) => UpdateItemView(item);
        StoreEvents.OnSoomlaStoreInitialized += OnSoomlaStoreInitialized;
        StoreEvents.OnGoodEquipped += UpdateItemView;
        StoreEvents.OnGoodUnEquipped += UpdateItemView;
        StoreEvents.OnGoodUpgrade += (item, upgrade) => UpdateItemView(item);
    }
    #endregion

    #region Private Functions
    private void OnMarketItemsRefreshFinished(List<MarketItem> updatedMarketItems)
    {
        var virtualStoreItemViews = FindObjectsOfType<VirtualStoreItemView>();

        foreach (var updatedMarketItem in updatedMarketItems)
        {
            var virtualStoreItemView = virtualStoreItemViews.SingleOrDefault(i => i.Item.ItemId == updatedMarketItem.ProductId);

            if (virtualStoreItemView)
            {
                virtualStoreItemView.UpdateItemData(updatedMarketItem);
            }
        }

        MenuView.SetLoadingOverlayVisiblity(false);
    }

    private void OnRestoreTransactionsFinished(bool isSuccess)
    {
        MenuView.SetLoadingOverlayVisiblity(false);

        PopUpMessage.Show("Restore Transactions", "Result: " + (isSuccess ? "Success!" : "Failed..."));
    }

    private void UpdateCoinBalanceLabel(VirtualCurrency currency = null, float? newBalance = null)
    {
        currency = currency ?? StoreInfo.Currencies[0];

        var balance = newBalance.HasValue ? newBalance.Value : currency.GetBalance();

		BalanceLabel.text = balance + " " + currency.Name + "s";
    }

    private void OnUnexpectedErrorInStore(string error)
    {
        PopUpMessage.Show("Error", error);
        MenuView.SetLoadingOverlayVisiblity(false);
    }

    private void UpdateItemView(VirtualGood item)
    {
        var virtualStoreItemView =
            FindObjectsOfType<VirtualStoreItemView>()
            .SingleOrDefault(v => v.Item != null && v.Item.ItemId == item.ItemId);

        if (virtualStoreItemView != null)
        {
            virtualStoreItemView.Item = item;
        }
    }

    private void OnSoomlaStoreInitialized()
    {
        IsStatusOK = true;

        UpdateCoinBalanceLabel();

        InitStoreItems();

//        GiveStarterItems();
    }

//    private void GiveStarterItems()
//    {
//        // Give the player the 1st character
//        if (StoreInventory.GetItemBalance(ExampleAssets.Character1.ItemId) == 0)
//        {
//            StoreInventory.GiveItem(ExampleAssets.Character1.ItemId, 1);
//        }
//    }

    private void InitStoreItems()
    {
        var itemTemplatePrefab = Resources.Load<GameObject>("Prefabs/VirtualStoreItem");

        var goods = StoreInfo.Goods.Where(g => !(g is UpgradeVG)).ToArray();

        InitContainer(GoodsContainer, goods, itemTemplatePrefab);
        InitContainer(CurrencyPacksContainer, StoreInfo.CurrencyPacks.ToArray(), itemTemplatePrefab);

        MenuView.SetLoadingOverlayVisiblity(false);
    }

    private void InitContainer(RectTransform container, ICollection<PurchasableVirtualItem> items, Object itemTemplatePrefab)
    {
        // Calculate a single item width
        var singleItemWidth = container.rect.width / container.childCount;

        var layout = container.GetComponent<HorizontalLayoutGroup>();

        if (layout)
        {
            singleItemWidth -= layout.spacing / (container.childCount + 1);
        }

        container.anchorMin = new Vector2(.5f, container.anchorMin.y);
        container.anchorMax = new Vector2(.5f, container.anchorMax.y);
        container.pivot = new Vector2(0, .5f);

        // Expand the container to its desired size
        container.sizeDelta = Vector2.right * items.Count * singleItemWidth;

        // Got the design parameters we need from the items - now get rid of them
        DestroyVirtualStoreItemViews(container);

        // Instantiate a new prefab for each virtual item
        foreach (var item in items)
        {
            var itemGameObject = Instantiate(itemTemplatePrefab) as GameObject;

            if (itemGameObject)
            {
                var virtualStoreItemTransform = itemGameObject.GetComponent<RectTransform>();

                virtualStoreItemTransform.SetParent(container);
                virtualStoreItemTransform.localScale = Vector3.one;

                var virtualStoreItemView = itemGameObject.GetComponent<VirtualStoreItemView>() ??
                                           itemGameObject.AddComponent<VirtualStoreItemView>();

                virtualStoreItemView.Item = item;
            }
        }

    }

    /// <summary>
    /// Destroy all children that were created during editing mode
    /// </summary>
    /// <param name="goodsContainer"></param>
    private void DestroyVirtualStoreItemViews(Transform goodsContainer)
    {
        for (var i = 0; i < goodsContainer.childCount; i++)
        {
            Destroy(goodsContainer.GetChild(i).gameObject);
        }
    }
    #endregion

    #region Public Functions
    public void RefreshInventory()
    {
        Debug.Log("RefreshInventory");

        SoomlaStore.RefreshInventory();
    }

    public void RestoreTransactions()
    {
        Debug.Log("RestoreTransactions");

        // We register the event here and not on the init method so the message will show only on user interaction
        // Instead of adding a new handler for this event with [+=], we make sure it's the only handler
        StoreEvents.OnRestoreTransactionsFinished = OnRestoreTransactionsFinished;

        SoomlaStore.RestoreTransactions();
    } 
    #endregion
}