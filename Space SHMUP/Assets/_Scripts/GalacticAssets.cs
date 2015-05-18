/// Copyright (C) 2012-2014 Soomla Inc.
///
/// Licensed under the Apache License, Version 2.0 (the "License");
/// you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at
///
///      http://www.apache.org/licenses/LICENSE-2.0
///
/// Unless required by applicable law or agreed to in writing, software
/// distributed under the License is distributed on an "AS IS" BASIS,
/// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
/// See the License for the specific language governing permissions and
/// limitations under the License.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Soomla.Store.Example {
	
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class GalacticAssets : IStoreAssets{
		
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion() {
			return 0;
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies() {
			return new VirtualCurrency[]{GALACTIC_CURRENCY};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods() {
//			return new  VirtualGood[] {WEAPON_SPREAD, PAVLOVA_GOOD,CHOCLATECAKE_GOOD, CREAMCUP_GOOD, NO_ADS_LTVG, 
//				ShieldUpgrade1, ShieldUpgrade2, Shield, Sword};
			return new  VirtualGood[] {WEAPON_SPREAD, ShieldUpgrade, NO_ADS_LTVG};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks() {
			return new VirtualCurrencyPack[] {TEN_COIN_PACK, FIFTY_COIN_PACK, FIVEHUND_COIN_PACK};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories() {
			return new VirtualCategory[]{GENERAL_CATEGORY};
		}

		#region Public Functions

//				
//				public VirtualGood[] GetGoods()
//				{
//					return new[]
//					{
		//				Shield,
		//				ShieldPack5,
		//				NoAds,
		//				Sword,
		//				PlasmaGun,
		//				SoomlaBotSidekick,
		//				Armor,
		//				SoomlaShirt,
		//				SpartonixShirt,
		//				Character1,
		//				Character2,
		//				ShieldDurability1,
		//				ShieldDurability2,
		//				ShieldDurability3,
		//				ShieldDurability4,
//		//				ShieldDurability5
//					};
//				}
		
		//		public VirtualCurrencyPack[] GetCurrencyPacks()
		//		{
		//			return new[] { CoinPack3, CoinPack5, CoinPack10 };
		//		}
		//		
		//		public VirtualCategory[] GetCategories()
		//		{
		//			return new[] { WearableGearCategory };
		//		}
		#endregion
		
		/** Static Final Members **/
		
		public const string GALACTIC_CURRENCY_ITEM_ID      = "galactic_currency"; //add the reverse domain?
		// com.gleeza.galacticbeats.galactic_currency?
		
		public const string SPREAD_GUN_ITEM_ID = "weapon_spread";
		
		public const string TENMUFF_PACK_PRODUCT_ID      = "android.test.refunded";
		
		public const string FIFTYMUFF_PACK_PRODUCT_ID    = "android.test.canceled";
		
		public const string FOURHUNDMUFF_PACK_PRODUCT_ID = "android.test.purchased";
		
		public const string THOUSANDMUFF_PACK_PRODUCT_ID = "2500_pack";
		
		//		public const string MUFFINCAKE_ITEM_ID   = "blaster_gun";
		
		public const string PAVLOVA_ITEM_ID   = "pavlova";
		
		public const string CHOCLATECAKE_ITEM_ID   = "chocolate_cake";
		
		public const string CREAMCUP_ITEM_ID   = "cream_cup";
		
		public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";

		public const string SHIELD_UPGRADE_1 = "shield_1";
		public const string SHIELD_UPGRADE_2 = "shield_2";
		public const string SHIELD_DURABILITY_PRODUCT_ID = "shield_dur_";
		public const string SHIELD_DURABILITY_NAME = "Durability ";
		public const string SHIELD_DURABILITY_DESC = "Increases shield durability to ";
		public const string SHIELD_PRODUCT_ID = "hero_shield";

		
		
		/** Virtual Currencies **/
		
		public static VirtualCurrency GALACTIC_CURRENCY = new VirtualCurrency(
			"Milky Buck",										// name
			"Currency in the Milky Way galaxy",					// description
			GALACTIC_CURRENCY_ITEM_ID							// item id
			);
		
		
		/** Virtual Currency Packs **/
		
		public static VirtualCurrencyPack TEN_COIN_PACK = new VirtualCurrencyPack(
			"10 Milky Bucks",                                   // name
			"Test refund of an item",                       // description
			"muffins_10",                                   // item id
			10,												// number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket(TENMUFF_PACK_PRODUCT_ID, 0.99)
			);
		
		public static VirtualCurrencyPack FIFTY_COIN_PACK = new VirtualCurrencyPack(
			"50 Milky Bucks",                                   // name
			"Test cancellation of an item",                 // description
			"muffins_50",                                   // item id
			50,                                             // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket(FIFTYMUFF_PACK_PRODUCT_ID, 1.99)
			);
		
		public static VirtualCurrencyPack FIVEHUND_COIN_PACK = new VirtualCurrencyPack(
			"500 Milky Bucks",                                  // name
			"Test purchase of an item",                 	// description
			"muffins_400",                                  // item id
			500,                                            // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket(FOURHUNDMUFF_PACK_PRODUCT_ID, 4.99)
			);

		public static VirtualCurrencyPack COCONUT = new VirtualCurrencyPack(
			"A coconut",                                  // name
			"Test purchase of an item",                 	// description
			"muffins_400",                                  // item id
			500,                                            // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket(FOURHUNDMUFF_PACK_PRODUCT_ID, 4.99)
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
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
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
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
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
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            7));                           // Payment amount
		
//		/// <summary>
//		/// An equipable armor that can be purchased for 7 coins.
//		/// </summary>
//		public static VirtualGood Armor = new EquippableVG(
//			EquippableVG.EquippingModel.CATEGORY,   // Equipping Model
//			"Armor",                                // Name
//			"Increases you defense",                // Description
//			ARMOR_PRODUCT_ID,                       // Item ID
//			new PurchaseWithVirtualItem(            // Purchase type
//		                            GALACTIC_CURRENCY.ItemId,                        // Virtual item to pay with
//		                            7));                                // Payment amount
//		
//		/// <summary>
//		/// An equipable shirt that can be purchased for 17 coins.
//		/// </summary>
//		public static VirtualGood SoomlaShirt = new EquippableVG(
//			EquippableVG.EquippingModel.CATEGORY,   // Equipping Model
//			"Soomla Shirt",                         // Name
//			"Spread the word - Soomla's great!",    // Description
//			SOOMLA_SHIRT_PRODUCT_ID,                // Item ID
//			new PurchaseWithVirtualItem(            // Purchase type
//		                            GALACTIC_CURRENCY.ItemId,                        // Virtual item to pay with
//		                            17));                               // Payment amount
		
		/// <summary>
		/// A playable character that can be purchased for 27 coins.
		/// </summary>
		public static VirtualGood Character1 = new EquippableVG(
			EquippableVG.EquippingModel.GLOBAL, // Equipping Model
			"Character 1",                      // Name
			"Character #1",                     // Description
			"character_1",                      // Item ID
			new PurchaseWithVirtualItem(        // Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
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
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            1000));                         // Payment amount 
		#endregion

		#region Upgrades
		/// <summary>
		/// Upgrade shield durability level 1
		/// </summary>
//		public static VirtualGood ShieldUpgrade1 = CreateUpgrade(
		//	SHIELD_DURABILITY_NAME,                         // Upgraded Item
//			SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
//			SHIELD_DURABILITY_NAME + 3,     // Name
//			SHIELD_DURABILITY_DESC,         // Decription
//			1,                              // Level
//			0);                             // Price (Costs 0 so it can be set as default)


		public static VirtualGood ShieldUpgrade1 = new UpgradeVG(
			SHIELD_PRODUCT_ID,
			SHIELD_UPGRADE_2,
			null,
			SHIELD_DURABILITY_NAME,
			SHIELD_DURABILITY_DESC,
			SHIELD_UPGRADE_1,
			new PurchaseWithVirtualItem(        // Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            10));


		public static VirtualGood ShieldUpgrade2 = new UpgradeVG(
			SHIELD_PRODUCT_ID,
			null,
			SHIELD_UPGRADE_1,
			SHIELD_DURABILITY_NAME,
			SHIELD_DURABILITY_DESC,
			SHIELD_UPGRADE_2,
			new PurchaseWithVirtualItem(        // Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            10));


// dup for looking
//		#region Private Functions
//		private static VirtualGood CreateUpgrade(VirtualItem upgradedGood, string upgradeItemId, string upgradeName, string upgradeDescription, int level, int price, bool isLast = false)
//		{
//			var prevItemId = level > 1 ? upgradeItemId + (level - 1) : null;
//			var nextItemId = isLast ? null : upgradeItemId + (level + 1);
//			
//			return new UpgradeVG(
//				upgradedGood.ItemId,            // Good Item ID
//				nextItemId,                     // Next Upgrade Item ID
//				prevItemId,                     // Previous Upgrade Item ID
//				upgradeName,                    // Name
//				upgradeDescription + level,     // Description
//				upgradeItemId + level,          // Item ID
//				new PurchaseWithVirtualItem(    // Purchase type
//			                            GALACTIC_CURRENCY.ItemId,                // Virtual item to pay with
//			                            price)                      // Payment amount
//				);
//		}
//		#endregion


		/*public UpgradeVG(string goodItemId, string nextItemId, string prevItemId, string name, string description, string itemId, PurchaseType purchaseType)
			: base(name, description, itemId, purchaseType)
		{
			this.GoodItemId = goodItemId;
			this.PrevItemId = prevItemId;
			this.NextItemId = nextItemId;
		}*/

//
////		/// <summary>
////		/// Upgrade shield durability level 2
////		/// </summary>
//		public static VirtualGood ShieldDurability2 = CreateUpgrade(
//			Shield,                         // Upgraded Item
//			SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
//			SHIELD_DURABILITY_NAME + 7,     // Name
//			SHIELD_DURABILITY_DESC,         // Decription
//			2,                              // Level
//			12);                            // Price
//		
//		/// <summary>
//		/// Upgrade shield durability level 3
//		/// </summary>
//		public static VirtualGood ShieldDurability3 = CreateUpgrade(
//			Shield,                         // Upgraded Item
//			SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
//			SHIELD_DURABILITY_NAME + 15,    // Name
//			SHIELD_DURABILITY_DESC,         // Decription
//			3,                              // Level
//			30);                            // Price
//		
//		/// <summary>
//		/// Upgrade shield durability level 4
//		/// </summary>
//		public static VirtualGood ShieldDurability4 = CreateUpgrade(
//			Shield,                         // Upgraded Item
//			SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
//			SHIELD_DURABILITY_NAME + 32,    // Name
//			SHIELD_DURABILITY_DESC,         // Decription
//			4,                              // Level
//			80);                            // Price
//		
//		/// <summary>
//		/// Upgrade shield durability level 5
//		/// </summary>
//		public static VirtualGood ShieldDurability5 = CreateUpgrade(
//			Shield,                         // Upgraded Item
//			SHIELD_DURABILITY_PRODUCT_ID,   // Item ID
//			SHIELD_DURABILITY_NAME + 100,   // Name
//			SHIELD_DURABILITY_DESC,         // Decription
//			5,                              // Level
//			200,                            // Price
//			true);                          // Last 
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
			                            GALACTIC_CURRENCY.ItemId,                // Virtual item to pay with
			                            price)                      // Payment amount
				);
		}
		#endregion
		

		
		/** Virtual Goods **/
		
		//		public static VirtualGood BLASTER_GUN = new SingleUseVG(
		//			"Blaster Gun",                                       		// name
		//			"A more powerful weapon to fend off aliens", // description
		//			"blaster_gun",                                       		// item id
		//			new PurchaseWithVirtualItem(GALACTIC_CURRENCY_ITEM_ID, 225)); // the way this virtual good is purchased
		
		public static VirtualGood PAVLOVA_GOOD = new SingleUseVG(
			"Pavlova",                                         			// name
			"Gives customers a sugar rush and they call their friends", // description
			"pavlova",                                          		// item id
			new PurchaseWithVirtualItem(GALACTIC_CURRENCY_ITEM_ID, 175)); // the way this virtual good is purchased
		
		
		public static VirtualGood CHOCLATECAKE_GOOD = new SingleUseVG(
			"Chocolate Cake",                                   		// name
			"A classic cake to maximize customer satisfaction",	 		// description
			"chocolate_cake",                                   		// item id
			new PurchaseWithVirtualItem(GALACTIC_CURRENCY_ITEM_ID, 250)); // the way this virtual good is purchased
		
		
		public static VirtualGood CREAMCUP_GOOD = new SingleUseVG(
			"Cream Cup",                                        		// name
			"Increase bakery reputation with this original pastry",   	// description
			"cream_cup",                                        		// item id
			new PurchaseWithVirtualItem(GALACTIC_CURRENCY_ITEM_ID, 50));  // the way this virtual good is purchased
		
		
		/** Virtual Categories **/
		// The muffin rush theme doesn't support categories, so we just put everything under a general category.
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory(
			"General", new List<string>(new string[] {PAVLOVA_ITEM_ID, CHOCLATECAKE_ITEM_ID, CREAMCUP_ITEM_ID })
			);
		
		
		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static VirtualGood NO_ADS_LTVG = new LifetimeVG(
			"No Ads", 														// name
			"No More Ads!",				 									// description
			"no_ads",														// item id
			new PurchaseWithMarket(NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
		
		public static VirtualGood WEAPON_SPREAD = new LifetimeVG(
			"Spreading Weapon", 														// name
			"A more powerful weapon to fend off aliens",				 	// description
			SPREAD_GUN_ITEM_ID,											// item id
			new PurchaseWithVirtualItem(GALACTIC_CURRENCY.ItemId, 100));	// the way this virtual good is purchased

		public static VirtualGood ShieldUpgrade = new LifetimeVG(
			"Shield Upgrade",                           // Name
			"Shields you from aliens",        // Description
			SHIELD_UPGRADE_1,                  // Item ID
			new PurchaseWithVirtualItem(        // Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            150)                            // Payment amount
			);
		
		
	}
	
}
