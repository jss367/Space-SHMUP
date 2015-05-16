using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	private Main main;

	public int level;
	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;
//	public float timeLimit;

	public GameObject spawn1;


	public GameObject AsteroidSpawn1;
//	public GameObject AsteroidSpawn11;
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
//		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
//		firstBreak = timeLimit / 4;
//		secondBreak = timeLimit / 2;
//		thirdBreak = 3 * timeLimit / 4;

		firstBreak = Level1Manager.instance.firstBreak;
		secondBreak = Level1Manager.instance.secondBreak;
		thirdBreak = Level1Manager.instance.thirdBreak;

		AsteroidSpawn1 = Level1Manager.instance.AsteroidSpawn1;
		AsteroidSpawn2 = Level1Manager.instance.AsteroidSpawn2;
		AsteroidSpawn3 = Level1Manager.instance.AsteroidSpawn3;

		EnemySpawn1 = Level1Manager.instance.EnemySpawn1;
		EnemySpawn2 = Level1Manager.instance.EnemySpawn2;
		EnemySpawn3 = Level1Manager.instance.EnemySpawn3;
	}
	
	// Update is called once per frame
	void Update () {
		float timer = Time.timeSinceLevelLoad;
	if (timer < firstBreak) {
			level = 1;
		//	Debug.Log("The time alive is " + main.timeAlive);
		//	Debug.Log("The first break is " + firstBreak);
			AsteroidSpawn1.SetActive(true);
			EnemySpawn1.SetActive(true);

		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			AsteroidSpawn1.SetActive(false);
			AsteroidSpawn2.SetActive(true);
			EnemySpawn1.SetActive(false);
			EnemySpawn2.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			AsteroidSpawn2.SetActive(false);
			AsteroidSpawn3.SetActive(true);
			EnemySpawn2.SetActive(false);
			EnemySpawn3.SetActive(true);
		} else {
			level = 4;
			AsteroidSpawn2.SetActive(true);
			EnemySpawn2.SetActive(true);
			}
		//Debug.Log ("The player is on level " + level.ToString());
	}
}
