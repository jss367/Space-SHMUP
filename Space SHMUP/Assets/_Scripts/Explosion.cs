using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {
	public float destroyAfterSeconds = 8.0f;
	
	void Awake() {
		// Destroy gameobject after delay
		Destroy(gameObject, destroyAfterSeconds);
	}
}
