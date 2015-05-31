using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager instance { get; private set; }
	public float		spawnPadding = 1.5f;
	public GameObject	LevelManager;

	private Main main;

//	public int level;
//	public float firstBreak;
//	public float secondBreak;
//	public float thirdBreak;
	public float timeLimit;

	public GameObject AsteroidSpawn0;
	public GameObject AsteroidSpawn1;
	public GameObject AsteroidSpawn2;
	public GameObject AsteroidSpawn3;
	public GameObject AsteroidSpawn4;
	public GameObject AsteroidSpawn5;
	public GameObject AsteroidSpawn6;
	public GameObject AsteroidSpawn7;
	public GameObject AsteroidSpawn8;
	public GameObject AsteroidSpawn9;
	public GameObject EnemySpawn0d1;
	public GameObject EnemySpawn1d1;
	public GameObject EnemySpawn1d2;
	public GameObject EnemySpawn1d3;
	public GameObject EnemySpawn2d1;
	public GameObject EnemySpawn3d1;
	public GameObject EnemySpawn3d2;
	public GameObject EnemySpawn4d3;
	public GameObject EnemySpawn5d1;
	public GameObject EnemySpawn10d1;
	public GameObject Mid1;
	public GameObject Mid2;
	public GameObject Bass1;
	public GameObject High1;

	void Awake()
	{
		instance = this;
//		LevelManager = GameObject.Find ("LevelManager");
//		AsteroidSpawn0 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn0");
		AsteroidSpawn1 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn1");
		AsteroidSpawn2 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn2");
		AsteroidSpawn3 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn3");
		AsteroidSpawn4 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn4");
		AsteroidSpawn5 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn5");
		AsteroidSpawn6 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn6");
		AsteroidSpawn7 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn7");
		EnemySpawn0d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn0-1");
		Debug.Log("AsteroidSpawn0 is " + AsteroidSpawn0);
		EnemySpawn1d3 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn1-3");
		EnemySpawn2d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn2-1");
		EnemySpawn3d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn3-1");
		Debug.Log("EnemySpawn1d3 is " + EnemySpawn1d3);

	}

	// Use this for initialization
	void Start () {
		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}
		Vector3 pos = Vector3.zero;
		pos.y = Utils.camBounds.max.y + spawnPadding;
		LevelManager.transform.position = pos;
	
		Debug.Log("EnemySpawn0d1 is " + EnemySpawn0d1);
		Debug.Log("EnemySpawn1d3 is " + EnemySpawn1d3);

	}

}
