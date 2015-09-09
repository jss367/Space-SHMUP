using UnityEngine;
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
		WeaponType.blaster,
		WeaponType.spread,
		WeaponType.spread,
		WeaponType.shield
	};

	public GameObject	earthReward;
	public GameObject 	restartButton;
	public GameObject 	mainMenuButton;
	public GameObject	pauseButton;
	public GameObject	nextLevelButton;
	public Text			scoreText;
	public Text 		highScoreText;
	public Text			newHighScoreText;
	public Text			victoryText;
	public Text			currentAccountText;
	public Text			victoryBonusText;
	public Text			finalScoreText;
	public Text			prevBalanceText;
	public Text			popText;
	public bool missileEquipped;
	public GameObject missilePowerUp;

	public bool ______________;

	public bool spreadEquipped = false;
	public bool weaponrySet = false;
	public bool pointsGiven = false;
	private float timeOfDeath;
	
	public WeaponType[]			activeWeaponTypes;
	public float				enemySpawnRate; //Display between enemy spawns
	
	private int		score;
	public float	timeAlive;
	public float	timeLimit;

	public float account;
	public float coinsGained;

	public bool stopSpawning = false;
	public bool gameHasEnded = false;
	public bool playerWins = false;
	public bool playerDead = false;
	private int prevBalance;

	public bool laserEquipped = false;
//	public bool bazookaEquipped;
//	public bool missileEquipped;
	
	private int earth1;
	private int earth2;
	private int earth3;

	public string currentLevel;
	public int victoryBonus;
	public float endGameDelay = .2f;

	Vector3 pos1 = new Vector3(14, 1, -10);
	Vector3 pos2 = new Vector3(22, 1, -10);
	Vector3 pos3 = new Vector3(30, 1, -10);

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
		//		spreadEquipped = true; // comment out for builds
		activeWeaponTypes = new WeaponType[weaponDefinitions.Length];
		for (int i = 0; i < weaponDefinitions.Length; i++) {
			activeWeaponTypes [i] = weaponDefinitions [i].type;
		}
		nextLevelButton.SetActive (false);
		restartButton.SetActive (false);
		mainMenuButton.SetActive (false);
		highScoreText.enabled = false;
		currentAccountText.enabled = false;
		victoryBonusText.enabled = false;
		finalScoreText.enabled = false;
		prevBalanceText.enabled = false;
		victoryText.enabled = false;
		popText.enabled = false;
		newHighScoreText.enabled = false;

		score = 0;
		ResetScore ();
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
//		Debug.Log ("The song length is " + timeLimit);
		gameHasEnded = false;

		currentLevel = MadLevel.currentLevelName;


		CheckInventory ();

		}

	void CheckInventory(){
		
		//		Debug.Log ("Checking inventory");
		try
		{
			
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BLASTER_WEAPON_ITEM_ID)){
				Debug.Log("Blaster is equipped");
				
			}
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)){
				Debug.Log("Spread is equipped");
				
				spreadEquipped = true;
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}

		try
		{
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.MISSILE_LAUNCHER_ITEM_ID)){
				missileEquipped = true;
			}
			
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}

//		missileEquipped = true;
//		laserEquipped = true;	

	}

	void SetWeaponry() {
		//		Debug.Log ("At SetWeaponry, spreadEquipped is " + spreadEquipped);
		if (spreadEquipped) {
//			Debug.Log("Setting weapon to spread");
			powerUpFrequency = new WeaponType[] {
//			WeaponType.spread,
//			WeaponType.spread,
			WeaponType.spread,
			WeaponType.shield,
//			WeaponType.laser
		};
		} else if (laserEquipped) {
			powerUpFrequency = new WeaponType[] {
				WeaponType.laser
			};

		} else	{
//			Debug.Log("Setting weapon to white");
			powerUpFrequency = new WeaponType[] {
				//			WeaponType.spread,
				//			WeaponType.spread,

				WeaponType.blaster,
				WeaponType.shield

			};
	}


		weaponrySet = true;
	}
	void Update() {
		float timer = Time.timeSinceLevelLoad;
//		Debug.Log (timer);
//		Debug.Log (playerWins);
		if (!playerDead && !gameHasEnded && (timer > timeLimit + endGameDelay)){
			InvokeRepeating ("WaitUntilLevelEmpties", 0.0f, 0.5f);
		}

		if (!weaponrySet) {
			SetWeaponry ();
					}
	}

	public void AddScore (int newScoreValue)
	{
		if (!gameHasEnded && !playerDead) {
			StartCoroutine(PopText(newScoreValue.ToString(), 0.2f));
//					Debug.Log ("Score has been updated");
			score += newScoreValue;

		scoreText.text = "Score : " + score;  // ToString is called implicitly when + is used to concatenate to a string
	}
	}

	public void EnemyDestroyed( Enemy e, bool combo) {
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
		}

		// change to Hero.S.missileEquipped
//		Debug.Log (missileEquipped);
//		Debug.Log (Hero.S.launch1);
		if (missileEquipped && Hero.S.launch1 && Random.value <= e.missileDropChance) {
			Debug.Log("Dropping missile power up");
			Instantiate(missilePowerUp, e.transform.position, e.transform.rotation);
		}
//		if (combo) {
//			AddScore (e.score * 2);
//		} else {
			AddScore (e.score);
//		}
	}

	
	public void WaitUntilLevelEmpties(){
		
					Debug.Log ("Waiting until level empties");
		stopSpawning = true;
		GameObject[] AsteroidsRemaining = GameObject.FindGameObjectsWithTag ("Asteroid");
		//		Debug.Log ("Asteroids remaining: " + AsteroidsRemaining.Length);
		GameObject[] EnemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy");
		//		Debug.Log ("Enemies remaining: " + EnemiesRemaining.Length);
		if ((EnemiesRemaining.Length == 0 && AsteroidsRemaining.Length == 0) || (Time.time - timeOfDeath > 5.0f)) {
			//			Debug.Log("gameHasEnded: " + gameHasEnded);
			//			Debug.Log("Time.time: " + Time.time);
			//			Debug.Log("timeOfDeath: " + timeOfDeath);
			if (playerDead && !gameHasEnded) {
				GameOver ();
				CancelInvoke();
			} else if (!playerWins && !playerDead && EnemiesRemaining.Length == 0 && AsteroidsRemaining.Length == 0) {
				PlayerWon ();
				CancelInvoke();
				
			}
		}
		
	}


	public void PlayerLoss()
	{
		playerDead = true;
		timeOfDeath = Time.time;
//		Debug.Log("Player lost the level!");
//		GameOver ();
		InvokeRepeating ("WaitUntilLevelEmpties", 0.0f, 0.5f);
	}
	
	public void PlayerWon()
	{
//		Debug.Log("Player beat the level!");
		victoryText.enabled = true;
		playerWins = true;
//		GiveStars ();
		victoryBonusText.enabled = true;
		GameOver ();
	}

	public void GameOver() {
		gameHasEnded = true;
		pauseButton.SetActive (false);
//		Debug.Log("Game has ended");
//		scoreText.enabled = false;
		try
		{
			prevBalance = Soomla.Store.StoreInventory.GetItemBalance ("galactic_currency");
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
			prevBalance = 0;
		}

		prevBalanceText.text = "Previous Balance : " + prevBalance + " Coins";
		prevBalanceText.enabled = true;
		restartButton.SetActive(true);
		mainMenuButton.SetActive (true);
		if (!playerDead) {
			nextLevelButton.SetActive (true);
		}
		GivePoints ();
	}

	public void GivePoints(){
//		try {

		if (playerWins) {
//			GiveStars();
			GiveVictoryBonus();
			score += victoryBonus;
			victoryBonusText.text = "Level Completion Bonus : " + victoryBonus;
			}
		StartCoroutine (CountScore());
		StoreHighScore (score);
		if (!pointsGiven) {
//			Debug.Log("pointsGiven is " + pointsGiven);
//			Debug.Log("Rewarding points");
			try
			{
				Soomla.Store.StoreInventory.GiveItem("galactic_currency", score);
			}
			catch (System.Exception e)
			{
				Debug.Log("Caught error: " + e);
			}
			pointsGiven = true;
		}


	}
	IEnumerator CountScore(){
		Debug.Log ("Counting the score");
		int displayScore = 0;
		int updateAmount = 0;

		while (displayScore < score){
//			Debug.Log(score - displayScore);
			if (score - displayScore > 10000) {
				updateAmount = 1000;
				//				Debug.Log(updateAmount);
			} else if (score - displayScore > 1000) {
				updateAmount = 250;
				//				Debug.Log(updateAmount);
			} else if (score - displayScore > 250) {
				updateAmount = 50;
			} else if (score - displayScore > 100) {
				updateAmount = 10;
				//				Debug.Log(updateAmount);
			} else {
				displayScore = score;
				updateAmount = 0;
			}

			displayScore+= updateAmount;
			bool earth1Toggle = false;
			if (displayScore >= earth1 && !earth1Toggle){
				MadLevelProfile.SetLevelBoolean (currentLevel, "earth_1", true);
				Instantiate(earthReward, pos1, Quaternion.identity);
				earth1Toggle = true;
			}
			bool earth2Toggle = false;
			if (displayScore >= earth2 && !earth2Toggle){
				MadLevelProfile.SetLevelBoolean (currentLevel, "earth_2", true);
				Instantiate(earthReward, pos2, Quaternion.identity);
				earth2Toggle = true;
			}
			bool earth3Toggle = false;
			if (displayScore >= earth3 && !earth3Toggle){
				MadLevelProfile.SetLevelBoolean (currentLevel, "earth_3", true);
				Instantiate(earthReward, pos3, Quaternion.identity);
				earth3Toggle = true;
			}
			finalScoreText.text = "Final Score: " + displayScore;
			finalScoreText.enabled = true;
			yield return new WaitForSeconds(.01f);
				}
		Debug.Log ("Displaying final score");

		currentAccountText.text = "New Balance : " + (prevBalance + score) + " Coins";
		currentAccountText.enabled = true;
	}

	public void GiveVictoryBonus (){
//		Debug.Log("You are currently on this level: " + currentLevel);

		switch (currentLevel) {
		case "Level 1":
//			Debug.Log("on level one");
			victoryBonus = 50;
			earth1 = 100;
			earth2 = 175;
			earth3 = 250;

			break;
		case "Level 2":
			victoryBonus = 100;
			earth1 = 1500;
			earth2 = 1800;
			earth3 = 2300;

			break;
		case "Level 3":
			victoryBonus = 150;
			earth1 = 1000;
			earth2 = 1400;
			earth3 = 1750;

			break;
		case "Level 4":
			victoryBonus = 200;
			earth1 = 1000;
			earth2 = 1500;
			earth3 = 2000;

			break;
		case "Level 5":
			victoryBonus = 300;
			earth1 = 2500;
			earth2 = 3000;
			earth3 = 3700;
			
			break;
		case "Level 6":
			victoryBonus = 400;
			earth1 = 2500;
			earth2 = 3250;
			earth3 = 4000;
			
			break;
		case "Level 7":
			victoryBonus = 500;
			earth1 = 4000;
			earth2 = 5500;
			earth3 = 7500;
			
			break;
		case "Level 8":
			victoryBonus = 700;
			earth1 = 4000;
			earth2 = 7500;
			earth3 = 9000;
			
			break;
		case "Level 9":
			victoryBonus = 900;
			earth1 = 5000;
			earth2 = 8000;
			earth3 = 10000;

			break;
		case "Level 10":
			victoryBonus = 1200;
			earth1 = 7500;
			earth2 = 10000;
			earth3 = 12000;
			
			break;
		default:
			victoryBonus = 1000;
			earth1 = 30000;
			earth2 = 40000;
			earth3 = 50000;

			break;

		}
		MadLevelProfile.Save ();
	}


	public void MainMenu() {
//		Application.LoadLevel ("LevelManager");
		MadLevel.LoadLevelByName ("Level Select");
	}

	public void NextLevel(){
		MadLevel.LoadNext ();
	}

	public void AsteroidDestroyed(Asteroid a) {
		AddScore (a.score);
	}

	void StoreHighScore(int newHighScore){
//		Debug.Log ("StoreHighScore has been called");
	
		//		Debug.Log ("The score is " + score);
		// Update the high score if PlayerPrefs if necessary
		if (score > PlayerPrefs.GetInt ("GalacticHighScore" + currentLevel, 0)) {
			Debug.Log("Setting new high score");
			PlayerPrefs.SetInt ("GalacticHighScore" + currentLevel, score);
			newHighScoreText.enabled = true;
		}
		highScoreText.text = "High Score: " + PlayerPrefs.GetInt ("GalacticHighScore" + currentLevel, 0);
		highScoreText.enabled = true;
	}



	IEnumerator PopText(string message, float time){
			popText.text = "+ " + message;
			popText.enabled = true;
			yield return new WaitForSeconds (time);
			popText.enabled = false;
//			Vector3 scre
//		popText.transform.position
	}
	
	void ResetScore ()
	{
//		Debug.Log ("Score has been updated");
		scoreText.text = "Score : " + score;  // ToString is called implicitly when + is used to concatenate to a string
	}


	public void RestartLevel()
	{
		MadLevel.LoadLevelByName (currentLevel);
	}

}



//}