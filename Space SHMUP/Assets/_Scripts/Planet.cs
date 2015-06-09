// Planet C# Script (version: 1.02)
// SPACE UNITY - Space Scene Construction Kit
// http://www.spaceunity.com
// (c) 2013 Stefan Persson

// DESCRIPTION:
// Script for the rotational behaviour of planets.

// INSTRUCTIONS:
// This script is attached to the planet prefabs and rotation speed around its own axis can be configured.
// The SpaceSceneConstructionKit window will automatically configure random rotation speed.

// PARAMETERS:
//  planetRotation	(The rotational vector (axis and speed) of the planet)

// Version History
// 1.02 - Prefixed with SU_Planet to avoid naming conflicts.
// 1.01 - Initial Release.

using UnityEngine;
using System.Collections;



public class Planet : MonoBehaviour {
	// Planet rotation vector specifying axis and rotational speed
	public Vector3 planetRotation;
	// Private variables
	private Transform _cacheTransform;
	public bool isTimer = true;
	private float timeLimit;
	private float speed;
	public Vector2 velocity;
	
	void Start () {
		// Cache reference to transform to improve performance
		_cacheTransform = transform;	

		timeLimit = GameObject.Find ("Beat").GetComponent<AudioManager> ().timeLimit;

		if (isTimer) {
			Vector3 pos = transform.position;
			pos.y = 30.0f;
			transform.position = pos;
			speed = 65.0f/timeLimit;
		}
	}
	
	void Update () {
		// Rotate the planet based on the rotational vector
		if (_cacheTransform != null) {			
			_cacheTransform.Rotate(planetRotation * Time.deltaTime);
		}


	}

	void FixedUpdate(){
	
		Vector3 pos = transform.position;
		pos.x += velocity.x * .02f;
	

		if (isTimer) {
			pos.y -= speed * .02f;
		} else {
			pos.y += velocity.y * .02f;
		}

		transform.position = pos;

	}

}
