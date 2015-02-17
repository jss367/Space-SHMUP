using UnityEngine;
using System.Collections;

public class ParticleMover : MonoBehaviour {

	public float averagePosition;

	void Update() {

		Vector3 pos = transform.position;
		pos.x = Random.Range (-6.0F, 6.0F);
		pos.z = averagePosition + Random.Range (-2.0F, 2.0F);
		transform.position = pos;

	}

}