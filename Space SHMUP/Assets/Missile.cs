using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float speed = 10;
	public GameObject missileExplosion;
	private GameObject[] enemies;
	private GameObject[] asteroids;


	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody> ().velocity = transform.up * speed;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll){
		enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for (int i = 0; i < enemies.Length; i++) {
			Destroy(enemies[i]);
		}
		asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		for (int i = 0; i < asteroids.Length; i++) {
			Destroy(asteroids[i]);
		}

		Instantiate(missileExplosion, transform.position, transform.rotation);
		Destroy (this.gameObject);
	}
}
