using System.Collections.Generic;
using Soomla.Store;

public class ExampleAssets : IStoreAssets
{
    #region Consts
    private const string COIN_PACK_3_PRODUCT_ID = "coins_3";
    private const string COIN_PACK_5_PRODUCT_ID = "coins_5";
    private const string COIN_PACK_10_PRODUCT_ID = "coins_10";
    private const string SHIELD_PRODUCT_ID = "shield";
    private const string SHIELD_PACK_5_PRODUCT_ID = "shields_5";
    private const string NO_ADS_PRODUCT_ID = "no_ads";
    private const string ARMOR_PRODUCT_ID = "armor";
    private const string SOOMLA_SHIRT_PRODUCT_ID = "soomla_shirt";
    private const string SPARTONIX_SHIRT_PRODUCT_ID = "spartonix_shirt";
    private const string SHIELD_DURABILITY_PRODUCT_ID = "shield_dur_";
    private const string SHIELD_DURABILITY_NAME = "Durability ";
    private const string SHIELD_DURABILITY_DESC = "Increases shield durability to ";
    #endregion

    #region Categories
    public static VirtualCategory WearableGearCategory = new VirtualCategory(
        "WearableGear", new List<string>
        {
            ARMOR_PRODUCT_ID,
            SOOMLA_SHIRT_PRODUCT_ID,
            SPARTONIX_SHIRT_PRODUCT_ID
        }
    );
    #endregion

    #region Virtual Currencies
    /// <summary>
    /// A coin that serves as an in-game currency
    /// </summary>
    public static VirtualCurrency Coin = new VirtualCurrency(
      "Coin",                           // Name
      "Collect coins to buy items",     // Description
      "currency_coin"                   // Item ID
    );
    #endregion

    #region Virtual Currency Packs
    public static VirtualCurrencyPack CoinPack3 = new VirtualCurrencyPack(
        "3 Coins",                          // Name
        "This is a 3-coin pack",            // Description
        COIN_PACK_3_PRODUCT_ID,             // Item ID
        3,                                  // Number of currencies in the pack
        Coin.ItemId,                        // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            COIN_PACK_3_PRODUCT_ID,         // Product ID
            0.99)                           // Initial price
        );

    public static VirtualCurrencyPack CoinPack5 = new VirtualCurrencyPack(
        "5 Coins",                          // Name
        "This is a 5-coin pack",            // Description
        COIN_PACK_5_PRODUCT_ID,             // Item ID
        5,                                  // Number of currencies in the pack
        Coin.ItemId,                        // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            COIN_PACK_5_PRODUCT_ID,         // Product ID
            1.99)                           // Initial price
        );

    public static VirtualCurrencyPack CoinPack10 = new VirtualCurrencyPack(
        "10 Coins",                         // Name
        "This is a 10-coin pack",           // Description
        COIN_PACK_10_PRODUCT_ID,            // Item ID
        10,                                 // Number of currencies in the pack
        Coin.ItemId,                        // The currency associated with this pack
        new PurchaseWithMarket(             // Purchase type
            COIN_PACK_10_PRODUCT_ID,        // Product ID
            2.49)                           // Initial price
        );
    #endregion

    #region Virtual Goods
    /// <summary>
    /// Shield that can be purchased for 150 coins.
    /// </summary>
    public static VirtualGood Shield = new SingleUseVG(
        "Shield",                           // Name
        "Shields you from monsters",        // Description
        SHIELD_PRODUCT_ID,                  // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            150)                            // Payment amount
    );

    /// <summary>
    /// Pack of 5 shields that can be purchased for $2.99.
    /// </summary>
    public static VirtualGood ShieldPack5 = new SingleUsePackVG(
        Shield.ItemId,                 // Good Item ID
        5,                             // Amount
        "5 Shields",                   // Name
        "This is a 5-shield pack",     // Description
        SHIELD_PACK_5_PRODUCT_ID,      // Item ID
        new PurchaseWithMarket(        // Purchase type
            SHIELD_PACK_5_PRODUCT_ID,  // Product ID
            2.99)                      // Initial price
        );

    /// <summary>
    /// Remove ads from the game, purchased for $0.99.
    /// </summary>
    public static VirtualGood NoAds = new LifetimeVG(
        "No Ads",                       // Name
        "Remove ads forever",           // Description
        NO_ADS_PRODUCT_ID,              // Item ID
        new PurchaseWithMarket(         // Purchase type
            NO_ADS_PRODUCT_ID,          // Product ID
            0.99)                       // Initial price
        );

    #region Equipables
    /// <summary>
    /// An equipable weapon that can be purchased for 7 coins.
    /// </summary>
    public static VirtualGood Sword = new EquippableVG(
        EquippableVG.EquippingModel.LOCAL,  // Equipping Model
        "Sword",                            // Name
        "Slash your enemies!",              // Description
        "sword",                            // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            7));                            // Payment amount

    /// <summary>
    /// An equipable weapon that can be purchased for 7 coins.
    /// </summary>
    public static VirtualGood PlasmaGun = new EquippableVG(
        EquippableVG.EquippingModel.LOCAL,  // Equipping Model
        "Plasma Gun",                       // Name
        "Spray & Pray",                     // Description
        "plasma_gun",                       // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            7));                           // Payment amount

    /// <summary>
    /// An equipable weapon that can be purchased for 7 coins.
    /// </summary>
    public static VirtualGood SoomlaBotSidekick = new EquippableVG(
        EquippableVG.EquippingModel.LOCAL,  // Equipping Model
        "SoomlaBot Sidekick",               // Name
        "The best sidekick EVER!",          // Description
        "soomlabot_sidekick",               // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            7));                           // Payment amount

    /// <summary>
    /// An equipable armor that can be purchased for 7 coins.
    /// </summary>
    public static VirtualGood Armor = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,   // Equipping Model
        "Armor",                                // Name
        "Increases you defense",                // Description
        ARMOR_PRODUCT_ID,                       // Item ID
        new PurchaseWithVirtualItem(            // Purchase type
            Coin.ItemId,                        // Virtual item to pay with
            7));                                // Payment amount

    /// <summary>
    /// An equipable shirt that can be purchased for 17 coins.
    /// </summary>
    public static VirtualGood SoomlaShirt = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,   // Equipping Model
        "Soomla Shirt",                         // Name
        "Spread the word - Soomla's great!",    // Description
        SOOMLA_SHIRT_PRODUCT_ID,                // Item ID
        new PurchaseWithVirtualItem(            // Purchase type
            Coin.ItemId,                        // Virtual item to pay with
            17));                               // Payment amount

    /// <summary>
    /// An equipable shirt that can be purchased for 27 coins.
    /// </summary>
    public static VirtualGood SpartonixShirt = new EquippableVG(
        EquippableVG.EquippingModel.CATEGORY,   // Equipping Model
        "Spartonix Shirt",                      // Name
        "Look cool :)",                         // Description
        SPARTONIX_SHIRT_PRODUCT_ID,             // Item ID
        new PurchaseWithVirtualItem(            // Purchase type
            Coin.ItemId,                        // Virtual item to pay with
            27));                               // Payment amount 

    /// <summary>
    /// A playable character that can be purchased for 27 coins.
    /// </summary>
    public static VirtualGood Character1 = new EquippableVG(
        EquippableVG.EquippingModel.GLOBAL, // Equipping Model
        "Character 1",                      // Name
        "Character #1",                     // Description
        "character_1",                      // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            0));                            // Payment amount 

    /// <summary>
    /// A playable character that can be purchased for 27 coins.
    /// </summary>
    public static VirtualGood Character2 = new EquippableVG(
        EquippableVG.EquippingModel.GLOBAL, // Equipping Model
        "Character 2",                      // Name
        "Character #2",                     // Description
        "character_2",                      // Item ID
        new PurchaseWithVirtualItem(        // Purchase type
            Coin.ItemId,                    // Virtual item to pay with
            1000));                         // Payment amount 
    #endregion

    #region Upgrades
    /// <summary>
    /// Upgrade shield durability level 1
    /// </summary>
    public static VirtualGood ShieldDurability1 = CreateUpgrade(
        Shield,                         // Upgraded Item
        SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
        SHIELD_DURABILITY_NAME + 3,     // Name
        SHIELD_DURABILITY_DESC,         // Decription
        1,                              // Level
        0);                             // Price (Costs 0 so it can be set as default)

    /// <summary>
    /// Upgrade shield durability level 2
    /// </summary>
    public static VirtualGood ShieldDurability2 = CreateUpgrade(
        Shield,                         // Upgraded Item
        SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
        SHIELD_DURABILITY_NAME + 7,     // Name
        SHIELD_DURABILITY_DESC,         // Decription
        2,                              // Level
        12);                            // Price

    /// <summary>
    /// Upgrade shield durability level 3
    /// </summary>
    public static VirtualGood ShieldDurability3 = CreateUpgrade(
        Shield,                         // Upgraded Item
        SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
        SHIELD_DURABILITY_NAME + 15,    // Name
        SHIELD_DURABILITY_DESC,         // Decription
        3,                              // Level
        30);                            // Price

    /// <summary>
    /// Upgrade shield durability level 4
    /// </summary>
    public static VirtualGood ShieldDurability4 = CreateUpgrade(
        Shield,                         // Upgraded Item
        SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
        SHIELD_DURABILITY_NAME + 32,    // Name
        SHIELD_DURABILITY_DESC,         // Decription
        4,                              // Level
        80);                            // Price

    /// <summary>
    /// Upgrade shield durability level 5
    /// </summary>
    public static VirtualGood ShieldDurability5 = CreateUpgrade(
        Shield,                         // Upgraded Item
        SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
        SHIELD_DURABILITY_NAME + 100,   // Name
        SHIELD_DURABILITY_DESC,         // Decription
        5,                              // Level
        200,                            // Price
        true);                          // Last 
    #endregion
    #endregion

    #region Private Functions
    private static VirtualGood CreateUpgrade(VirtualItem upgradedGood, string upgradeItemId, string upgradeName, string upgradeDescription, int level, int price, bool isLast = false)
    {
        var prevItemId = level > 1 ? upgradeItemId + (level - 1) : null;
        var nextItemId = isLast ? null : upgradeItemId + (level + 1);

        return new UpgradeVG(
        upgradedGood.ItemId,            // Good Item ID
        nextItemId,                     // Next Upgrade Item ID
        prevItemId,                     // Previous Upgrade Item ID
        upgradeName,                    // Name
        upgradeDescription + level,     // Description
        upgradeItemId + level,          // Item ID
        new PurchaseWithVirtualItem(    // Purchase type
            Coin.ItemId,                // Virtual item to pay with
            price)                      // Payment amount
        );
    }
    #endregion

    #region Public Functions
    public int GetVersion()
    {
        return 1;
    }

    public VirtualCurrency[] GetCurrencies()
    {
        return new[] { Coin };
    }

    public VirtualGood[] GetGoods()
    {
        return new[]
        {
            Shield,
            ShieldPack5,
            NoAds,
            Sword,
            PlasmaGun,
            SoomlaBotSidekick,
            Armor,
            SoomlaShirt,
            SpartonixShirt,
            Character1,
            Character2,
            ShieldDurability1,
            ShieldDurability2,
            ShieldDurability3,
            ShieldDurability4,
            ShieldDurability5
        };
    }

    public VirtualCurrencyPack[] GetCurrencyPacks()
    {
        return new[] { CoinPack3, CoinPack5, CoinPack10 };
    }

    public VirtualCategory[] GetCategories()
    {
        return new[] { WearableGearCategory };
    }
    #endregion
}