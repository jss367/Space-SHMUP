using UnityEngine;
using System.Collections;

public class BossSpawn : MonoBehaviour {

	public GameObject EnemyBoss;

	// Use this for initialization
	void Start () {
		Instantiate (EnemyBoss);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
