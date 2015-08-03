using UnityEngine;
using System.Collections;

public class BazookaBullet : MonoBehaviour {

	public GameObject bazookaBulletExplosion;
	public float speed = 20;
	public float damageRadius = 10;

	public void Start () {
		GetComponent<Rigidbody> ().velocity = transform.up * speed;
	}

	void OnTriggerEnter (Collider coll){
		Debug.Log ("Mine has been triggered by " + coll);
		Destroy (this.gameObject);
		Destroy(coll.gameObject);
		Instantiate(bazookaBulletExplosion, transform.position, transform.rotation);
		ExplosionDamage (transform.position, damageRadius);
	}

	void ExplosionDamage(Vector3 center, float radius) {
		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) {
//			hitColliders[i].SendMessage("AddDamage");
			hitColliders[i].BroadcastMessage("ApplyDamage", 5.0f);
			i++;
		}
	}
}
