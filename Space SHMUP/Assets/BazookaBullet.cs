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
			Debug.Log("Explosion found this hitCollider: " + hitColliders[i]);
			GameObject go = Utils.FindTaggedParent(hitColliders[i].gameObject);
			Enemy recipient = go.GetComponent<Enemy> (); // this might hit enemies with many body parts more than those with only one
			Debug.Log(recipient);
			if (recipient != null) {
				Debug.Log("You're looking for " + hitColliders[i].ToString());
				if (hitColliders[i].ToString() == "Left (UnityEngine.BoxCollider)" ||
				    hitColliders[i].ToString() == "Right (UnityEngine.BoxCollider)" ||
				    hitColliders[i].ToString() == "Middle (UnityEngine.BoxCollider)"){
					continue;
				}
				recipient.ReceiveDamage(damage);
			}
			Asteroid asteroidRecipient = go.GetComponent<Asteroid>();
			//also do for angle asteroid!!!
			if (asteroidRecipient != null) {
				Debug.Log("Hit an asteroid");
//				asteroidRecipient.ReceiveDamage(damage);
			}
			i++;
		}
	}
}

