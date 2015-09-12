using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public GameObject mineExplosion;

	void OnTriggerEnter (Collider coll){
		Debug.Log ("Mine has been triggered by " + coll);
		Destroy (this.gameObject);
		Destroy(coll.gameObject);
		Instantiate(mineExplosion, transform.position, transform.rotation);
}
}