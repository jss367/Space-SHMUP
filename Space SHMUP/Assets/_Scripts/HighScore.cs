//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public class HighScore : MonoBehaviour {
//	static public int		score = 1000;
//	public Text highScoreText;
//
//	void Awake() {
//		// If the HighScore already exists, read it
//		if (PlayerPrefs.HasKey ("SpaceSHMUPHighScore")) {
//			score = PlayerPrefs.GetInt ("SpaceSHMUPHighScore");
//		}
//		//Assign the high score to SpaceSHMUPHighScore
//		PlayerPrefs.SetInt ("SpaceSHMUPHighScore", score);
//	}
//
//	// Update is called once per frame
//	void Update () {
////		GUIText gt = this.GetComponent<GUIText> ();
//		highScoreText.text = "High Score: " + score;
////		Debug.Log
//		// Update the high score if PlayerPrefs if necessary
//		if (score > PlayerPrefs.GetInt ("SpaceSHMUPHighScore")) {
//			PlayerPrefs.SetInt ("SpaceSHMUPHighScore", score);
//		}
//	}
//}
