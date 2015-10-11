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

namespace Soomla.Store.Example
{
	
	/// <summary>
	/// This class defines our game's economy, which includes virtual goods, virtual currencies
	/// and currency packs, virtual categories
	/// </summary>
	public class GalacticAssets : IStoreAssets
	{
		
		/// <summary>
		/// see parent.
		/// </summary>
		public int GetVersion ()
		{
			return 1;
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrency[] GetCurrencies ()
		{
			return new VirtualCurrency[]{GALACTIC_CURRENCY};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualGood[] GetGoods ()
		{
			return new  VirtualGood[] {
				WEAPON_BLASTER,
				WEAPON_SPREAD,
				BaseSpeed,
				BaseShield,
				DoubleBlaster,
				MISSILE_LAUNCHER,
				BAZOOKA_LAUNCHER
			};//,Shield, ShieldUpgrade1, //, NO_ADS_LTVG};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCurrencyPack[] GetCurrencyPacks ()
		{
			return new VirtualCurrencyPack[] {
				THOUSAND_COIN_PACK,
				TEN_THOUSAND_COIN_PACK,
				HUNDRED_THOUSAND_COIN_PACK
			};
		}
		
		/// <summary>
		/// see parent.
		/// </summary>
		public VirtualCategory[] GetCategories ()
		{
			return new VirtualCategory[] {
				GENERAL_CATEGORY,
				WEAPON_CATEGORY,
				LAUNCHER_CATEGORY
			};
		}


		
		/** Static Final Members **/
		
		public const string GALACTIC_CURRENCY_ITEM_ID = "galactic_currency"; //add the reverse domain?
		// com.gleeza.galacticbeats.galactic_currency?
		
		public const string TENMUFF_PACK_PRODUCT_ID = "android.test.refunded";
		public const string FIFTYMUFF_PACK_PRODUCT_ID = "android.test.canceled";
		public const string FOURHUNDMUFF_PACK_PRODUCT_ID = "android.test.purchased";
		public const string THOUSANDMUFF_PACK_PRODUCT_ID = "2500_pack";
		public const string CHOCLATECAKE_ITEM_ID = "chocolate_cake";
		public const string CREAMCUP_ITEM_ID = "cream_cup";
		public const string NO_ADS_LIFETIME_PRODUCT_ID = "no_ads";


//		public const string SHIELD_UPGRADE_2 = "shield_2";
//		public const string SHIELD_DURABILITY_PRODUCT_ID = "shield_dur_";
//		public const string SHIELD_DURABILITY_NAME = "Durability ";
//		public const string SHIELD_DURABILITY_DESC = "Increases shield durability to ";
//		public const string SHIELD_PRODUCT_ID = "hero_shield";

		
		
		/** Virtual Currencies **/
		
		public static VirtualCurrency GALACTIC_CURRENCY = new VirtualCurrency (
			"Coin",										// name
			"Currency in the Cosmos",						// description
			Constants.GALACTIC_CURRENCY_ITEM_ID							// item id
		);
		
		
		/** Virtual Currency Packs **/
		
		public static VirtualCurrencyPack THOUSAND_COIN_PACK = new VirtualCurrencyPack (
			"1000 Coins",                                   // name
			"Coins for use in store",                       // description
			Constants.THOUSAND_COIN_PACK_ITEM_ID,                                   // item id
			1000,												// number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket (Constants.THOUSAND_COIN_PACK_ITEM_ID, 0.99)
		);
		public static VirtualCurrencyPack TEN_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
			"10000 Coins",                                   // name
			"Coins for use in store",                 // description
			Constants.TEN_THOUSAND_COIN_PACK_ITEM_ID,                                   // item id
			10000,                                             // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket (Constants.TEN_THOUSAND_COIN_PACK_ITEM_ID, 1.99)
		);
		public static VirtualCurrencyPack HUNDRED_THOUSAND_COIN_PACK = new VirtualCurrencyPack (
			"100000 Coins",              	 		            // name
			"Coins for use in store",                 	// description
			Constants.HUNDRED_THOUSAND_COIN_PACK_ITEM_ID,                                  // item id
			100000,                                            // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket (Constants.HUNDRED_THOUSAND_COIN_PACK_ITEM_ID, 2.50)
		);
		public static VirtualCurrencyPack COCONUT = new VirtualCurrencyPack (
			"A coconut",                                  // name
			"Test purchase of an item",                 	// description
			"muffins_400",                                  // item id
			500,                                            // number of currencies in the pack
			GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
			new PurchaseWithMarket (FOURHUNDMUFF_PACK_PRODUCT_ID, 4.99)
		);

		#region Equipables
		/// <summary>
		/// An equipable weapon that can be purchased for 7 coins.
		/// </summary>

		public static VirtualGood WEAPON_SPREAD = new EquippableVG (
			EquippableVG.EquippingModel.CATEGORY,
			"Spread Weapon", 														// name
			"Shoots three spreading bullets",						 	// description
			Constants.SPREAD_WEAPON_ITEM_ID,											// item id
			new PurchaseWithVirtualItem (GALACTIC_CURRENCY.ItemId, 50000));
		public static VirtualGood WEAPON_BLASTER = new EquippableVG (
			EquippableVG.EquippingModel.CATEGORY,
			"Blaster", 														// name
			"The weapon you start with",				 	// description
			Constants.BLASTER_WEAPON_ITEM_ID,											// item id
			new PurchaseWithVirtualItem (GALACTIC_CURRENCY.ItemId, 0));
		public static VirtualGood MISSILE_LAUNCHER = new EquippableVG (
			EquippableVG.EquippingModel.CATEGORY,
			"Missile Launcher",                           // Name
			"Launch devastating missiles",        // Description
			Constants.MISSILE_LAUNCHER_ITEM_ID,                  // Item ID
			new PurchaseWithVirtualItem (GALACTIC_CURRENCY.ItemId, 15000));
		public static VirtualGood BAZOOKA_LAUNCHER = new EquippableVG (
			EquippableVG.EquippingModel.CATEGORY,
			"Bazooka Launcher", 														// name
			"Shoots two bazookas",				 	// description
			Constants.BAZOOKA_LAUNCHER_ITEM_ID,											// item id
			new PurchaseWithVirtualItem (GALACTIC_CURRENCY.ItemId, 25000));


		/// <summary>
		/// An equipable weapon that can be purchased for 7 coins.
		/// </summary>
		public static VirtualGood PlasmaGun = new EquippableVG (
			EquippableVG.EquippingModel.LOCAL,  // Equipping Model
			"Plasma Gun",                       // Name
			"Spray & Pray",                     // Description
			"plasma_gun",                       // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            7));                           // Payment amount
		
//		/// <summary>
//		/// An equipable weapon that can be purchased for 7 coins.
//		/// </summary>
//		public static VirtualGood SoomlaBotSidekick = new EquippableVG(
//			EquippableVG.EquippingModel.LOCAL,  // Equipping Model
//			"SoomlaBot Sidekick",               // Name
//			"The best sidekick EVER!",          // Description
//			"soomlabot_sidekick",               // Item ID
//			new PurchaseWithVirtualItem(        // Purchase type
//		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
//		                            7));                           // Payment amount

		/// <summary>
		/// A playable character that can be purchased for 27 coins.
		/// </summary>
		public static VirtualGood Character1 = new EquippableVG (
			EquippableVG.EquippingModel.GLOBAL, // Equipping Model
			"Character 1",                      // Name
			"Character #1",                     // Description
			"character_1",                      // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            0));                            // Payment amount 
		
		/// <summary>
		/// A playable character that can be purchased for 27 coins.
		/// </summary>
		public static VirtualGood Character2 = new EquippableVG (
			EquippableVG.EquippingModel.GLOBAL, // Equipping Model
			"Character 2",                      // Name
			"Character #2",                     // Description
			"character_2",                      // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            1000));                         // Payment amount 
		#endregion

		#region Upgrades


//		public static VirtualGood ShieldUpgrade1 = new UpgradeVG(
//			Constants.SHIELD_ITEM_ID,	// Item ID of the associated good that is being upgraded
//			null,						// Item ID of the next upgrade good
//			null,						// Item ID of the previous upgrade good
//			"Shield Upgrade",			// Name
//			"Shield takes two hits",	// Description
//			Constants.SHIELD_UPGRADE_1,	// Item ID
//			new PurchaseWithVirtualItem(        // Purchase type
//		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
//		                            8000));

		public static VirtualGood BaseSpeed = new LifetimeVG (
			"Speed",                           // Name
			"Makes you move faster",        // Description
			Constants.SPEED_ITEM_ID,                  // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            5000)                            // Payment amount
		);
		public static VirtualGood DoubleBlaster = new LifetimeVG (
			"Blaster Double Damage",                           // Name
			"Doubles the amount of damage from the blaster",        // Description
			Constants.DOUBLE_BLASTER_WEAPON_ITEM_ID,                  // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            15000)                            // Payment amount
		);
		public static VirtualGood BaseShield = new LifetimeVG (
			"Shield Upgrade",                           // Name
			"Doubles the health of your shield",        // Description
			Constants.BASESHIELD_ITEM_ID,                  // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            10000)                            // Payment amount
		);
		public static VirtualGood QuickFire = new LifetimeVG (
			"Faster",                           // Name
			"Doubles the health of your shield",        // Description
			Constants.BASESHIELD_ITEM_ID,                  // Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            0)                            // Payment amount
		);
		
//		public static VirtualGood Shield = new LifetimeVG(
//			"Shield",                           // Name
//			"Shields you from aliens and asteroids",        // Description
//			Constants.SHIELD_ITEM_ID,                  // Item ID
//			new PurchaseWithVirtualItem(        // Purchase type
//		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
//		                            0)                            // Payment amount
//			);
		
		
		public static VirtualGood SpeedUpgrade1 = new UpgradeVG (
			Constants.SPEED_ITEM_ID,	// Item ID of the associated good that is being upgraded
			Constants.SPEED_UPGRADE_2,	// Item ID of the next upgrade good
			null,					// Item ID of the previous upgrade good
			"Speed Upgrade",			// Name
			"Makes ship faster",	// Description
			Constants.SPEED_UPGRADE_1,	// Item ID
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            8000));
		public static VirtualGood SpeedUpgrade2 = new UpgradeVG (
			Constants.SPEED_ITEM_ID,
			null,
			Constants.SPEED_UPGRADE_1,
			"Shield Upgrade 2",
			"Upgrade does nothing",
			Constants.SPEED_UPGRADE_2,
			new PurchaseWithVirtualItem (// Purchase type
		                            GALACTIC_CURRENCY.ItemId,                    // Virtual item to pay with
		                            10000));



		#endregion

		#region Private Functions
		private static VirtualGood CreateUpgrade (VirtualItem upgradedGood, string upgradeItemId, string upgradeName, string upgradeDescription, int level, int price, bool isLast = false)
		{
			var prevItemId = level > 1 ? upgradeItemId + (level - 1) : null;
			var nextItemId = isLast ? null : upgradeItemId + (level + 1);
			
			return new UpgradeVG (
				upgradedGood.ItemId,            // Good Item ID
				nextItemId,                     // Next Upgrade Item ID
				prevItemId,                     // Previous Upgrade Item ID
				upgradeName,                    // Name
				upgradeDescription + level,     // Description
				upgradeItemId + level,          // Item ID
				new PurchaseWithVirtualItem (// Purchase type
			                            GALACTIC_CURRENCY.ItemId,                // Virtual item to pay with
			                            price)                      // Payment amount
			);
		}
		#endregion
		

		/** Virtual Categories **/
		// The muffin rush theme doesn't support categories, so we just put everything under a general category.
		public static VirtualCategory GENERAL_CATEGORY = new VirtualCategory (
			"General", new List<string> (new string[] {
			CHOCLATECAKE_ITEM_ID,
			CREAMCUP_ITEM_ID
		})
		);
		public static VirtualCategory WEAPON_CATEGORY = new VirtualCategory (
			"Weapons", new List<string> (new string[] {
			Constants.BLASTER_WEAPON_ITEM_ID,
			Constants.SPREAD_WEAPON_ITEM_ID
		})
		);
		public static VirtualCategory LAUNCHER_CATEGORY = new VirtualCategory (
			"Launchers", new List<string> (new string[] {
			Constants.MISSILE_LAUNCHER_ITEM_ID,
			Constants.BAZOOKA_LAUNCHER_ITEM_ID
		})
		);
		
		
		/** LifeTimeVGs **/
		// Note: create non-consumable items using LifeTimeVG with PuchaseType of PurchaseWithMarket
		public static VirtualGood NO_ADS_LTVG = new LifetimeVG (
			"No Ads", 														// name
			"No More Ads!",				 									// description
			"no_ads",														// item id
			new PurchaseWithMarket (NO_ADS_LIFETIME_PRODUCT_ID, 0.99));	// the way this virtual good is purchased
		


		
		
	}
	
}
