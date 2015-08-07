using UnityEngine;
using System.Collections;

public class BazookaBullet : MonoBehaviour {

	public GameObject bazookaBulletExplosion;
	public float speed = 20;
	public float damageRadius = 20;
	public float damage = 10.0f;

	public void Start () {
		GetComponent<Rigidbody> ().velocity = transform.up * speed;
	}

	void OnTriggerEnter (Collider coll){
		Debug.Log ("BazookaBullet has been triggered by " + coll);
		Destroy (this.gameObject);
//		Destroy(coll.gameObject);
		Instantiate(bazookaBulletExplosion, transform.position, transform.rotation);
		ExplosionDamage (transform.position, damageRadius);

	}

	void ExplosionDamage(Vector3 center, float radius) {
//		Enemy recipient = 

		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) {
			Enemy recipient = hitColliders[i].GetComponent<Enemy> ();
			if (recipient != null) {
				recipient.ReceiveDamage(damage);
			}
			Asteroid asteroidRecipient = hitColliders[i].GetComponent<Asteroid>();
			//also do for angle asteroid!!!
			if (asteroidRecipient != null) {
				Debug.Log("Hit an asteroid");
//				asteroidRecipient.ReceiveDamage(damage);
			}
			i++;
		}
	}
}
