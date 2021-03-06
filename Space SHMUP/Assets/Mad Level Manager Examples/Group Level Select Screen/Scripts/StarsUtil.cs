/*
* Mad Level Manager by Mad Pixel Machine
* http://www.madpixelmachine.com
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MadLevelManager;

public class StarsUtil : MonoBehaviour {

    #region Public Static Methods

    public static int CountAvailableStars(string groupName) {
        var levels = MadLevel.GetAllLevelNames(MadLevel.Type.Level, groupName);
        return levels.Length * 3;
    }

    public static int CountAcquiredStars(string groupName) {
        var levels = MadLevel.GetAllLevelNames(MadLevel.Type.Level, groupName);
        int collectedStars = 0;
        for (int i = 0; i < levels.Length; ++i) {
            if (MadLevelProfile.GetLevelBoolean(levels[i], "earth_1")) collectedStars++;
			if (MadLevelProfile.GetLevelBoolean(levels[i], "earth_2")) collectedStars++;
			if (MadLevelProfile.GetLevelBoolean(levels[i], "earth_3")) collectedStars++;
        }

        return collectedStars;
    }

    #endregion

}