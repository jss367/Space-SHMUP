using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using Soomla.Profile;
using Soomla.Levelup;
using Soomla.Highway;

public class SoomlaManager : MonoBehaviour {


		//
		// Utility method for creating the game's worlds
		// and levels hierarchy
		//
		private World createMainWorld() {
			World worldA = new World("world_a");
			World worldB = new World("world_b");
			
//			Reward coinReward = new VirtualItemReward(
//				"coinReward",                       // ID
//				"100 Coins",                        // Name
//				COIN_CURRENCY.ID,                   // Associated item ID
//				100                                 // Amount
//				);
			
//			Mission likeMission = new SocialLikeMission(
//				"likeMission",                      // ID
//				"Like Mission",                     // Name
//				new List<Reward>(){coinReward},     // Reward
//			Soomla.Profile.Provider.FACEBOOK,   // Social Provider
//			"[page name]"                       // Page to "Like"
//			);
			
//			// Add 10 levels to each world
//			worldA.BatchAddLevelsWithTemplates(10, null,
//			                                   null, new List<Mission>(){likeMission});
//			worldB.BatchAddLevelsWithTemplates(10, null,
//			                                   null, new List<Mission>(){likeMission});
			
			// Create a world that will contain all worlds of the game
			World mainWorld = new World("main_world");
			mainWorld.InnerWorldsMap.Add(worldA.ID, worldA);
			mainWorld.InnerWorldsMap.Add(worldB.ID, worldB);
			
			return mainWorld;
		}
		
		//
		// Various event handling methods
		//
//		public void onGoodBalanceChanged(VirtualGood good, int balance, int amountAdded) {
//			SoomlaUtils.LogDebug("TAG", good.ID + " now has a balance of " + balance);
//		}
//		public static void onLoginFinished(UserProfile userProfileJson, string payload){
//			SoomlaUtils.LogDebug("TAG", "Logged in as: " + UserProfile.toJSONObject().print());
//		}
//		public void onLevelStarted(Level level) {
//			SoomlaUtils.LogDebug("TAG", "Level started: " + level.toJSONObject().print());
//		}
		
	
	void Awake(){
		SoomlaHighway.Initialize ();
	}
	

		
		//
		// Initialize all of SOOMLA's modules
		//
		void Start () {
				
				// Setup event handlers
//			StoreEvents.OnGoodBalanceChanged += onGoodBalanceChanged;
//			ProfileEvents.OnLoginFinished += onLoginFinished;
//			LevelUpEvents.OnLevelStarted += onLevelStarted;
			
			SoomlaHighway.Initialize ();
//			SoomlaStore.Initialize(new GalacticBeatsAssets());
			SoomlaProfile.Initialize();
//			SoomlaLevelup.Initialize(createMainWorld());
		}
	}

