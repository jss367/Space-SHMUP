using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public GameObject hero;
	public bool isAimed = false;
	public float speed;

	void Awake(){
		//Test to see whether this has passed off screen every 2 seconds
		InvokeRepeating ("CheckOffscreen", 2f, 2f);
	}
		
		void Start ()
		{
		hero = (GameObject)GameObject.FindWithTag ("Hero");
			GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}

	void CheckOffscreen() {
		if (Utils.ScreenBoundsCheck (GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero) {
			Destroy (this.gameObject);
		}
	}

	}