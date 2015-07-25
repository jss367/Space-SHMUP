using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Rotation speed
	public float rotationSpeed = 200.0f;
	// Vector3 axis to rotate around
	public Vector3 rotationalAxis = Vector3.forward;
	public bool makeRandom = false;
	private float finalSpeed = 200.0f;

	void Start(){
		if (makeRandom) {
			finalSpeed = Random.Range(-1, 1) * rotationSpeed;
		} else {
			finalSpeed = rotationSpeed;
		}
	}

	void Update () {
		
		if (transform != null) {


			// Rotate around own axis
			transform.Rotate(rotationalAxis * (finalSpeed) * Time.deltaTime);
		}
		//		Debug.Log (speedVariation);
	}
}
