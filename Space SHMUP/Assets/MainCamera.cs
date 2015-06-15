using UnityEngine;
using System.Collections;
using System.Linq;

public class MainCamera : MonoBehaviour {

	public Transform		c0, c1, c2, c3;
	public float			timeDuration = 15;
	// Set checktoCalculate to true to start moving
	public bool				checkToCalculate = false;
	public GameObject		camera;

	public bool ______________;

	public float			u;
	public Vector3			p0123;
	public bool				moving = false;
	public float			timeStart;
	public GameObject[]		planets;
	public GameObject[]		stars;
	public GameObject[]		moons;
	public GameObject[]		celestialBodies;
	public GameObject		closestObject;
	public Vector3[]		planetPositions;

		
//	private Vector3 randrotSpeed;
//		public Vector3 rotationSpeed = new Vector3 (5,5,5);	
//		
//	private Vector3 randVelocity;
//	public Vector3 velocity = new Vector3 (0,0,5);	


	void Start() {
//		randrotSpeed = Vector3.Scale (rotationSpeed, Random.onUnitSphere);
		//randVelocity = Vector3.Scale (velocity, Random.onUnitSphere);
//		velocity = Vector3 (0,0,50);

		planets = GameObject.FindGameObjectsWithTag ("Planet");
		stars = GameObject.FindGameObjectsWithTag ("Star");
		moons = GameObject.FindGameObjectsWithTag ("Moon");
		Debug.Log (moons.Length);

//		celestialBodies[] = new GameObject[planets.Length + stars.Length + moons.Length];
//		celestialBodies[] = planets.Concat(stars).ToArray;
		closestObject = planets [0];
//		Debug.Log(celestialBodies.Length);

		Vector3 bodss = moons [1].transform.position;
		Debug.Log (bodss);
//		Vector3 bods = celestialBodies[2].transform.position;
//		Debug.Log(bods);
	}

		
		void Update () {		
//		Vector3 pos = transform.position;
//		pos += velocity * Time.deltaTime;
//		transform.position = pos;
//
//		transform.Rotate (randrotSpeed * Time.deltaTime );

		u = (Time.time - timeStart) / timeDuration;
		if (u > 1) {
			u = 1;
			moving = false;
		}

//		planetPositions = GameObject.FindGameObjectsWithTag ("Planet").transform.position;

		// 4-point Bezier curve calculation
		Vector3 p01, p12, p23, p012, p123;

		p01 = (1 - u) * c0.position + u * c1.position;
		p12 = (1 - u) * c1.position + u * c2.position;
		p23 = (1 - u) * c2.position + u * c3.position;

		p012 = (1 - u) * p01 + u * p12;
		p123 = (1 - u) * p12 + u * p23;

		p0123 = (1 - u) * p012 + u * p123;

		transform.position = p0123;

		GetClosestObject ();

		transform.LookAt (closestObject.transform);
		}

	void GetClosestObject(){


		Vector3 cams = Camera.main.gameObject.transform.position;
//		Vector3 bods = (celestialBodies [i].transform.position);

		for (int i = 0; i < planets.Length; i++) {
//			Vector3 bods = celestialBodies[2].transform.position;
//			Debug.Log(bods);
			if (Vector3.Distance(
							Camera.main.gameObject.transform.position, 
			                     planets [i].transform.position) < 
						    Vector3.Distance(Camera.main.gameObject.transform.position, 
			                 closestObject.transform.position)){
			    closestObject = planets[i];
			    }
			    // might compare old value of closest body!
			}
//		Debug.Log("Closest obejct is " + closestObject);

	}

}