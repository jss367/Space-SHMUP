using UnityEngine;
using System.Collections;

public class Level7Manager : MonoBehaviour {
	
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


			//	Debug.Log("The time alive is " + main.timeAlive);
			//	Debug.Log("The first break is " + firstBreak);
			SpawnManager.instance.EnemySpawn0d1.SetActive(true);

 
		//		Debug.Log ("The player is on level " + SpawnManager.instance.level.ToString());
	}
	
	
	
}
