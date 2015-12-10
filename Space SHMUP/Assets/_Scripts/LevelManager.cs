/// <summary>
/// Level manager.
/// </summary>
using UnityEngine;
using System.Collections;
using MadLevelManager;

public class LevelManager : MonoBehaviour
{
	
//	public int level;
//	public float firstBreak;
//	public float secondBreak;
//	public float thirdBreak;
	private float timeLimit;
//	private string currentLevel;
	private string musicLevel;
	private float timer;
	public bool spawningStoppedToggle = false;
	public float startRepeating = 0.0f;
	public float repeatFreq = .1f;
	private int numWaves = 1;
	private GameObject[] AsteroidManagers;
	private GameObject[] EnemyManagers;
	public float timeBetweenWaves = 5;
	private bool deadPeriod;
	public float waveOne = 2;
	public float waveTwo = 999;
	public float waveThree = 999;
	public float waveFour = 999;
	public float waveFive = 999;
	public float waveSix = 999;
	public float waveSeven = 999;
	public float waveEight = 999;
	public bool waveNotReady = false;

	//
	void Start ()
	{

		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		musicLevel = GameObject.Find ("Beat").GetComponent<AudioSource> ().clip.ToString ();
		string[] levelSplit = musicLevel.Split (' ');
		musicLevel = levelSplit [0];
//		currentLevel = MadLevel.currentLevelName;
//		InvokeRepeating ("StopSpawn", startRepeating, 2.0f);	
		//Get the level number
//		string[] currLevelSplit = currentLevel.Split (' ');
		//Concatentate level number to appropriate fireworks
		SpawnManager.instance.Mid = GameObject.Find ("LevelManager/FireworksManager/Mid" + musicLevel);
		//Debug.Log ("LevelManager/FireworksManager/Mid" + musicLevel);
		SpawnManager.instance.Mid.SetActive (true);
		SpawnManager.instance.Bass = GameObject.Find ("LevelManager/FireworksManager/Bass" + musicLevel);
		SpawnManager.instance.Bass.SetActive (true);
		SpawnManager.instance.WaveText.enabled = false;
//		Debug.Log ("On level: Level" + levelSplit [1] + "Update");
		InvokeRepeating (musicLevel, startRepeating, repeatFreq);
		switch (musicLevel) {
		case "First30":
			numWaves = 1;
			waveOne = 2;
			break;
		case "Mix21":
			numWaves = 1;
			waveOne = 2;
			break;
		case "Ninety12Remix55":
			numWaves = 2;
			waveOne = 2;
			waveTwo = 32;
			break;
		case "JazzyFrenchy145":
			numWaves = 3;
			waveOne = 2;
			waveTwo = 35;
			waveThree = 67;
			break;
		case "Ectoplasm2_125":
			//Ectoplasm
			numWaves = 3;
			waveOne = 2;
			waveTwo = 39;
			waveThree = 57;
			break;
		case "LoveYoursHands200":
			numWaves = 4;
			waveOne = 2;
			waveTwo = 32;
			waveThree = 67;
			waveFour = 91;
			break;
		case "ChecksForFree100":
			numWaves = 2;
			waveOne = 2;
			waveTwo = 31;
			break;
		case "SciFi230":
			numWaves = 5;
			waveOne = 15.5f;
			waveTwo = 32;
			waveThree = 60;
			waveFour = 90;
			waveFive = 115.5f;
			break;
		case "HighTension210":
			numWaves = 5;
			waveOne = 2;
			waveTwo = 35.5f;
			waveThree = 52;
			waveFour = 85;
			waveFive = 103;
			break;
		case "Sarabande230":
			//Sarabande
			numWaves = 3;
			waveOne = 2;
			waveTwo = 39;
			waveThree = 110;
			break;
		case "NinetySecondsOfFunk130":
			// 90 seconds of funk
			numWaves = 3;
			waveOne = 2;
			waveTwo = 34;
			waveThree = 66;
			break;
		case "Epic":
			numWaves = 5;
			waveOne = 2;
			waveTwo = 35;
			waveThree = 63;
			waveFour = 109;
			waveFive = 144;
			break;
		case "ThumpetteMini125":
			// Thumpette, at least for now
			numWaves = 2;
			waveOne = 2;
			waveTwo = 43;
			break;
		case "Cresc230":
			numWaves = 5;
			waveOne = 2;
			waveTwo = 30;
			waveThree = 58;
			waveFour = 85;
			waveFive = 120;
			break;
		default:
			numWaves = 17;
			waveOne = timeLimit / 4;
			///etc.
			break;
		}

	}

	void Update ()
	{
		timer = Time.timeSinceLevelLoad;
//		Debug.Log (timer);
//		Debug.Log (deadPeriod);
//		Debug.Log (Main.S.stopSpawning);
		if (Main.S.stopSpawning) {
			StopSpawn ();
		}
	}

	void StopSpawn ()
	{
//		Debug.Log ("Spawning has stopped");
//		Debug.Log ("Main.S.playerDead " + Main.S.playerDead);
//		Debug.Log ("Main.S.playerWins " + Main.S.playerWins);
//		Debug.Log ("spawningStoppedToggle " + spawningStoppedToggle);
//		Debug.Log ("Main.S.stopSpawning: " + Main.S.stopSpawning);
		AsteroidManagers = GameObject.FindGameObjectsWithTag ("AsteroidManager");
		EnemyManagers = GameObject.FindGameObjectsWithTag ("EnemyManager");
		foreach (GameObject obj in AsteroidManagers) {
			obj.SetActive (false);
		}
		foreach (GameObject obj in EnemyManagers) {
			obj.SetActive (false);
		}
//		if ((Main.S.playerDead || Main.S.playerWins) && !spawningStoppedToggle) {
		if (Main.S.stopSpawning && !spawningStoppedToggle) {
			spawningStoppedToggle = true;
//		Debug.Log ("Spawning has stopped");

		}

	}

	IEnumerator Wave (int level)
	{
		waveNotReady = true;
//			Debug.Log ("Started Coroutine");
		deadPeriod = true;
//	delthis	//Wait .2 seconds after deadPeriod has been declared to finished all activations
//		yield return new WaitForSeconds (.2f);
		StopSpawn ();
		yield return new WaitForSeconds (timeBetweenWaves - 1);
		SpawnManager.instance.WaveText.text = "Wave " + level + " of " + numWaves;
		SpawnManager.instance.WaveText.enabled = true;
		yield return new WaitForSeconds (1f);	
		deadPeriod = false;
		yield return new WaitForSeconds (1f);
		SpawnManager.instance.WaveText.enabled = false;
		waveNotReady = false;
	}

	void CheckForNextWave (float time, int level)
	{
		if ((timer > time - timeBetweenWaves) && !waveNotReady) {
			StartCoroutine (Wave (level));
		}
	}

	void First30 ()
	{
		
		//This is for First30
		if (!Main.S.stopSpawning && !deadPeriod) {
			
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
			} else {//if (timer < 25){
				
				SpawnManager.instance.AsteroidSpawnFirst.SetActive (true);
				SpawnManager.instance.EnemySpawn1dFirst.SetActive (true);
//			} 	else {
//				//				Debug.Log(Main.S.stopSpawning);
//				SpawnManager.instance.AsteroidSpawn1.SetActive (false);
//				SpawnManager.instance.EnemySpawn1d1.SetActive (false);
//				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
			}
			
		}
	}

	void Mix21 ()
	{

		//This is for Mix21
		if (!Main.S.stopSpawning && !deadPeriod) {

			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
			} else if (timer < 19) {

				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
			} else {
//				Debug.Log(Main.S.stopSpawning);
				SpawnManager.instance.AsteroidSpawn1.SetActive (false);
				SpawnManager.instance.EnemySpawn1d1.SetActive (false);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
			}

		}
	}

	void Ninety12Remix55 ()
	{
//		InvokeRepeating ("CheckForNextWave", 0, .5f);
		//This is for 2_90-12Remix_48
		if (!Main.S.stopSpawning && !deadPeriod) {

			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);


			} else if (timer < waveTwo) {
			
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				//CheckForNextWave must go below the spawn manager
				CheckForNextWave (waveTwo, 2);
			} else {

				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn15d2.SetActive (true);

			}

		}
	}

	void JazzyFrenchy145 ()
	{
		//		InvokeRepeating ("CheckForNextWave", 0, .5f);
		//This is for 2_90-12Remix_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
				
				
			} else if (timer < waveTwo) {
				
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				//CheckForNextWave must go below the spawn manager
				CheckForNextWave (waveTwo, 2);
			} else  if (timer < waveThree) {
				
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn15d2.SetActive (true);
				
			} else {
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				SpawnManager.instance.EnemySpawn15d2.SetActive (true);
			}
			
		}
	}

	void Ectoplasm2_125 ()
	{
		// This is for 6Ectoplasm2_125_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);

			} else if (timer < waveTwo) {

				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
//				SpawnManager.instance.EnemySpawn1dtest.SetActive (true);
				SpawnManager.instance.EnemySpawn15d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				CheckForNextWave (waveTwo, 2);

			} else if (timer < waveThree) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.EnemySpawn2d15.SetActive (true);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
//				SpawnManager.instance.EnemySpawn1d15.SetActive(true);
//				SpawnManager.instance.EnemySpawn1dtest.SetActive (true);

				CheckForNextWave (waveThree, 3);
			} else if (timer < 66) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.EnemySpawn2d15.SetActive (true);
				SpawnManager.instance.EnemySpawn15d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn3.SetActive (true);
				SpawnManager.instance.AsteroidSpawn31.SetActive (true);
			} else {
				SpawnManager.instance.AsteroidSpawn3.SetActive (false);
				SpawnManager.instance.AsteroidSpawn31.SetActive (false);
				SpawnManager.instance.EnemySpawn2d15.SetActive (false);
				SpawnManager.instance.EnemySpawn15d1.SetActive (false);
				SpawnManager.instance.EnemySpawn2u3.SetActive (true);
				SpawnManager.instance.EnemySpawn15u3.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}
	
	void ChecksForFree100 ()
	{
		// This is for ChecksForFree_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);

			} else if (timer < waveTwo) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
//				SpawnManager.instance.EnemySpawn1dtest.SetActive (true);
				CheckForNextWave (waveTwo, 2);
			} else {
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				
				SpawnManager.instance.EnemySpawn15u4.SetActive (true);
//				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
	

			}
		}
	}


	void LoveYourHands200()
	{
		// This is for ChecksForFree_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
				
			} else if (timer < waveTwo) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
				//				SpawnManager.instance.EnemySpawn1dtest.SetActive (true);
				CheckForNextWave (waveTwo, 2);
			} else if (timer < waveThree) {
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				
				SpawnManager.instance.EnemySpawn15u4.SetActive (true);
				//				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
				
				CheckForNextWave(waveThree, 3);
			} else if (timer < waveFour) {

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				
				SpawnManager.instance.EnemySpawn15u4.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
	
				CheckForNextWave(waveFour, 4);

			} else {

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				
				SpawnManager.instance.EnemySpawn15u4.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
			}

		}
	}

	void SciFi230 ()
	{
		// This is for 5Sci-Fi_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			if (timer < waveOne) {
				CheckForNextWave (waveOne, 1);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);

			} else if (timer < waveTwo) {
		
				
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
//				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.EnemySpawn15d3.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
				SpawnManager.instance.EnemySpawn1u5.SetActive (true);
		
				CheckForNextWave (waveTwo, 2);
			} else if (timer < waveThree) {
				//Small wave
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn515.SetActive (true);

				CheckForNextWave (waveThree, 3);
			} else if (timer < waveFour) {
				//Big wave
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn51.SetActive (true);
				SpawnManager.instance.EnemySpawn1u5.SetActive (true);
				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				CheckForNextWave (waveFour, 4);
			} else if (timer < waveFive) {
				//Small wave
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn515.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);

				CheckForNextWave (waveFive, 5);
			} else {
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn51.SetActive (true);
				SpawnManager.instance.EnemySpawn3d3.SetActive (true);
				SpawnManager.instance.EnemySpawn2d3.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);

			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}

	}
	
	void HighTension210 ()
	{
		// This is for 4HighTension_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			if (timer < waveOne) {
				CheckForNextWave (waveTwo, 2);
			} else if (timer < 15) {
				
				//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);

				
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				
				
			} else if (timer < waveTwo) {

				SpawnManager.instance.EnemySpawn4d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);
				CheckForNextWave (waveTwo, 2);
			} else if (timer < waveThree) {
		

				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				SpawnManager.instance.EnemySpawn15d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				CheckForNextWave (waveThree, 3);
			} else if (timer < waveFour) {
	

				SpawnManager.instance.EnemySpawn1u6.SetActive (true);
				SpawnManager.instance.EnemySpawn4u6.SetActive (true);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn61.SetActive (true);
				CheckForNextWave (waveFour, 4);
			} else if (timer < waveFive) {
	
				SpawnManager.instance.EnemySpawn1d15.SetActive (true);
				SpawnManager.instance.EnemySpawn4u6.SetActive (true);
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);
				CheckForNextWave (waveFive, 5);
			} else {

				SpawnManager.instance.EnemySpawn5d3.SetActive (true);
				SpawnManager.instance.EnemySpawn2u6.SetActive (true);


			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Sarabande230 ()
	{
		//			This is for 9Sarabande_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			
			if (timer < waveOne) {
			
				//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				CheckForNextWave (waveOne, 1);
			} else if (timer < 22) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				
			} else if (timer < waveTwo) {

				//				level = 3;
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);

				CheckForNextWave (waveTwo, 2);

			} else if (timer < waveThree) {

				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
//				SpawnManager.instance.EnemySpawn1dtest.SetActive(true);
				CheckForNextWave (waveThree, 3);
			} else if (timer < 143) {
				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				//				level = 3;
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
			} else {
				//				level = 4;
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (false);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}
	
	void NinetySecondsOfFunk130 ()
	{
		// This is for 90 seconds of funk
		if (!Main.S.stopSpawning && !deadPeriod) {
			
			if (timer < waveOne) {
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				CheckForNextWave (waveOne, 1);

			} else if (timer < waveTwo) {
			
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d2.SetActive (true);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				CheckForNextWave (waveTwo, 2);
			} else if (timer < 41.5) {
				
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);

				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn4u8.SetActive (true);
			} else if (timer < waveThree) {
		
				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn3u8.SetActive (true);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);

				SpawnManager.instance.EnemySpawn6d3.SetActive (true);
				CheckForNextWave (waveThree, 3);
				
			} else {
				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
				SpawnManager.instance.EnemySpawn3d3.SetActive (true);
			}
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}
	
	void ThumpetteMini125 ()
	{
		//		This is for 7Thumpette_48
		if (!Main.S.stopSpawning && !deadPeriod) {
			//			timer = Time.timeSinceLevelLoad;
			if (timer < waveOne) {

				//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				CheckForNextWave (waveOne, 1);
			} else if (timer < 22) {
				//				level = 2;
				//			AsteroidSpawn2.SetActive(false);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
			} else if (timer < 34) {
				//				level = 3;

				SpawnManager.instance.AsteroidSpawn4.SetActive (true);
				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
			} else if (timer < 39) {
				//				level = 3;
				SpawnManager.instance.AsteroidSpawn1.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
	
			} else if (timer < waveTwo) {
			
	
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn3u9.SetActive (true);
				SpawnManager.instance.EnemySpawn4u9.SetActive (true);
				CheckForNextWave (waveTwo, 2);

			} else {
				//				level = 4;
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.AsteroidSpawn6.SetActive (true);
				SpawnManager.instance.EnemySpawn5d2.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
			}
			
			//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
		}
	}

	void Cresc230 ()
	{
		// This is for Cresc_48
		if (!Main.S.stopSpawning && !deadPeriod) {

			if (timer < waveOne) {
			
				SpawnManager.instance.AsteroidSpawn0.SetActive (true);


				CheckForNextWave (waveOne, 1);
			} else if (timer < waveTwo) {

				SpawnManager.instance.EnemySpawn2d1.SetActive (true);
				SpawnManager.instance.EnemySpawn1d1.SetActive (true);
				SpawnManager.instance.AsteroidSpawn10.SetActive (true);
//				SpawnManager.instance.EnemySpawn1d3.SetActive (true);
				SpawnManager.instance.EnemySpawn15d2.SetActive (true);
				CheckForNextWave (waveTwo, 2);
			} else if (timer < waveThree) {
		
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.AsteroidSpawn10.SetActive (true);

				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
				CheckForNextWave (waveThree, 3);
			} else if (timer < 68) {

				SpawnManager.instance.AsteroidSpawn8.SetActive (true);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
		

				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
			} else if (timer < 85) {

				SpawnManager.instance.EnemySpawn4d1.SetActive (true);

			} else if (timer < 94) {
				//				level = 3;
				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn7.SetActive (true);
				SpawnManager.instance.EnemySpawn3d2.SetActive (true);
				SpawnManager.instance.EnemySpawn6d2.SetActive (true);
			} else if (timer < waveFour) {

				SpawnManager.instance.AsteroidSpawn5.SetActive (true);
				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.EnemySpawn4d3.SetActive (true);
				SpawnManager.instance.EnemySpawn6d2.SetActive (true);
				CheckForNextWave (waveFour, 4);
			} else if (timer < waveFive) {

				SpawnManager.instance.AsteroidSpawn10.SetActive (true);
				SpawnManager.instance.EnemySpawn5d1.SetActive (true);
				SpawnManager.instance.EnemySpawn15d3.SetActive (true);
				CheckForNextWave (waveFive, 5);
				//				level = 4;
			} else {
				SpawnManager.instance.AsteroidSpawn10.SetActive (true);

				SpawnManager.instance.AsteroidSpawn9.SetActive (true);
				SpawnManager.instance.EnemySpawn2d2.SetActive (true);
				SpawnManager.instance.EnemySpawn4d2.SetActive (true);
				SpawnManager.instance.EnemySpawn6d1.SetActive (true);
			}
		}
	}
	
	void Level11Update ()
	{
		// This is for 10Roboskater_48
		////////NOT OPTIMIZED!!!!
		if (!Main.S.stopSpawning && !deadPeriod) {
			if (timer < 50) {
//				level = 1;
				//	Debug.Log("The time alive is " + main.timeAlive);
				//	Debug.Log("The first break is " + firstBreak);
//				SpawnManager.instance.AsteroidSpawn0.SetActive (true);
				//			AsteroidSpawn0.SetActive(true);
//				SpawnManager.instance.AsteroidSpawn2.SetActive (true);
				SpawnManager.instance.EnemySpawn11d1.SetActive (true);
			
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

	void Level14Update ()
	{
		//This is for 6Ectoplasm2_48
		if (!Main.S.stopSpawning && !deadPeriod) {
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

	void Dubstep200 ()
	{
		// This is for 8FunkyJunky_48
		if (!Main.S.stopSpawning && !deadPeriod) {
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
//				SpawnManager.instance.EnemySpawn3d1.SetActive (true);
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


}

