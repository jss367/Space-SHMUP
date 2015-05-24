using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager instance { get; private set; }

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
	}

	// Use this for initialization
	void Start () {
		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}

	
	}

}
