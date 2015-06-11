using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

		
	private Vector3 randrotSpeed;
		public Vector3 rotationSpeed = new Vector3 (5,5,5);	
		
	private Vector3 randVelocity;
	public Vector3 velocity = new Vector3 (0,0,5);	


	void Start() {
		randrotSpeed = Vector3.Scale (rotationSpeed, Random.onUnitSphere);
		//randVelocity = Vector3.Scale (velocity, Random.onUnitSphere);
//		velocity = Vector3 (0,0,50);	
	}

		
		void Update () {		
		Vector3 pos = transform.position;
		pos += velocity * Time.deltaTime;
		transform.position = pos;

		transform.Rotate (randrotSpeed * Time.deltaTime );
		}
	}