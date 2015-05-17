//using UnityEngine;
//using System.Collections;
//
//public class Angle2 : Asteroid2 {
//
//	public Vector3 orthogAxis;
//	public float orthogSpeed = 10.0f;
//	
//
//
//	void Start () {
//
//		if (transform.position.x > 0) {
//			orthogAxis = Vector3.left;
//		} else {
//			orthogAxis = Vector3.right;
//		}
//		
//		score = 40;
//	}
//	
//	void Update () {
//
////		tMultiplier = main.timeMultiplier;
//		
//		if (transform != null) {
//			// Rotate around own axis
//			transform.Rotate(rotationalAxis * (rotationSpeed) * Time.deltaTime);
//			// Move in world space according to drift speed
//			transform.Translate(driftAxis * (driftSpeed) * Time.deltaTime, Space.World);
//			transform.Translate(orthogAxis * (orthogSpeed) * Time.deltaTime, Space.World);
//		}
//		
//	}
//}
