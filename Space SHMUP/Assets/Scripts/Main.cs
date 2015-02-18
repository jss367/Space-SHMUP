﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic; //Required to use Lists or Dictionaries

public class Main : MonoBehaviour {
	static public Main			S;
	static public Dictionary<WeaponType, WeaponDefinition> W_DEFS;
	public GameObject[]			prefabEnemies;
	public float				enemySpawnPerSecond = 0.5f;
	public float				enemySpawnPadding = 1.5f;
	public WeaponDefinition[]	weaponDefinitions;
	public GameObject			prefabPowerUp;
	public WeaponType[]			powerUpFrequency = new WeaponType[] {
		WeaponType.blaster,
		WeaponType.blaster,
		WeaponType.spread,
		WeaponType.shield
	}; //Two blasters because it is twice as likely to appear in a powerup

	public Text scoreText;
	public GUIText restartText;  //change these to Text
	public GUIText gameOverText;


	public bool ______________;
	
	public WeaponType[]			activeWeaponTypes;
	public float				enemySpawnRate; //Display between enemy spawns
	
	private int score;
	public float timeAlive;
	public float timeMultiplier;
	private float timeLastReset;
	
	void Awake(){
		S = this;
		//Set Utils.camBounds
		Utils.SetCameraBounds (this.camera);
		//  0.5 enemies/second = enemySpawnRate of 2
		enemySpawnRate = 1f / enemySpawnPerSecond;
		//Invoke call SpawnEnemy() once after a 2 second delay
		Invoke ("SpawnEnemy", enemySpawnRate);
		
		//A generic Dictionary with WeaponType as the key
		W_DEFS = new Dictionary<WeaponType, WeaponDefinition> ();
		foreach (WeaponDefinition def in weaponDefinitions) {
			W_DEFS [def.type] = def;
		}
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

		score = 0;
		UpdateScore ();
	}


	void Update() {
		//TimeAlive is the time since the last reset
		timeAlive = Time.time - timeLastReset;
		//Debug.Log (timeAlive);
		timeMultiplier = timeAlive / 4;  // Subtract the time of the most recent death
		//Debug.Log ("The time since the last reset is " + timeLastReset);
	}

	public void SpawnEnemy(){
		//Pick a random Enemy prefab to instantiate
		int ndx = Random.Range (0, prefabEnemies.Length);
		GameObject go = Instantiate (prefabEnemies [ndx]) as GameObject;
		//Position the Enemy above the screen with a random x position
		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x + enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - enemySpawnPadding;
		pos.x = Random.Range (xMin, xMax);
		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
		go.transform.position = pos;
		//Call SpawnEnemy() again in a couple of seconds
		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	public void DelayedRestart(float delay) {
		//Invoke the Restart() method in delay seconds
		Invoke ("Restart", delay);
	}
	public void Restart(){
		
		//Reload scene Main to restart the game
		Application.LoadLevel ("Main");
		//Set the time of the last reset
		timeLastReset = Time.time;
		Debug.Log ("timeLastReset is set to " + timeLastReset);

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

	public void AsteroidDestroyed(SU_Asteroid a) {
		AddScore (a.score);
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;  // should this be score.ToString()?
	}

}