using UnityEngine;
using System.Collections;

public class GalaxyManager : MonoBehaviour {

	public float rotSpeed = 5;

	// Use this for initialization
	void Start () {
	
	}
	

	void Update(){
		transform.Rotate(0.0f, 0.0f, rotSpeed * Time.deltaTime, Space.World);
	}
}
