using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPreferences : MonoBehaviour {

	public Toggle testToggleStyle;

	void Start(){
		testToggleStyle.onValueChanged.AddListener (SetPref);
		PlayerPrefs.SetInt ("AutoShoot", 1);
	}

			public void SetPref(bool autoShoot){
		Debug.Log (autoShoot);
		if (autoShoot) {
			PlayerPrefs.SetInt ("AutoShoot", 1);
		}
		if (!autoShoot) {
			PlayerPrefs.SetInt ("AutoShoot", 0);
		}
	}


}
