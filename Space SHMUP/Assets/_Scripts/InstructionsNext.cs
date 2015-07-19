using UnityEngine;
using System.Collections;

public class InstructionsNext : MonoBehaviour {

	public int introTime = 5;

	// Use this for initialization
	void Start () {
		StartCoroutine(DeleteText ());
		}
	
	// Update is called once per frame
	IEnumerator DeleteText () {
//		Debug.Log ("DeleteText has been called");
		yield return new WaitForSeconds (introTime);
//		Debug.Log ("DeleteText has waited");
		GameObject.Destroy (gameObject);
//		this.enabled = false;
//		this.isActiveAndEnabled = false;
	}
}
