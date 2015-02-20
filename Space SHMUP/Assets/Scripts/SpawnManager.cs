using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	private Main main;

	public int level;
	public float firstBreak = 5;
	public float secondBreak = 10;
	public float thirdBreak = 15;

	public GameObject AsteroidSpawn1;
	public GameObject AsteroidSpawn2;
	public GameObject AsteroidSpawn3;

	public GameObject EnemySpawn1;
	public GameObject EnemySpawn2;
	public GameObject EnemySpawn3;


	// Use this for initialization
	void Start () {
	
		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}

	}
	
	// Update is called once per frame
	void Update () {
	if (main.timeAlive < firstBreak) {
			level = 1;
		//	Debug.Log("The time alive is " + main.timeAlive);
		//	Debug.Log("The first break is " + firstBreak);
		} else if (main.timeAlive >= firstBreak && main.timeAlive < secondBreak) {
			level = 2;
			AsteroidSpawn1.SetActive(false);
			AsteroidSpawn2.SetActive(true);
			EnemySpawn1.SetActive(false);
			EnemySpawn2.SetActive(true);
		} else {
			level = 3;
			AsteroidSpawn2.SetActive(false);
			AsteroidSpawn3.SetActive(true);
		}
		//Debug.Log ("The player is on level " + level.ToString());
	}
}
