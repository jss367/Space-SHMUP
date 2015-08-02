using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public GameObject mineExplosion;

	void OnTriggerEnter (Collider coll){
		Destroy (this);
//			Destroy(coll);
		Instantiate(mineExplosion, transform.position, transform.rotation);
}
}