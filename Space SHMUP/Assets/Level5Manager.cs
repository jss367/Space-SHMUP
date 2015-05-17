using UnityEngine;
using System.Collections;

public class Level5Manager : MonoBehaviour {
	
	public int level;
	public float firstBreak;
	public float secondBreak;
	public float thirdBreak;
	public float timeLimit;
	/*
variables that should be changed by level:
spawn breaks
victorypoints
amount of points needed to get stars
*/
	
	//
	void Start(){
		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;
		firstBreak = timeLimit / 4;
		secondBreak = timeLimit / 2;
		thirdBreak = 3 * timeLimit / 4;
	}
	
	
	// Update is called once per frame
	void Update () {
		float timer = Time.timeSinceLevelLoad;
		if (timer < firstBreak) {
			level = 1;
			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.AsteroidSpawn0.SetActive(true);
			//			AsteroidSpawn0.SetActive(true);
			SpawnManager.instance.AsteroidSpawn2.SetActive(true);
			SpawnManager.instance.EnemySpawn1d1.SetActive(true);
			
		} else if (timer >= firstBreak && timer < secondBreak) {
			level = 2;
			//			AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn5.SetActive(true);
			SpawnManager.instance.EnemySpawn1d3.SetActive(false);
			SpawnManager.instance.EnemySpawn2d1.SetActive(true);
			SpawnManager.instance.EnemySpawn3d1.SetActive(true);
		} else if (timer >= secondBreak && timer < thirdBreak) {
			level = 3;
			SpawnManager.instance.AsteroidSpawn2.SetActive(false);
			SpawnManager.instance.AsteroidSpawn7.SetActive(true);
			SpawnManager.instance.EnemySpawn2d1.SetActive(false);
			SpawnManager.instance.EnemySpawn4d3.SetActive(true);
		} else {
			level = 4;
			SpawnManager.instance.AsteroidSpawn8.SetActive(true);
			SpawnManager.instance.EnemySpawn4d3.SetActive(false);
			SpawnManager.instance.EnemySpawn5d1.SetActive(true);
		}
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}
	
	
	
}
