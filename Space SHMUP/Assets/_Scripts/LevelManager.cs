using UnityEngine;
using System.Collections;
using MadLevelManager;

public class LevelManager : MonoBehaviour {
	
	public int level;
	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;
	public float timeLimit;
	public string currentLevel;
	public float timer;
	public bool spawningStoppedToggle = false;
	/*
variables that should be changed by level:
spawn breaks
victorypoints
amount of points needed to get stars
*/
	
	//
	void Start(){

		currentLevel = MadLevel.currentLevelName;

		switch (currentLevel) {
		case "Level 1":
			Level1Start ();
			break;
		case "Level 2":
			Level2Start ();
			break;
		case "Level 3":
			Level3Start ();
			break;
		case "Level 4":
			Level4Start ();
			break;
		case "Level 5":
			Level5Start ();
			break;
		case "Level 6":
			Level6Start ();
			break;
		case "Level 7":
			Level7Start ();
			break;
		case "Level 8":
			Level8Start ();
			break;	
		case "Level 9":
			Level9Start ();
			break;
		case "Level 10":
			Level10Start ();
			break;
		
		}

	}




	void Level1Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level2Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level3Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level4Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level5Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level6Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	void Level7Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;

	}
	void Level8Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
		
	}
	void Level9Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
		
	}
	void Level10Start(){
		
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
		
	}



	void Update()
	{
//		Debug.Log("LevelManager thinks playerDead is " + Main.S.playerDead);
		if (!Main.S.playerDead && !Main.S.playerWins &&!spawningStoppedToggle) {

			timer = Time.timeSinceLevelLoad;

			switch (currentLevel) {
			case "Level 1":
				Level1Update ();
				break;
			case "Level 2":
				Level2Update ();
				break;
			case "Level 3":
				Level3Update ();
				break;
			case "Level 4":
				Level4Update ();
				break;
			case "Level 5":
				Level5Update ();
				break;
			case "Level 6":
				Level6Update ();
				break;
			case "Level 7":
				Level7Update ();
				break;
			case "Level 8":
				Level8Update ();
				break;
			case "Level 9":
				Level9Update ();
				break;
			case "Level 10":
				Level10Update ();
				break;

			}
		} else {
			StopSpawn();
		}
		}

	void StopSpawn(){
		spawningStoppedToggle = true;
//		Debug.Log ("Spawning has stopped");
		SpawnManager.instance.AsteroidSpawn0.SetActive (false);
		SpawnManager.instance.AsteroidSpawn1.SetActive (false);
		SpawnManager.instance.AsteroidSpawn2.SetActive (false);
		SpawnManager.instance.AsteroidSpawn3.SetActive (false);
		SpawnManager.instance.AsteroidSpawn4.SetActive (false);
		SpawnManager.instance.AsteroidSpawn5.SetActive (false);
		SpawnManager.instance.AsteroidSpawn6.SetActive (false);
		SpawnManager.instance.AsteroidSpawn7.SetActive (false);
		SpawnManager.instance.AsteroidSpawn8.SetActive (false);
		SpawnManager.instance.AsteroidSpawn9.SetActive (false);
		SpawnManager.instance.EnemySpawn0d1.SetActive (false);
		SpawnManager.instance.EnemySpawn1d1.SetActive (false);
		SpawnManager.instance.EnemySpawn1d2.SetActive (false);
		SpawnManager.instance.EnemySpawn1d3.SetActive (false);
		SpawnManager.instance.EnemySpawn2d1.SetActive (false);
		SpawnManager.instance.EnemySpawn3d1.SetActive (false);
		SpawnManager.instance.EnemySpawn3d2.SetActive (false);
		SpawnManager.instance.EnemySpawn4d3.SetActive (false);
		SpawnManager.instance.EnemySpawn5d1.SetActive (false);


	}

	// Update is called once per frame
	void Level1Update () {


		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			//			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			//			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}
	
	void Level2Update () {

		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	void Level3Update () {

		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	void Level4Update () {

		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	void Level5Update () {
		

			if (timer < firstBreak) {
				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				
			} else if (timer >= firstBreak && timer < secondBreak) {
				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
			} else if (timer >= secondBreak && timer < thirdBreak) {
				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else {
				level = 4;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());

	}

	void Level6Update () {

		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	void Level7Update () {
		//		float timer = Time.timeSinceLevelLoad;
		
		
		//	Debug.Log("The time alive is " + main.timeAlive);
		//	Debug.Log("The first break is " + firstBreak);
		SpawnManager.instance.EnemySpawn0d1.SetActive(true);
//		SpawnManager.instance.AsteroidSpawn0.SetActive(true);
		
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	
	void Level8Update () {
		
		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	
	void Level9Update () {
		
		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}

	
	void Level10Update () {
		
		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}
}
