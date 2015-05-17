//
//using UnityEngine;
//using System.Collections;
//
//public class Asteroid2 : MonoBehaviour {
//
//	
//	public float		health = 1;
//	public int			score = 10; //Points earned for destroying this
//	
//	public int			showDamageForFrames = 0; // # of frames to show damage
//	public float		powerUpDropChance = 0f; // Chance to drop a power-up
//	
//	public Color[]		originalColors;
//	public Material[]	materials; //All the Materials of this & its children
//	public int			remainingDamageFrames = 0; // Damage frames left
//	
//	public Bounds bounds; //The Bounds of this and its children
//	public Vector3 boundsCenterOffset; //Distance of bounds.center from position
//	
//	
//	public GameObject explosion;
//	
//	// Rotation speed
//	public float rotationSpeed = 200.0f;
//	// Vector3 axis to rotate around
//	public Vector3 rotationalAxis = Vector3.up;	
//	// Drift/movement speed
//	public float driftSpeed = -20.0f;
//	public float speedVariation;
//	// Vector3 direction for drift/movement
//	public Vector3 driftAxis = Vector3.up;
//	
//	public float tMultiplier;
//	public Main main;
//	
//	// Private variables
//	private Transform _cacheTransform;
//	
//	void Awake() {
//		InvokeRepeating ("CheckOffscreen", 0f, 2f);
//	}
//	
//	void Start () {
//		// Cache transforms to increase performance
////		_cacheTransform = transform;
//		// Set the mesh based on poly count (quality)
//
//		speedVariation = Random.Range(0.5f, 2.0f);
//		GameObject mainObject = GameObject.Find ("Main Camera");
//		main = mainObject.GetComponent<Main> ();
//		
//		//		Debug.Log ("SU_Asteroid.cs is being used");
//		
//	}
//	
//	void Update () {
////		tMultiplier = main.timeMultiplier;
//		
//		if (transform != null) {
//			// Rotate around own axis
//			transform.Rotate(rotationalAxis * (rotationSpeed) * Time.deltaTime);
//			// Move in world space according to drift speed
//			transform.Translate(driftAxis * (speedVariation * driftSpeed) * Time.deltaTime, Space.World);
//		}
//		//		Debug.Log (speedVariation);
//	}
//
//	
//	//This variable holds a reference to the last triggering GameObject
//	public GameObject lastTriggerGo = null;
//	
//	void OnTriggerEnter(Collider other){
//		//Find the tag of other.gameObject or its parent GameObjects
//		GameObject go = Utils.FindTaggedParent (other.gameObject);
//		//If there is a parent with a tag
//		if (go != null) {
//			//Make sure it's not the same triggering go as last time
//			if (go ==lastTriggerGo){
//				return;
//			}
//			lastTriggerGo = go;
//			
//			if(go.tag == "Enemy"){
//				//Destroy the asteroid
//				Destroy(this.gameObject);
//				// Destroy the enemy
//				//Destroy(go);
//				Instantiate(explosion, transform.position, transform.rotation);
//			}else if (go.tag == "Asteroid") {
//				//Destroy the asteroid
//				Destroy(this.gameObject);
//				
//				Instantiate(explosion, transform.position, transform.rotation);
//			}else if (go.tag == "Hero") {
//				//Destroy the asteroid
//				//				Destroy(this.gameObject); Destroyed in other script
//				
//				Instantiate(explosion, transform.position, transform.rotation);
//			}else if (go.tag == "ProjectileHero") {
//				Destroy(this.gameObject);
//				Destroy(go);
//				Main.S.AsteroidDestroyed2(this);
//				Instantiate(explosion, transform.position, transform.rotation);
//			}else{
//				//Announce it
//				//				print ("Triggered: " + go.name);
//				//Make sure it's not the same triggering go as last time
//			}
//		}else {
//			//Otherwise announce the original gameObject
//			//			print ("Triggered: " + other.gameObject.name);
//		}
//	}
//	
//	void CheckOffscreen(){
//		//If bounds are still their default value...
//		if (bounds.size == Vector3.zero) {
//			//then set them
//			bounds = Utils.CombineBoundsOfChildren (this.gameObject);
//			//Also find the diff between bounds.center & transform.position
//			boundsCenterOffset = bounds.center - transform.position;
//		}
//		
//		//Every time, update the counds to the current position
//		bounds.center = transform.position + boundsCenterOffset;
//		//Check to see whether the bounds are completely offscreen
//		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
//		if (off != Vector3.zero) {
//			//If this enemy has gone off the bottom edge of the screen
//			if (off.y < 0) {
//				//then destroy it
//				Destroy (this.gameObject);
//			}
//		}
//	}
//
//	void ShowDamage() {
//		foreach (Material m in materials) {
//			m.color = Color.red;
//		}
//		remainingDamageFrames = showDamageForFrames;
//	}
//	void UnShowDamage() {
//		for (int i = 0; i < materials.Length; i++) {
//			materials[i].color = originalColors[i];
//		}
//	}
//
//}
