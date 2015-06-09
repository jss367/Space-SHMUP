using UnityEngine;
using System.Collections;
using MadLevelManager;

public class BackgroundManager : MonoBehaviour {

//	public static BackgroundManager instance { get; private set; }

	public string currentLevel;
	public GameObject Level1Background;
	public GameObject Level2Background;
	public GameObject Level3Background;
	public GameObject Level4Background;
	public GameObject Level5Background;
	public GameObject Level6Background;
	public GameObject Level7Background;
	public GameObject Level8Background;
	public GameObject Level9Background;
	public GameObject Level10Background;

	void Awake(){
//		instance = this;
//		Level1Background = GameObject.Find("BackgroundManager/Level1Background");	
//		Level1Background = GameObject.Find("Level1Background");
//		Level3Background = GameObject.Find("Level3Background");
	}

	void Start(){
//		Debug.Log ("Level1Background is " + Level1Background);

		currentLevel = MadLevel.currentLevelName;

		
		switch (currentLevel) {
		case "Level 1":
//			Level1Background.SetActive (true);

			break;
		case "Level 2":
			Level2Background.SetActive (true);
			break;
		case "Level 3":
			Level3Background.SetActive (true);
			break;
		case "Level 4":
			Level4Background.SetActive (true);
			break;
		case "Level 5":
			Level5Background.SetActive (true);
			break;
		case "Level 6":
			Level2Background.SetActive (true);
			break;
		case "Level 7":
			Level2Background.SetActive (true);
			break;
		case "Level 8":
			Level2Background.SetActive (true);
			break;
		case "Level 9":
			Level2Background.SetActive (true);
			break;
		case "Level 10":
			Level2Background.SetActive (true);
			break;
			
		}
		
	}
	

}
