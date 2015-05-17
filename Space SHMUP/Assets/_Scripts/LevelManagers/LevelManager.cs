//using UnityEngine;
//using System.Collections;
//
//public class LevelManager : MonoBehaviour {
//	
//	public GameObject SpawnManager;
//	
//	public GameObject AsteroidSpawn1;
//	public GameObject AsteroidSpawn2;
//	public GameObject AsteroidSpawn3;
//	public GameObject AsteroidSpawn4;
//	public GameObject AsteroidSpawn5;
//	public GameObject AsteroidSpawn6;
//	public GameObject AsteroidSpawn7;
//	public GameObject AsteroidSpawn8;
//	public GameObject AsteroidSpawn9;
//	
//	public GameObject EnemySpawn1;
//	public GameObject EnemySpawn2;
//	public GameObject EnemySpawn3;
//	
//	public float timeLimit;
//	
//	public float firstBreak;
//	public float secondBreak;
//	public float thirdBreak;
//	
//	/*
//variables that should be changed by level:
//spawn breaks
//victorypoints
//
//*/
//	//Private reference for this class only
//	//	private static Level1Manager _instance;
//	
//	//Public reference for other classes
//	public static LevelManager instance { get; private set;}
//	
//	void Awake()
//	{
//		instance = this;
//		timeLimit = GameObject.Find ("Beat").GetComponentInChildren<AudioManager> ().timeLimit;
//		//		firstBreak = timeLimit / 4;
//		//		secondBreak = timeLimit / 2;
//		//		thirdBreak = 3 * timeLimit / 4;
//		
//		firstBreak = 4;
//		secondBreak = 10;
//		thirdBreak = 13;
//	}
//	
//	void Start()
//	{
//		AsteroidSpawn1 = GameObject.Find ("SpawnManager/AsteroidSpawn21");
//		//		AsteroidSpawn1 = GameObject.Find ("SpawnManager");
//	}
//	
//	void Update()
//	{
////		Debug.Log (AsteroidSpawn1);
//	}
//	
//	//	public string GetValues()
//	//	{
//	//	}
//	//
//	//	public float GetFloats()
//	//	{
//	//
//	//	}
//	
//}
