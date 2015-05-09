﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; //Required to use Lists or Dictionaries
using Soomla;
using MadLevelManager;

//namespace Soomla.Store.Example {

public class Main : MonoBehaviour {
	static public Main			S;
	static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;
	public GameObject[]			prefabEnemies;
	public float				enemySpawnPerSecond = 0.5f;
	public float				enemySpawnPadding = 1.5f;
	public WeaponDefinition[]	weaponDefinitions;
	public GameObject			prefabPowerUp;
	public WeaponType[]			powerUpFrequency = new WeaponType[] {
		WeaponType.ball,
		WeaponType.blaster,
		WeaponType.spread,
		WeaponType.shield
	};

	public Text scoreText;
	public GameObject restartButton;
	public GameObject mainMenuButton;
	public Text gameOverText;
	public Text highScoreText;
	public Text accountText;


	public bool ______________;
	
	public WeaponType[]			activeWeaponTypes;
	public float				enemySpawnRate; //Display between enemy spawns
	
	private int score;
	public float timeAlive;
	public float timeMultiplier;
//	private float timeLastReset;
	public float timeLimit;

	public float account;
	public float coinsGained;

	public bool gameHasEnded;

	public string currentLevel;

	public float endGameDelay = 2.5f;

	void Awake(){

		S = this;
		//Set Utils.camBounds
		Utils.SetCameraBounds (this.GetComponent<Camera>());
		//  0.5 enemies/second = enemySpawnRate of 2
		enemySpawnRate = 1f / enemySpawnPerSecond;
		//Invoke call SpawnEnemy() once after a 2 second delay
//		Invoke ("SpawnEnemy", enemySpawnRate);
		
		//A generic Dictionary with WeaponType as the key
		W_DEFS = new Dictionary<WeaponType, WeaponDefinition> ();
		foreach (WeaponDefinition def in weaponDefinitions) {
			W_DEFS [def.type] = def;
		}


		// If the HighScore already exists, read it
		if (PlayerPrefs.HasKey ("GalacticHighScore")) {
			score = PlayerPrefs.GetInt ("GalacticHighScore");
		}
		//Assign the high score to GalacticHighScore
		PlayerPrefs.SetInt ("GalacticHighScore", score);

	}

	static public WeaponDefinition GetWeaponDefinition (WeaponType wt) {
		//Check to make sure that the key exists in the Dictionary
		//Attempting to retrieve a key that didn't exist would throw an error
		//so use the following if statement first
		if (W_DEFS.ContainsKey (wt)) {
			return (W_DEFS [wt]);
		}
		//This will return a definition for WeaponType.none, which
		// means it has failed to find the WeaponDefinition
		return(new WeaponDefinition ());
		
	}
	
	void Start() {
		activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
		for (int i = 0; i < weaponDefinitions.Length; i++) {
			activeWeaponTypes [i] = weaponDefinitions [i].type;
		}
		restartButton.SetActive (false);
		mainMenuButton.SetActive (false);
		highScoreText.enabled = false;
		accountText.enabled = false;
		score = 0;
		UpdateScore ();
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
//		Debug.Log ("The song length is " + timeLimit);
		Soomla.Store.SoomlaStore.Initialize(new Soomla.Store.Example.GalacticBeatsAssets());
		gameHasEnded = false;

		currentLevel = MadLevel.currentLevelName;
	}

	void Update() {
		float timer = Time.timeSinceLevelLoad;
		timeMultiplier = Time.timeSinceLevelLoad / 4;
		if (!gameHasEnded && (timer > timeLimit + endGameDelay)){
			GameOver();
			gameHasEnded = true;
		}


		highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("Highscore", 0);
//		Debug.Log ("The score is " + score);
		// Update the high score if PlayerPrefs if necessary
		if (score > PlayerPrefs.GetInt ("GalacticHighScore")) {
			Debug.Log("Setting new high score");
			PlayerPrefs.SetInt ("GalacticHighScore", score);
		}
	}

//	public void SpawnEnemy(){
//		//Pick a random Enemy prefab to instantiate
//		int ndx = Random.Range (0, prefabEnemies.Length);
//		GameObject go = Instantiate (prefabEnemies [ndx]) as GameObject;
//		//Position the Enemy above the screen with a random x position
//		Vector3 pos = Vector3.zero;
//		float xMin = Utils.camBounds.min.x + enemySpawnPadding;
//		float xMax = Utils.camBounds.max.x - enemySpawnPadding;
//		pos.x = Random.Range (xMin, xMax);
//		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
//		go.transform.position = pos;
//		//Call SpawnEnemy() again in a couple of seconds
//		Invoke ("SpawnEnemy", enemySpawnRate);
//	}

	public void DelayedRestart(float delay) {
		//Invoke the Restart() method in delay seconds
		Invoke ("Restart", delay);
	}
	public void Restart(){
		
		//Reload scene to restart the game
		//Application.LoadLevel (Application.loadedLevel);
		restartButton.SetActive(true);
		StoreHighScore (score);
	}

	public void RestartGame()
	{
//		Application.LoadLevel (Application.loadedLevel);
		MadLevel.LoadLevelByName (currentLevel);
	}

	public void ShipDestroyed( Enemy e) {
		// Potentially generate a PowerUp
		if (Random.value <= e.powerUpDropChance) {
			//Random.value generates a value between 0 & 1 (through never == 1)
			// If the e.powerUpDropChnace is .50f, a PowerUp will be generated
			// 50% of the time. For testing, it's now set to 1f.
			
			//Choose which PowerUp to pick
			// Pick one from the possibilities in powerUpFrequency
			int ndx = Random.Range(0, powerUpFrequency.Length);
			WeaponType puType = powerUpFrequency[ndx];
			
			// Spawn a PowerUp
			GameObject go = Instantiate(prefabPowerUp) as GameObject;
			PowerUp pu = go.GetComponent<PowerUp>();
			// Set it to the proper WeaponTYpe
			pu.SetType(puType);
			
			// Set it to the position of the destroyed ship
			pu.transform.position = e.transform.position;

			AddScore(e.score);
		}
	}

	public void GameOver() {
		mainMenuButton.SetActive (true);
		StoreHighScore (score);
		highScoreText.enabled = true;
		GivePoints ();
	}

	public void GivePoints(){
//		try {
		account = Soomla.Store.StoreInventory.GetItemBalance ("galactic_currency");
//		coinsGained = Soomla.Store.StoreInventory.GiveItem ("galactic_currency", 10);
		Soomla.Store.StoreInventory.GiveItem("galactic_currency", 10); // fix this
		accountText.text = "Your final score is: " + score + "\nYou now have: " + account;
		accountText.enabled = true;
		RewardPlayer ();
//		} catch (Exception e) {
//			Debug.LogError ("SOOMLA/UNITY " + e.Message);
//		}

		// Update Level Manager
		MadLevelProfile.SetCompleted (currentLevel, true);
//		MadLevelProfile.SetPropertyEnabled (currentLevel, "star", true);
		MadLevelProfile.SetLevelBoolean (currentLevel, "earth_1", true);
		MadLevelProfile.SetLevelBoolean (currentLevel, "earth_2", true);
		MadLevelProfile.Save ();


	}

	public void RewardPlayer(){
//		Soomla.Store.StoreInventory.GiveItem ("galactic_currency", 10);

	}

//	public VirtualCurrencyPack[] GetCurrencyPacks() {
////		return new VirtualCurrencyPack[] {TENMUFF_PACK, FIFTYMUFF_PACK, FOURHUNDMUFF_PACK, THOUSANDMUFF_PACK};
//	}
//
//	public static VirtualCurrencyPack THOUSANDMUFF_PACK = new VirtualCurrencyPack(
//		"1000 Muffins",                                 // name
//		"Test item unavailable",                 		// description
//		"muffins_1000",                                 // item id
//		1000,                                           // number of currencies in the pack
//		GALACTIC_CURRENCY_ITEM_ID,                        // the currency associated with this pack
//		new PurchaseWithMarket(THOUSANDMUFF_PACK_PRODUCT_ID, 8.99)
//		);

	public void MainMenu() {
//		Application.LoadLevel ("LevelManager");
		MadLevel.LoadLevelByName ("Level Select");
	}

	public void AsteroidDestroyed(SU_Asteroid a) {
		AddScore (a.score);
	}

	void StoreHighScore(int newHighScore){
//		Debug.Log ("StoreHighScore has been called");
		int oldHighScore = PlayerPrefs.GetInt ("Highscore", 0);
		if (newHighScore > oldHighScore) {
			PlayerPrefs.SetInt ("Highscore", newHighScore);
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		//Track the high score
//		if (score > HighScore.score) {
//			HighScore.score = score;
//		}
		scoreText.text = "Score: " + score;  // ToString is called implicitly when + is used to concatenate to a string
		//UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;  // ToString is called implicitly when + is used to concatenate to a string
	}

}



//}