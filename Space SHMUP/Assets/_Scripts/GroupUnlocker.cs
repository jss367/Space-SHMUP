/*
* Mad Level Manager by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using MadLevelManager;

// this script should be placed in group select screen and will unlock group icons when needed
public class GroupUnlocker : MonoBehaviour {

	public Text remainingStars;
	int starsToGo = 4;
//	int finalLevel = 20;
	int[] requiredStars = new int[] {
		1, // level 2
		2, // level 3
		4, // level 4
		6, // level 5
		8, // level 6
		10, // level 7
		13, // level 8
		16, // level 9
		20, // level 10
	};


    void Start() {
//		Debug.Log (MadLevel.currentGroupName);
        string[] groups = MadLevel.GetAllLevelNames(MadLevel.Type.Level, MadLevel.defaultGroupName);

		int starsOwned = StarsUtil.CountAcquiredStars("(default)");

		int currentLevel = 2;
			foreach (int reqStars in requiredStars){
//			Debug.Log(currentLevel);
//			Debug.Log(starsOwned);
//			Debug.Log(StarsUtil.CountAvailableStars("(default)"));
			//See if you have enough stars to unlock the level
				if (starsOwned >= reqStars){
				MadLevelProfile.SetLocked("Level " + currentLevel, false);
			
//				Debug.Log(currentLevel);
				// If you don't have enough find out the minimum number to get to the next level
				} else {
				if (starsToGo > (reqStars-starsOwned)){
					starsToGo = reqStars-starsOwned;
				}

			}
			//If you have enough stars turn off the text about more stars
			if (starsOwned >= requiredStars[requiredStars.Length - 1]){
				remainingStars.enabled = false;

				// Print the number of stars needed to reach the next level
			}	else {
				if (starsToGo == 1){
					remainingStars.text = starsToGo + " more star to unlock next level!";
				} else {


				remainingStars.text = starsToGo + " more stars to unlock next level!";
				}
			}
			currentLevel++;
			}


//		MadLevel.ReloadCurrent();
    }

//    void Update() {
//    }

}