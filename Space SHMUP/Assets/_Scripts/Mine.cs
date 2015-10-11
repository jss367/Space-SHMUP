using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour {

	public GameObject mineExplosion;

	void OnTriggerEnter (Collider coll){
		if (coll.ToString() != "Shield (UnityEngine.SphereCollider)" ){

		Debug.Log ("Mine has been triggered by " + coll);
			GameObject parent = Utils.FindTaggedParent(coll);
			Debug.Log ("The parent is " + parent);

			Destroy (this.gameObject);
		Destroy(parent.gameObject);
		Instantiate(mineExplosion, transform.position, transform.rotation);
}
}
}