using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

	// Rotation speed
	public float rotationSpeed = 200.0f;
	// Vector3 axis to rotate around
	public Vector3 rotationalAxis = Vector3.forward;	
	
	void Update () {
		
		if (transform != null) {
			// Rotate around own axis
			transform.Rotate(rotationalAxis * (rotationSpeed) * Time.deltaTime);
		}
		//		Debug.Log (speedVariation);
	}
}
