using UnityEngine;
using System.Collections;

public class ParticleMover : MonoBehaviour {

	public float averagePosition;
	public float xRange = 30.0f;
	public float zRange = 10.0f;

	void Update() {

		Vector3 pos = transform.position;
		pos.x = Random.Range (-xRange, xRange);
		pos.z = averagePosition + Random.Range (-zRange, zRange);
		transform.position = pos;

	}

}