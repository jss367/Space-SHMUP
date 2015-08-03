using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float speed = 10;
	public GameObject missileExplosion;
	private GameObject[] enemies;
	private GameObject[] asteroids;
//	public bool onFire = false;


//	void Awake(){
//		Missile = this;
//	}

	// Use this for initialization
	public void Start () {
		GetComponent<Rigidbody> ().velocity = transform.up * speed;
	}
	
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
