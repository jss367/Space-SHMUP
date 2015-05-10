using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using Soomla.Profile;
using Soomla.Levelup;
using Soomla.Highway;

using System.Collections;
using System.Collections.Generic;

//namespace Soomla.Store.Example {

public class WorldManager {

		
		public World CreateInitialWorld() {
			
			/** Scores **/
			
			Score pointScore = new Score(
				"pointScore_ID",                            // ID
				"Point Score",                              // Name
				true                                        // Higher is better
				);
			
			Score bananaScore = new Score(
				"bananaScore_ID",                           // ID  
				"Banana Score",                             // Name
				true                                        // Higher is better
				);
			
			/** Rewards **/
			
			Reward medalReward = new BadgeReward(
				"medalReward_ID",                           // ID
				"Medal Reward"                              // Name
				);
			
			Reward jungTwoHundCoinReward = new VirtualItemReward(
				"jungTwoHundCoinReward_ID",                 // ID  
				"10 Coin Reward",                          // Name  
			Soomla.Store.Example.GalacticAssets.TEN_COIN_PACK.ID,         // Associated virtual item
				1                                           // Amount
				);
			
			Reward desTwoHundCoinReward = new VirtualItemReward(
				"desTwoHundCoinReward_ID",                  // ID  
				"10 Coin Reward",                          // Name  
			Soomla.Store.Example.GalacticAssets.TEN_COIN_PACK.ID,         // Associated virtual item
				1                                           // Amount
				);
			
			Reward fiveHundCoinReward = new VirtualItemReward(
				"fiveHundCoinReward_ID",                    // ID  
				"500 Coin Reward",                          // Name  
				Soomla.Store.Example.GalacticAssets.FIVEHUND_COIN_PACK.ID,        // Associated virtual item
				1                                           // Amount
				);
			
			/** Missions **/
			
			Mission pointMission = new RecordMission(
				"pointMission_ID",                          // ID
				"Point Mission",                            // Name
				new List<Reward>(){medalReward},            // Rewards
			pointScore.ID,                              // Associated score
			3                                           // Desired record
			);
			
			Mission coconutMission = new BalanceMission(
				"coconutMission_ID",                        // ID
				"Coconut Mission",                          // Name
				new List<Reward>() {fiveHundCoinReward},    // Rewards
			Soomla.Store.Example.GalacticAssets.COCONUT.ID,                   // Associated virtual item
			5                                           // Desired balance
			);
			coconutMission.Schedule = Schedule.AnyTimeOnce();
			
			Mission likeMission = new SocialLikeMission(
				"likeMission_ID",                           // ID
				"Like Mission",                             // Name
				new List<Reward>(){medalReward},            // Rewards
			Soomla.Profile.Provider.FACEBOOK,           // Social provider
			"pageToLike"                                // Page to like
			);
			likeMission.Schedule = Schedule.AnyTimeOnce();
			
			Mission statusMissionJungle = new SocialStatusMission(
				"statusMissionJungle_ID",                   // ID
				"Status Mission Jungle",                    // Name
				new List<Reward>(){jungTwoHundCoinReward},  // Rewards
			Soomla.Profile.Provider.FACEBOOK,           // Social provider
			"JUNGLE World completed!"                   // Status to post
			);
			
			Mission statusMissionDesert = new SocialStatusMission(
				"statusMissionDesert_ID",                   // ID
				"Status Mission Desert",                    // Name
				new List<Reward>(){desTwoHundCoinReward},   // Rewards
			Soomla.Profile.Provider.FACEBOOK,           // Social provider
			"DESERT World completed!"                   // Status to post
			);
			
			/** Worlds **/
			
			// Initial world
			World mainWorld = new World(
				"main_world", null, null, null,
				new List<Mission>(){coconutMission, likeMission}
			);
			
			World jungleWorld = new World(
				"jungleWorld_ID",                           // ID
				null, null, null,                           // Gate, Inner worlds, Scores
				new List<Mission>(){statusMissionJungle}    // Missions
			);
			
			World desertWorld = new World(
				"desertWorld_ID",                           // ID
				null, null, null,                           // Gate, Inner worlds, Scores
				new List<Mission>(){statusMissionDesert}    // Missions
			);
//			
//			/** Levels **/
//			
			jungleWorld.BatchAddLevelsWithTemplates(
				3,                                          // Number of levels
				null,                                       // Gate template
				new List<Score>(){bananaScore},             // Score templates
			null                                        // Mission templates
			);
			
			desertWorld.BatchAddLevelsWithTemplates(
				3,                                          // Number of levels
				null,                                       // Gate template
				new List<Score>(){bananaScore},             // Score templates
			null                                        // Mission templates
			);
			
			// Bind pointMission to the first level of the first world (jungleWorld)
			Level firstLevel = (Level)jungleWorld.GetInnerWorldAt(0);
			firstLevel.AddMission(pointMission);
			
			/** Gates **/
			
			// Once users finish Jungle world, they can continue to Desert world.
			Gate desertGate = new WorldCompletionGate(
				"desertGate_ID",                            // Item ID
				jungleWorld.ID                              // Associated world ID
				);
			desertWorld.Gate = desertGate;
//			
//			// See private function below
//			AddGatesToWorld(jungleWorld);
//			AddGatesToWorld(desertWorld);
			
			
			/** Add Worlds to Initial World **/
			mainWorld.AddInnerWorld(jungleWorld);
			mainWorld.AddInnerWorld(desertWorld);
			
			return mainWorld;
			
		}

			
//		private void AddGatesToWorld(World world) {
//			
//			// Iterate over all levels of the given world
//			for (int i = 1; i < world.InnerWorldsMap.Count; i++) {
//				
//				Level previousLevel = (Level)world.GetInnerWorldAt(i - 1);
//				Level currentLevel = (Level)world.GetInnerWorldAt(i);
//				Score bananaScoreOfPrevLevel = previousLevel.Scores.Values.ElementAt(1);
//				
////				 The associated score of this Level's RecordGate is the bananaScore
////				 of the previous level.
//				Gate bananaGate = new RecordGate(
//					"bananaGate_" + world.ID + "_level_" + i.ToString(),              // ID
//					bananaScoreOfPrevLevel.ID,                          // Associated Score
//					2                                                     // Desired record
//					);
//				
//				// The associated world of this Level's WorldCompletionGate is the
//				// previous level.
//				Gate prevLevelCompletionGate = new WorldCompletionGate(
//					"prevLevelCompletionGate_" + world.ID + "_level_" + i.ToString(), // ID
//					previousLevel.ID                                    // Associated World
//					);
//				
//				// The gates in this Level's GatesListAND are the 2 gates declared above.
//				currentLevel.Gate = new GatesListAND(
//					"gate_" + world.ID + "_level_" + i.ToString(),                    // ID
//					new List<Gate>(){prevLevelCompletionGate, bananaGate}  // List of Gates
//				);
//			}
//		}
	}
//}