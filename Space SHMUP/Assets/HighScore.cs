using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {
	static public int		score = 1000;

	void Awake() {
		// If the HighScore already exists, read it
		if (PlayerPrefs.HasKey ("SpaceSHMUPHighScore")) {
			score = PlayerPrefs.GetInt ("SpaceSHMUPHighScore");
		}
		//Assign the high score to SpaceSHMUPHighScore
		PlayerPrefs.SetInt ("SpaceSHMUPHighScore", score);
	}

	// Update is called once per frame
	void Update () {
		GUIText gt = this.GetComponent<GUIText> ();
		gt.text = "High Score: " + score;
		// Update the high score if PlayerPrefs if necessary
		if (score > PlayerPrefs.GetInt ("SpaceSHMUPHighScore")) {
//			PlayerPrefs.SetInt ("SpaceSHMUPHighScore", HighScore);
		}
	}
}
