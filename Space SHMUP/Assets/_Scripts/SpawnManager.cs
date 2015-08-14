using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public GameObject EnemySpawn1d05;
	public GameObject EnemySpawn1d1;
	public GameObject EnemySpawn1d15;
	public GameObject EnemySpawn1d2;
	public GameObject EnemySpawn1d3;
	public GameObject EnemySpawn2d1;
	public GameObject EnemySpawn2d15;
	public GameObject EnemySpawn2d2;
	public GameObject EnemySpawn2d3;
	public GameObject EnemySpawn3d1;
	public GameObject EnemySpawn3d15;
	public GameObject EnemySpawn3d2;
	public GameObject EnemySpawn3d3;
	public GameObject EnemySpawn4d1;
	public GameObject EnemySpawn4d2;
	public GameObject EnemySpawn4d3;
	public GameObject EnemySpawn5d1;
	public GameObject EnemySpawn5d2;
	public GameObject EnemySpawn5d3;
	public GameObject EnemySpawn6d1;
	public GameObject EnemySpawn6d2;
	public GameObject EnemySpawn6d3;
	public GameObject EnemySpawn10d1;
	public GameObject Bass1;
	public GameObject Bass2;
	public GameObject Bass3;
	public GameObject Bass4;
	public GameObject Bass5;
	public GameObject Bass6;
	public GameObject Bass7;
	public GameObject Bass8;
	public GameObject Bass9;
	public GameObject Bass10;
	public GameObject Bass11;
	public GameObject Mid1;
	public GameObject Mid2;
	public GameObject Mid3;
	public GameObject Mid4;
	public GameObject Mid5;
	public GameObject Mid6;
	public GameObject Mid7;
	public GameObject Mid8;
	public GameObject Mid9;
	public GameObject Mid10;
	public GameObject Mid11;
//	public GameObject High1;
//	public GameObject High2;
//	public GameObject High3;
//	public GameObject High4;
//	public GameObject High5;
//	public GameObject High6;
//	public GameObject High7;
//	public GameObject High8;
//	public GameObject High9;
//	public GameObject High10;
//	public GameObject High11;
	public GameObject Mid;
	public GameObject Bass;
	public Text WaveText;

	void Awake()
	{
		instance = this;
//		LevelManager = GameObject.Find ("LevelManager");
//		AsteroidSpawn0 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn0");
//		AsteroidSpawn1 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn1");
//		AsteroidSpawn2 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn2");
//		AsteroidSpawn3 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn3");
//		AsteroidSpawn4 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn4");
//		AsteroidSpawn5 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn5");
//		AsteroidSpawn6 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn6");
//		AsteroidSpawn7 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn7");
//		AsteroidSpawn8 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn8");
//		AsteroidSpawn9 = GameObject.Find ("LevelManager/AsteroidManager/AsteroidSpawn9");
//		EnemySpawn0d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn0-1");
//		EnemySpawn1d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn1-1");
//		EnemySpawn1d2 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn1-2");
//		EnemySpawn1d3 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn1-3");
//		EnemySpawn2d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn2-1");
//		EnemySpawn3d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn3-1");
//		EnemySpawn3d2 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn3-2");
//		EnemySpawn4d3 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn4-3");
//		EnemySpawn5d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn5-1");
//		EnemySpawn10d1 = GameObject.Find ("LevelManager/EnemyManager/EnemySpawn10-1");
//		Mid1 = GameObject.Find ("LevelManager/FireworksManager/Mid1");
//		Mid2 = GameObject.Find ("LevelManager/FireworksManager/Mid2");
//		High1 = GameObject.Find ("LevelManager/FireworksManager/High1");
//		Bass1 = GameObject.Find ("LevelManager/FireworksManager/Bass1");

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


	}

}
