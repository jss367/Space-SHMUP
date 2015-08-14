 	/// <summary>
/// Level manager.
/// </summary>


using UnityEngine;
using System.Collections;
using MadLevelManager;

public class LevelManager : MonoBehaviour {
	
//	public int level;
	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;
	public float timeLimit;
	public string currentLevel;
	public float timer;
	public bool spawningStoppedToggle = false;
	public float startRepeating = 0.0f;
	public float repeatFreq = .1f;
	private int numWaves = 1;

	
	//
	void Start(){

		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		currentLevel = MadLevel.currentLevelName;
		InvokeRepeating ("StopSpawn", startRepeating, 2.0f);
		//Get the level number
		string[] levelSplit = currentLevel.Split (' ');
		//Concatentate level number to appropriate fireworks
		SpawnManager.instance.Mid = GameObject.Find ("LevelManager/FireworksManager/Mid" + levelSplit [1]);
		SpawnManager.instance.Mid.SetActive (true);
		SpawnManager.instance.Bass = GameObject.Find ("LevelManager/FireworksManager/Bass" + levelSplit [1]);
		SpawnManager.instance.Bass.SetActive (true);
		SpawnManager.instance.WaveText.enabled = false;
//		Debug.Log ("On level: Level" + levelSplit [1] + "Update");
		InvokeRepeating ("Level" + levelSplit [1] + "Update", startRepeating, repeatFreq);
		switch (currentLevel) {
		case "Level 1":
			numWaves = 1;
			break;
		case "Level 2":
			numWaves = 2;
			break;
		case "Level 3":
			numWaves = 3;
			break;
		case "Level 4":
			numWaves = 5;
			break;
		case "Level 5":
			numWaves = 1;
			break;
		case "Level 6":
			numWaves = 1;
			break;
		case "Level 7":
			numWaves = 1;
			break;
		case "Level 8":
			numWaves = 1;
			break;
		case "Level 9":
			numWaves = 1;
			break;
		case "Level 10":
			numWaves = 1;
			break;
		default:
			numWaves = 1;
			break;
		}

	}


	void Update(){
		timer = Time.timeSinceLevelLoad;
	}

//	void Wave(int level){
//		Debug.Log ("Wave has been called");
//			SpawnManager.instance.WaveText.text = "Wave " + level + " of " + numWaves;
//			SpawnManager.instance.WaveText.enabled = true;
//
////			yield return new WaitForSeconds(1);
//			SpawnManager.instance.WaveText.enabled = false;
//	}

	IEnumerator Wave(int level){
		Debug.Log ("Wave has been called");
		SpawnManager.instance.WaveText.text = "Wave " + level + " of " + numWaves;
		SpawnManager.instance.WaveText.enabled = true;
		yield return new WaitForSeconds(2);
		SpawnManager.instance.WaveText.enabled = false;
	}

	void StopSpawn(){
//		Debug.Log ("Main.S.playerDead " + Main.S.playerDead);
//		Debug.Log ("Main.S.playerWins " + Main.S.playerWins);
//		Debug.Log ("spawningStoppedToggle " + spawningStoppedToggle);
//		Debug.Log ("Main.S.stopSpawning: " + Main.S.stopSpawning);

//		if ((Main.S.playerDead || Main.S.playerWins) && !spawningStoppedToggle) {
		if (Main.S.stopSpawning && !spawningStoppedToggle){
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
		SpawnManager.instance.EnemySpawn2d2.SetActive (false);
		SpawnManager.instance.EnemySpawn2d3.SetActive (false);
		SpawnManager.instance.EnemySpawn3d1.SetActive (false);
		SpawnManager.instance.EnemySpawn3d2.SetActive (false);
		SpawnManager.instance.EnemySpawn4d3.SetActive (false);
		SpawnManager.instance.EnemySpawn5d1.SetActive (false);
//		SpawnManager.instance.Mid1.SetActive (false);
//		SpawnManager.instance.Mid2.SetActive (false);
//		SpawnManager.instance.Bass1.SetActive (false);
//		SpawnManager.instance.High1.SetActive (false);
		}

	}


	void Level1Update () {
		//This is for Mix_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 1.5) {
				StartCoroutine (Wave(1));
			} else if (timer < 6.5){
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
			} else if (timer < 8) {
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);

				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
//				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
			} else if (timer < 17.7) {
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
				SpawnManager.instance.EnemySpawn1d2.SetActive (false);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
			}
			else {
			
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);

			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level2Update () {
		Debug.Log (timer);
		//This is for 2_90-12Remix_48
		if (!Main.S.stopSpawning) {
			if (timer < 1.5) {
				StartCoroutine (Wave(1));
			} else if (timer < 4) {

//				Debug.Log("Coroutine has started");
				} else if (timer < 16) {
				
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
			} else if (timer < 27) {

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);

			} else if (timer < 29) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.EnemySpawn1d15.SetActive (false);
				SpawnManager.instance.AsteroidSpawn4.SetActive (false);

			} else if (timer < 30.5) {
				StartCoroutine (Wave(2));
			}else{

				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn2d15.SetActive (true);


			} 
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level3Update () {
		// This is for 90 seconds of funk
		if (!Main.S.stopSpawning) {

			if (timer < 1.5){
				StartCoroutine (Wave(1));


			} else if (timer < 23) {

				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				
			} else if (timer < 24.5) {
				StartCoroutine (Wave(2));
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.EnemySpawn1d1.SetActive (false);

			} else if (timer < 41.5) {

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
			} else if (timer < 56.5) {
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
			} else if (timer < 58) {
				StartCoroutine (Wave(3));

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);

				//			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
			} else {

				//			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
				//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
				//			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level4Update () {
		// This is for 4HighTension_48
		if (!Main.S.stopSpawning) {
			if (timer < 1.5){
				StartCoroutine (Wave(1));
			} else if (timer < 15) {
				
				//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				
			} else if (timer < 33.5) {
							
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);

				SpawnManager.instance.EnemySpawn2d15.SetActive (true);


			} else if (timer < 35) {
				StartCoroutine (Wave(2));
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.EnemySpawn2d15.SetActive (false);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 50.5) {
				//				level = 3;
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
			
		} else if (timer < 52) {
				StartCoroutine (Wave(3));
				SpawnManager.instance.EnemySpawn1d15.SetActive (false);
				SpawnManager.instance.EnemySpawn2d15.SetActive (false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (false);
				SpawnManager.instance.AsteroidSpawn8.SetActive (false);
			} else if (timer < 83.5) {
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				SpawnManager.instance.EnemySpawn2d15.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
			} else if (timer < 85) {
				StartCoroutine (Wave(4));
				SpawnManager.instance.EnemySpawn1d15.SetActive (false);
				SpawnManager.instance.EnemySpawn2d15.SetActive (false);
				SpawnManager.instance.AsteroidSpawn4.SetActive (false);
				SpawnManager.instance.AsteroidSpawn1.SetActive (false);
			} else if (timer < 101.5) {
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);

			} else if (timer < 103) {
				StartCoroutine (Wave(4));
				SpawnManager.instance.EnemySpawn1d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d2.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.AsteroidSpawn9.SetActive (false);
			}	else {
				//				level = 4;
	
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);

			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level12Update () {
		// This is for 4ChecksForFree_48
		if (!Main.S.stopSpawning) {
			if (timer < 1.5){
				StartCoroutine (Wave(1));
			} else if (timer < 15) {

//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);

				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
			
			} else if (timer < 31) {
//				level = 2;

				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
			} else if (timer < 44) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level5Update () {
		// This is for 5Sci-Fi_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 8) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
//				SpawnManager.instance.AsteroidSpawn2.SetActive (true);

			} else if (timer < 16) {
//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
			} else if (timer < 31) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn5.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 59) {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d2.SetActive (false);
			} else if (timer < 90) {
//				level = 5;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
			} else if (timer < 116) {
//				level = 6;
				SpawnManager.instance.AsteroidSpawn8.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (false);
				SpawnManager.instance.EnemySpawn2d3.SetActive (true);
			} else if (timer < 150) {
//				level = 7;
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
				SpawnManager.instance.EnemySpawn6d2.SetActive(true);
			} else {
				SpawnManager.instance.AsteroidSpawn9.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d3.SetActive (true);
				SpawnManager.instance.EnemySpawn6d2.SetActive(false);
				SpawnManager.instance.EnemySpawn10d1.SetActive (true);
				}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}

	}

	void Level6Update () {
		//This is for 6Ectoplasm2_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 21) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
			
			} else if (timer < 34) {
//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
			} else if (timer < 46) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn5.SetActive (false);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (false);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
			} else if (timer < 55) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d3.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else if (timer < 63) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn2d3.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);

			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level7Update () {
		//		This is for 7Thumpette_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 11) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
			
			} else if (timer < 22) {
//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (false);
				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
			} else if (timer < 34) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
			} else if (timer < 39) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 43) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else if (timer < 70) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.EnemySpawn3d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
			} else if (timer < 95) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn4.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (false);
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
			} else if (timer < 99) {
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn9.SetActive (false);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
			}
		
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	
	void Level8Update () {
		// This is for 8FunkyJunky_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 4) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
			} else if (timer < 36) {
//				level = 2;
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
			} else if (timer < 123) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (false);
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 140) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn6d1.SetActive (false);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	
	void Level9Update () {
		//			This is for 9Sarabande_48
		if (!Main.S.stopSpawning) {

			if (timer < 10) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
			} else if (timer < 32) {
//				level = 2;
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
			} else if (timer < 45) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn3d1.SetActive (false);
				SpawnManager.instance.EnemySpawn2d3.SetActive (true);
				SpawnManager.instance.EnemySpawn3d3.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 70) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d3.SetActive (false);
				SpawnManager.instance.EnemySpawn3d3.SetActive (false);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
			} else if (timer < 103) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
			} else if (timer < 143) {
//				level = 3;
				SpawnManager.instance.EnemySpawn6d1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (false);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	
	void Level10Update () {
		// This is for 10Roboskater_48
		////////NOT OPTIMIZED!!!!
		if (!Main.S.stopSpawning) {
			if (timer < 30) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
			
			} else if (timer < 60) {
//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 90) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				//			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
//				SpawnManager.instance.EnemySpawn10d1.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Level11Update () {
		// This is for 11Cresc_48
		if (!Main.S.stopSpawning) {
//			timer = Time.timeSinceLevelLoad;
			if (timer < 29.5) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				
			} else if (timer < 40) {
//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);

			} else if (timer < 58) {
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (false);
				SpawnManager.instance.EnemySpawn1d3.SetActive (false);
				SpawnManager.instance.EnemySpawn2d2.SetActive (false);
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (false);
			} else if (timer < 68) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn3d1.SetActive (false);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			} else if (timer < 85) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn8.SetActive (false);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (false);
			} else if (timer < 94) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
			} else if (timer < 105) {
//				level = 3;
				SpawnManager.instance.EnemySpawn4d1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (false);
				SpawnManager.instance.EnemySpawn3d2.SetActive (false);
			} else if (timer < 122) {
//				level = 3;
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
			} else {
//				level = 4;
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
				}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}
}

