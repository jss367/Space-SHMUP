using UnityEngine;
using System.Collections;

public class Level2Manager : SpawnManager {
	
	public int level;
	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;
	public float timeLimit;
	
	/*
variables that should be changed by level:
spawn breaks
victorypoints

*/
	//Private reference for this class only
	//	private static Level1Manager _instance;
	
	//Public reference for other classes
	public static Level2Manager instance { get; private set;}
	
	void Awake()
	{
		instance = this;
		timeLimit = GameObject.Find ("Beat").GetComponentInChildren<AudioManager> ().timeLimit;
		//		firstBreak = timeLimit / 4;
		//		secondBreak = timeLimit / 2;
		//		thirdBreak = 3 * timeLimit / 4;
		
		firstBreak = 4;
		secondBreak = 10;
		thirdBreak = 13;
	}
	
	void Start()
	{
//		AsteroidSpawn1 = GameObject.Find ("SpawnManager/AsteroidSpawn2-1");
//		AsteroidSpawn2 = GameObject.Find ("SpawnManager/AsteroidSpawn22");
//		AsteroidSpawn3 = GameObject.Find ("SpawnManager/AsteroidSpawn23");
//		EnemySpawn1d2 = GameObject.Find ("SpawnManager/EnemySpawn1");
		//		AsteroidSpawn1 = GameObject.Find ("SpawnManager");
	}
	
	// Update is called once per frame
	void Update () {
		float timer = Time.timeSinceLevelLoad;
		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			AsteroidSpawn0.SetActive(true);
			AsteroidSpawn2.SetActive(true);
			EnemySpawn1d3.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			AsteroidSpawn2.SetActive(false);
			AsteroidSpawn5.SetActive(true);
			EnemySpawn1d3.SetActive(false);
			EnemySpawn2d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			AsteroidSpawn5.SetActive(false);
			AsteroidSpawn7.SetActive(true);
			EnemySpawn2d1.SetActive(false);
			EnemySpawn3d2.SetActive(true);
		} else {
			level = 4;
			AsteroidSpawn2.SetActive(true);
			EnemySpawn2d1.SetActive(true);
		}
		//Debug.Log ("The player is on level " + level.ToString());
	}
	
	//	public string GetValues()
	//	{
	//	}
	//
	//	public float GetFloats()
	//	{
	//
	//	}
	
}
