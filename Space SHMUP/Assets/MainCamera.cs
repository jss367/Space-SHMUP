using UnityEngine;
using System.Collections;
using System.Linq;

public class MainCamera : MonoBehaviour {

	public enum EasingType {
		linear,
		easeIn,
		easeOut,
		easeInOut,
		sin,
		sinIn,
		sinOut
	}

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

	public EasingType		easingType = EasingType.linear;
	public float			easingMod = 2;

	public Quaternion		r01;
	public Quaternion		r02;
		
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
//		Debug.Log ("Number of moons: " + moons.Length);

//		celestialBodies[] = new GameObject[planets.Length + stars.Length + moons.Length];
//		celestialBodies[] = planets.Concat(stars).ToArray;
		closestObject = planets [0];
//		Debug.Log(celestialBodies.Length);

//		Vector3 bodss = moons [1].transform.position;
//		Debug.Log (bodss);
//		Vector3 bods = celestialBodies[2].transform.position;
//		Debug.Log(bods);
	}

		
		void Update () {

		Vector3 p01 = (1 - u) * c0.position + u * c1.position;
		if (Time.time > 10) {
			p01 = (1 - u) * c1.position + u * c2.position;
		} else if (Time.time > 20) {
			p01 = (1 - u) * c2.position + u * c3.position;

		}
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

		//Easing functions
		u = EaseU (u, easingType, easingMod);

//		planetPositions = GameObject.FindGameObjectsWithTag ("Planet").transform.position;

		// 4-point Bezier curve calculation
//		Vector3 p01, p12, p23, p012, p123;


//		p12 = (1 - u) * c1.position + u * c2.position;
//		p23 = (1 - u) * c2.position + u * c3.position;
//
//		p012 = (1 - u) * p01 + u * p12;
//		p123 = (1 - u) * p12 + u * p23;
//
//		p0123 = (1 - u) * p012 + u * p123;

//		transform.position = p0123;
		transform.position = p01;

		GetClosestObject ();
		r02 = Quaternion.LookRotation (closestObject.transform.position);

		r01 = Quaternion.Slerp (c0.rotation, r02, u);
		transform.rotation = r01;
//		transform.LookAt (closestObject.transform);

		}

	void GetClosestObject(){

		for (int i = 0; i < planets.Length; i++) {
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

	public float EaseU(float u, EasingType eType, float eMod){
		float u2 = u;
		
		switch (eType) {
		case EasingType.linear:
			u2 = u;
			break;
			
		case EasingType.easeIn:
			u2 = Mathf.Pow (u, eMod);
			break;
			
		case EasingType.easeOut:
			u2 = 1 - Mathf.Pow (1 - u, eMod);
			break;
			
		case EasingType.easeInOut:
			if (u <= 0.5f) {
				u2 = 0.5f * Mathf.Pow (u * 2, eMod);
			} else {
				u2 = 0.5f + 0.5f * (1 - Mathf.Pow (1 - (2 * (u - 0.5f)), eMod));
			}
			break;
			
		case EasingType.sin:
			// Try eMod values of 0.16f and -0.2f for EasingType.sin
			u2 = u + eMod + Mathf.Sin (2 * Mathf.PI * u);
			break;
			
		case EasingType.sinIn:
			// eMod is ignored for SinIn
			u2 = 1 - Mathf.Cos (u * Mathf.PI * 0.5f);
			break;
			
		case EasingType.sinOut:
			// eMod is ignored for SinOut
			u2 = Mathf.Sin (u * Mathf.PI * 0.5f);
			break;
		}
		
		return (u2);
	}

}