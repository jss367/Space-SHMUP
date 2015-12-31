using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
	public GameObject mineExplosion;
	public float damageRadius = 10.0f;
	public float damage = 10.0f;

	void OnTriggerEnter (Collider coll)
	{
		if (coll.ToString () != "Shield (UnityEngine.SphereCollider)") {
//			Debug.Log ("Mine has been triggered by " + coll);
			GameObject parent = Utils.FindTaggedParent (coll.gameObject);
//			Debug.Log ("The parent is " + parent);
			Destroy (this.gameObject);
	//		Destroy (parent.gameObject);
			ExplosionDamage (transform.position, damageRadius);
			Instantiate (mineExplosion, transform.position, transform.rotation);
		}
	}

	void ExplosionDamage(Vector3 center, float radius) {

		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
		int i = 0;
		while (i < hitColliders.Length) {
			if (hitColliders [i].ToString () != "Mine 1(Clone) (UnityEngine.SphereCollider)") {
//				Debug.Log (hitColliders.Length);
//				Debug.Log ("Explosion found this hitCollider: " + hitColliders [i]);
				GameObject go = Utils.FindTaggedParent (hitColliders [i].gameObject);
//				Debug.Log (go);

				Enemy recipient = go.GetComponent<Enemy> (); // this might hit enemies with many body parts more than those with only one
//				Debug.Log ("Recipient is :" + recipient);
				if (recipient != null) {
					//				Debug.Log("You're looking for " + hitColliders[i].ToString());
					if (hitColliders [i].ToString () == "Left (UnityEngine.BoxCollider)" ||
						hitColliders [i].ToString () == "Right (UnityEngine.BoxCollider)" ||
						hitColliders [i].ToString () == "Middle (UnityEngine.BoxCollider)" ||
						hitColliders [i].ToString () == "Mine 1(Clone) (UnityEngine.SphereCollider)") {
						break;
					}
					recipient.ReceiveDamage (damage);
				}
				Asteroid asteroidRecipient = go.GetComponent<Asteroid> ();
				//also do for angle asteroid!!!
				if (asteroidRecipient != null) {
//					Debug.Log ("Hit an asteroid, destroyed in asteroid.cs");
//					Destroy (parent.gameObject);
					//				asteroidRecipient.ReceiveDamage(damage);
				}
				i++;
			}
			i++;
		}
	}
}