using UnityEngine;
using System.Collections;

public class NebulaMover : MonoBehaviour {

	// Rotation speed
	public float rotationSpeed = 200.0f;
	// Vector3 axis to rotate around
	public Vector3 rotationalAxis = Vector3.up;	
	// Drift/movement speed
	public float driftSpeed = -20.0f;
	public float speedVariation;
	// Vector3 direction for drift/movement
	public Vector3 driftAxis = Vector3.up;

	// Use this for initialization
	void Start () {
	
	}
	
	void Update () {

		if (transform != null) {
			// Rotate around own axis
			transform.Rotate(rotationalAxis * (rotationSpeed) * Time.deltaTime);
			// Move in world space according to drift speed
			transform.Translate(driftAxis * (driftSpeed) * Time.deltaTime, Space.World);
		}
;
	}
}
