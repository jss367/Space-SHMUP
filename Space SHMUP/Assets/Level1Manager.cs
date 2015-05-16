using UnityEngine;
using System.Collections;

public class Level1Manager : MonoBehaviour {

	public GameObject AsteroidSpawn1;
	public GameObject AsteroidSpawn2;
	public GameObject AsteroidSpawn3;

	public GameObject EnemySpawn1;
	public GameObject EnemySpawn2;
	public GameObject EnemySpawn3;

	public float timeLimit;

	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;

	/*
variables that should be changed by level:
spawn breaks
victorypoints

*/
	//Private reference for this class only
//	private static Level1Manager _instance;

	//Public reference for other classes
	public static Level1Manager instance { get; private set;}

	void Awake()
	{
		instance = this;
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
//		firstBreak = timeLimit / 4;
//		secondBreak = timeLimit / 2;
//		thirdBreak = 3 * timeLimit / 4;

		firstBreak = 4;
		secondBreak = 10;
		thirdBreak = 13;
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
