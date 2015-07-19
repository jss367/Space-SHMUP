using UnityEngine;
using System.Collections;
using MadLevelManager;

public class GroupUnlocker : MonoBehaviour {

	// Use this for initialization
	     void Start() {
		         string[] groups = MadLevel.GetAllLevelNames(MadLevel.Type.Level, MadLevel.defaultGroupName);
		 
		         for (int i = 1; i < groups.Length; ++i) {
			             string prevGroup = groups[i - 1];
			             string group = groups[i];
			 
				             int acquired = StarsUtil.CountAcquiredStars(prevGroup);
			             if (acquired >= 6) {
				                 if (MadLevelProfile.IsLocked(group)) {
					                     MadLevelProfile.SetLocked(group, false);
					                     MadLevel.ReloadCurrent();
					                 }
				             }
			         }
		     }
	
	// Update is called once per frame
	void Update () {
	
	}
}
