﻿using UnityEngine;
using System.Collections;


public class Enemy_3 : Enemy {
	// Enemy_3 uses a Sin wave to modify a 2-point linear interpolation
	public Vector3[]		points;
	public float			birthTime;
	public float			lifeTime = 10;
	// Determines how much the Sine wave will affect movement
	public float			sinEccentricity = 0.6f;
	
	void Start() {
		// Initialize the points
		points = new Vector3[2];
		
		// Find Utils.camBounds
		Vector3 cbMin = Utils.camBounds.min;
//		Debug.Log ("The min bound is at " + cbMin);
		Vector3 cbMax = Utils.camBounds.max;
//		Debug.Log ("The max bound is at " + cbMax);

		Vector3 v = Vector3.zero;
		// Pick any point on the left side of the screen
		v.x = cbMin.x - Main.S.enemySpawnPadding;
		v.y = Random.Range( cbMin.y, cbMax.y);
		points[0] = v;
//		Debug.Log ("The left point is at " + points[0]);
		
		// Pick any point on the right side of the screen
		v = Vector3.zero;
		v.x = cbMax.x + Main.S.enemySpawnPadding;
		v.y = Random.Range(cbMin.y, cbMax.y);
		points[1] = v;
//		Debug.Log ("The right point is at " + points[1]);
		
		// Possibly swap sides
		if (Random.value < 0.5f) {
			// Setting the .x of each point to its negative will move it to the other side of the screen
			points[0].x *= -1;
			points[1].x *= -1;
		}
		
		// Set the birthTime to the current time
		birthTime = Time.time;
	}
	
	public override void Move() {
		// Bezier curves work based on a u value between 0 & 1
		float u = (Time.time - birthTime) / lifeTime;
		
		// If u > 1, then it has been longer than lifeTime since birthTime
		if (u > 1) {
			// This Enemy_3 has finished its lifeTime
			Destroy( this.gameObject);
			return;
		}
		
		// Adjust u by adding an easing curve based on a Sine wave
		u = u + sinEccentricity*(Mathf.Sin(u*Mathf.PI*2));
		
		// Interpolate the two linear interpolation points
		pos = (1-u)* points[0] + u * points [1];
	}
}