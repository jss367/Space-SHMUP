﻿using UnityEngine;
using System.Collections;


public class Enemy_2 : Enemy{
	
	// Enemy_2 will move following a Bezier curve, which is a linear interpolation between more than two points
	public Vector3[]		points;
	public float			birthTime;
	public float			lifeTime = 10;
	public GameObject hero;
	
	// Again, Start works well ecause it is not used by Enemy
	void Start() {

		hero = (GameObject)GameObject.FindWithTag ("Hero");
		points = new Vector3[3]; // Initialize points
		// The start position has already been set by Main.SpawnEnemy()
		points[0] = pos;
		
		// Set xMin and xMax the same way that Main.SpawnEnemy() does
		float xMin = Utils.camBounds.min.x + Main.S.enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - Main.S.enemySpawnPadding;
		
		Vector3 v;
//		// Pick a random middle position in the bottom half of the screen
//		v = Vector3.zero;
//		v.x = Random.Range( xMin, xMax);
//		v.y = Random.Range(Utils.camBounds.min.y, 0);
//		Debug.Log (hero);
		if (hero != null) {
			v = hero.transform.position;
		}
		else {
			v = Vector3.zero;
		}
		points[1] = v;
		
		// Pick a random final position in the bottom half of the screen
		v = Vector3.zero;
		v.y = Random.Range(Utils.camBounds.min.y, 0);
		v.x = Random.Range(xMin, xMax);
		points[2] = v;
		
		// Set the birthTime to the current time
		birthTime = Time.time;
	}
	
	public override void Move() {
		// Bezier curves work based on a u value between 0 & 1
		float u = (Time.time - birthTime) / lifeTime;
		
		if (u > 1) {
			// This Enemy_4 has finished its life
			Destroy(this.gameObject);
			return;
		}
		
		// Interpolate the three Bezier curve points
		Vector3 p01, p12;
		p01 = (1-u) * points[0] + u*points[1];
		p12 = (1-u) * points[1] + u*points[2];
		pos = (1-u) * p01 + u*p12;
	}
}