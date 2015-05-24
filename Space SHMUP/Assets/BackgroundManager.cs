using UnityEngine;
using System.Collections;
using MadLevelManager;

public class BackgroundManager : MonoBehaviour {

	public string currentLevel;
	public GameObject Level1Background;
	public GameObject Level2Background;
	public GameObject Level3Background;
	public GameObject Level4Background;

	void Start(){
		
		currentLevel = MadLevel.currentLevelName;

		
		switch (currentLevel) {
		case "Level 1":
			Level1Background.SetActive (true);

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
			Level2Background.SetActive (true);
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
