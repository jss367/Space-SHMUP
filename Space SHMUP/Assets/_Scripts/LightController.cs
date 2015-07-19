using UnityEngine;
using System.Collections;

public class LightController : MonoBehaviour {

	public float timeToLive = .1f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, timeToLive);
	}

}
