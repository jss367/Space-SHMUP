using System;
using System.Globalization;
using Soomla.Store;
using UnityEngine;
using UnityEngine.UI;

public class VirtualStoreItemView : MonoBehaviour
{
    #region Consts
    protected const string IMAGES_PATH = "Images/Products/";
    protected const string STRING_TO_REMOVE_FROM_MARKET_TITLE = " (Soomla Template)";
    protected const string BUY_FORMAT = "Buy ({0})";
    protected const string UPGRADE_FORMAT = "Upgrade to {0} ({1})";
    private readonly string TAG = typeof(VirtualStoreItemView).Name;
    #endregion

    #region Protected Variables
    protected PurchasableVirtualItem item;
    #endregion

    #region Editor Properties
    [SerializeField]
    protected Text nameLabel;
    [SerializeField]
    protected Text descriptionLabel;
    [SerializeField]
    protected Text amountLabel;
    [SerializeField]
    protected RectTransform amountPanel;
    [SerializeField]
    protected Text currentLevelLabel;
    [SerializeField]
    protected Image itemImage;
    [SerializeField]
    protected Image soldImage;
    [SerializeField]
    protected Image purchasedImage;
    [SerializeField]
    protected Image equippedImage;
    [SerializeField]
    protected Text visualBuyButtonLabel;
    [SerializeField]
    protected Button upgradeButton;
    [SerializeField]
    protected Text upgradeButtonLabel;
    [SerializeField]
    protected Button mainButton;
    #endregion

    #region Public Properties
    public PurchasableVirtualItem Item
    {
        get { return item; }
        set
        {
            item = value;

            OnVirtualGoodChanged();
        }
    }
    #endregion

    #region Protected Properties
    protected Sprite ItemSprite
    {
        get
        {
            // Try to load an image for this specific item
            Sprite sprite = null;
            
            if (Item is SingleUsePackVG)
            {
                sprite = GetSpriteAtPath((Item as SingleUsePackVG).GoodItemId);
            }
            else if (Item is VirtualCurrencyPack)
            {
                sprite = GetSpriteAtPath((Item as VirtualCurrencyPack).CurrencyItemId);
            }
            else if (Item is UpgradeVG)
            {
                sprite = GetSpriteAtPath(Item.ItemId.Remove(Item.ItemId.LastIndexOf('_')));

                if (!sprite)
                {
                    sprite = GetSpriteAtPath((Item as UpgradeVG).GoodItemId);
                }
            }

            // If a specific image doesn't exist, try to load a more generic image
            if (!sprite)
            {
                sprite = Resources.Load<Sprite>(IMAGES_PATH + item.ItemId);
            }

            // If all of the above fails, there really is no image to load
            if (!sprite)
            {
                Log(string.Format("{0} - Failed to load image!", item.ItemId), true);
            }

            return sprite;
        }
    }

    protected int Balance
    {
        get { return (Item is VirtualGood && !(Item is SingleUsePackVG)) ? Item.GetBalance() : 0; }
    }

    protected bool IsPurchased
    {
        get { return Balance > 0; }
    }

    #endregion

    #region Protected functions

    protected void Log(string message, bool isWarning = false)
    {
        if (isWarning)
        {
            Debug.LogWarning(TAG + ": " + message);
        }
        else
        {
            Debug.Log(TAG + ": " + message);
        }
    }

    protected void SetBuyButtonState(Button button, bool isInteractable)
    {
        if (button)
        {
            button.interactable = isInteractable;
            button.onClick.RemoveListener(OnClick);

            if (isInteractable)
            {
                button.onClick.AddListener(OnClick);
            }
        }
    }

    protected void OnVirtualGoodChanged()
    {
        UpdateItemData();

        itemImage.sprite = ItemSprite;

        // This item has upgrades (It is not an UpgradeVG, there are UpgradeVGs for this item)
        if (StoreInfo.GetUpgradesForVirtualGood(Item.ItemId) != null)
        {
            DisplayUpgradeUI();
        }

        // This item can be, and has been, purchased once
        if (Item is LifetimeVG && IsPurchased)
        {
            // This item is equippable
            if (Item is EquippableVG)
            {
                DisplayAsEquippable();
            }
            else
            {
                DisplayItemAsSold();
            }
        }
        // Regular item
        else
        {
            // Set item amount
            amountPanel.gameObject.SetActive(IsPurchased);
            amountLabel.text = Balance.ToString();

            SetBuyButtonState(mainButton, true);
        }
    }

    private void DisplayAsEquippable()
    {
        var isEquipped = StoreInventory.IsVirtualGoodEquipped(Item.ItemId);

        visualBuyButtonLabel.text = isEquipped ? "Unequip" : "Equip";

        equippedImage.gameObject.SetActive(isEquipped);
        purchasedImage.gameObject.SetActive(true);

        SetBuyButtonState(mainButton, true);
    }

    private void DisplayUpgradeUI()
    {
        var currentUpgradeLevel = StoreInventory.GetGoodUpgradeLevel(Item.ItemId);

        // The item has no level - Upgrade it to set its level to 1
        if (currentUpgradeLevel == 0)
        {
            StoreInventory.UpgradeGood(Item.ItemId);

            // The upgrade will trigger this function again so we're stopping here for now
            return;
        }

        var currentUpgradeItemId = StoreInventory.GetGoodCurrentUpgrade(Item.ItemId);

        var currentUpgradeVG = StoreInfo.GetItemByItemId(currentUpgradeItemId) as UpgradeVG;

        if (currentUpgradeVG == null)
        {
            throw new Exception("Failed to find UpgradeVG: " + currentUpgradeItemId);
        }

        nameLabel.text = string.Format("{0} ({1})", Item.Name, currentUpgradeLevel);
       
        currentLevelLabel.text = currentUpgradeVG.Name;

        currentLevelLabel.enabled = true;

        var nextUpgradeVG =
            // If there's an item available as the next upgrade - get its details
            !string.IsNullOrEmpty(currentUpgradeVG.NextItemId)
                ? (StoreInfo.GetItemByItemId(currentUpgradeVG.NextItemId) as UpgradeVG)
                : null;

        upgradeButton.gameObject.SetActive(true);

        if (upgradeButtonLabel)
        {
            // The next upgrade is available
            if (nextUpgradeVG != null)
            {
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.onClick.AddListener(() => UpgradeItem(nextUpgradeVG));

                upgradeButtonLabel.text = string.Format(UPGRADE_FORMAT,
                    (currentUpgradeLevel + 1),
                    GetPurchaseTypePrice(nextUpgradeVG.PurchaseType));
            }
            // There are no more upgrades (fully upgraded)
            else
            {
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.interactable = false;
                upgradeButtonLabel.text = "Upgraded to MAX";
            }
        }
        else
        {
            Log("Upgrade button has no label so we can't display the next upgrade's level - was this intended?", true);
        }
    }

    protected void DisplayItemAsSold()
    {
        var color = itemImage.color;
        color.a = .25f;
        itemImage.color = color;

        soldImage.enabled = true;

        SetBuyButtonState(mainButton, false);
    }

    protected string GetPurchaseTypePrice(PurchaseType purchaseType)
    {
        var purchaseWithMarket = purchaseType as PurchaseWithMarket;

        if (purchaseWithMarket != null)
        {
            return !string.IsNullOrEmpty(purchaseWithMarket.MarketItem.MarketPriceAndCurrency)
                ? purchaseWithMarket.MarketItem.MarketPriceAndCurrency
                : purchaseWithMarket.MarketItem.Price.ToString("C", CultureInfo.CurrentCulture);
        }

        var purchaseWithVirtualItem = purchaseType as PurchaseWithVirtualItem;

        if (purchaseWithVirtualItem != null)
        {
            return purchaseWithVirtualItem.Amount.ToString("G7") + " " + StoreInfo.GetItemByItemId(purchaseWithVirtualItem.TargetItemId).Name + "s";
        }

        return string.Empty;
    }

    protected Sprite GetSpriteAtPath(string productId)
    {
        var imagePath = IMAGES_PATH + productId;

        Log(string.Format("{0} - Looking for image at {1}", item.ItemId, imagePath));

        return Resources.Load<Sprite>(imagePath);
    }

    protected void OnClick()
    {
        if (Item is EquippableVG && IsPurchased)
        {
            EquipOrUnequipItem();
        }
        else
        {
            BuyItem();
        }
    }

    protected void EquipOrUnequipItem()
    {
        MenuView.SetLoadingOverlayVisiblity(true);

        if (StoreInventory.IsVirtualGoodEquipped(Item.ItemId))
        {
            StoreInventory.UnEquipVirtualGood(Item.ItemId);
        }
        else
        {
            StoreInventory.EquipVirtualGood(Item.ItemId);
        }

        MenuView.SetLoadingOverlayVisiblity(false);
    }

    protected void BuyItem()
    {
        try
        {
            StoreInventory.BuyItem(Item.ItemId);
        }
        catch (InsufficientFundsException)
        {
            HandleInsufficientFundsException(Item);
        }
    }

    private void UpgradeItem(PurchasableVirtualItem upgrade)
    {
        try
        {
            StoreInventory.UpgradeGood(Item.ItemId);
        }
        catch (Exception)
        {
            HandleInsufficientFundsException(upgrade);
        }
    }

    private void HandleInsufficientFundsException(PurchasableVirtualItem errorItem)
    {
        var currency = StoreInfo.Currencies[0];

        var message = "You don't have enough " + currency.Name + "s";

        var purchaseWithVirtualItem = errorItem.PurchaseType as PurchaseWithVirtualItem;

        if (purchaseWithVirtualItem != null)
        {
            message = string.Format("'{0}' costs {1} {2}s, but you only have {3}",
                                    errorItem.Name,
                                    purchaseWithVirtualItem.Amount,
                                    currency.Name.ToLower(),
                                    currency.GetBalance());
        }

        PopUpMessage.Show("Insufficient Funds!", message);

        MenuView.SetLoadingOverlayVisiblity(false);
    }
    #endregion

    #region Public Functions
    public void UpdateItemData(MarketItem marketItem = null)
    {
        if (!visualBuyButtonLabel)
        {
            Log("Buy button has no label so we can't display the next upgrade's level - was this intended?", true);
        }

        if (marketItem != null)
        {
            Item.Name = marketItem.MarketTitle.Replace(STRING_TO_REMOVE_FROM_MARKET_TITLE, string.Empty);
            Item.Description = marketItem.MarketDescription;

            var purchaseWithMarket = Item.PurchaseType as PurchaseWithMarket;

            if (purchaseWithMarket != null)
            {
                purchaseWithMarket.MarketItem = marketItem;
            }
        }

        nameLabel.text = Item.Name;
        descriptionLabel.text = Item.Description;
        visualBuyButtonLabel.text = string.Format(BUY_FORMAT, GetPurchaseTypePrice(Item.PurchaseType));
    }
    #endregion
}