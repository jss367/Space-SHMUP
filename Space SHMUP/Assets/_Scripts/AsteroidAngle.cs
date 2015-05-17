using UnityEngine;
using System.Collections;

public class AsteroidAngle : MonoBehaviour {

	public float		health = 1;
	public int			score = 10; //Points earned for destroying this
	
	public int			showDamageForFrames = 0; // # of frames to show damage
	public float		powerUpDropChance = 0f; // Chance to drop a power-up
	
	public Color[]		originalColors;
	public Material[]	materials; //All the Materials of this & its children
	public int			remainingDamageFrames = 0; // Damage frames left
	
	public Bounds bounds; //The Bounds of this and its children
	public Vector3 boundsCenterOffset; //Distance of bounds.center from position
	
	
	public GameObject explosion;
	
	// Rotation speed
	public float rotationSpeed = 200.0f;
	// Vector3 axis to rotate around
	public Vector3 rotationalAxis = Vector3.up;	
	// Drift/movement speed
	public float driftSpeed = -20.0f;
	// Vector3 direction for drift/movement
	public Vector3 driftAxis = Vector3.up;
	public Vector3 orthogAxis;
	public float orthogSpeed = 10.0f;

	private float tMultiplier;
	private Main main;
	
	// Private variables
	private Transform _cacheTransform;
	
	void Start () {
		// Cache transforms to increase performance
		_cacheTransform = transform;

		if (transform.position.x > 0) {
			orthogAxis = Vector3.left;
		} else {
			orthogAxis = Vector3.right;
		}

/*
		//gameController = GameObject.Find("GameController").GetComponent<GameController>();
		GameObject gameControllerObject = GameObject.Find("GameController");
		//	Debug.Log("gameControllerObject is: " + gameControllerObject);
		
		gameController = gameControllerObject.GetComponent<GameController>();
		//	Debug.Log("gameController is: " + gameController);
*/
		GameObject mainObject = GameObject.Find ("Main Camera");
		main = mainObject.GetComponent<Main> ();
	}
	
	void Update () {
		tMultiplier = main.timeMultiplier;
		
		if (_cacheTransform != null) {
			// Rotate around own axis
			_cacheTransform.Rotate(rotationalAxis * (rotationSpeed + tMultiplier) * Time.deltaTime);
			// Move in world space according to drift speed
			_cacheTransform.Translate(driftAxis * (driftSpeed - tMultiplier) * Time.deltaTime, Space.World);
			_cacheTransform.Translate(orthogAxis * (orthogSpeed) * Time.deltaTime, Space.World);
		}
		
	}
	
	void OnCollisionEnter (Collision coll) {
		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ProjectileHero":
			Projectile p = other.GetComponent<Projectile> ();
			// Asteroids don't take damage unless they're onscreen
			// This stops the player from shooting them before they are visible
			bounds.center = transform.position + boundsCenterOffset;
			if (Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) { //removed "bounds.extents == Vector3.zero ||" ... be careful
				Destroy (other);
				break;
			}
			// Hurt this Asteroid
			ShowDamage();
			// Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= Main.W_DEFS [p.type].damageOnHit;
			if (health <= 0) {
				// Tell the Main singleton that this ship has been destroyed
				//Main.S.ShipDestroyed(this);
				// Destroy this Asteroid
				Destroy (this.gameObject);
				Instantiate(explosion, transform.position, transform.rotation);
			}
			Destroy (other);
			break;
		}
	}
	
	//This variable holds a reference to the last triggering GameObject
	public GameObject lastTriggerGo = null;
	
	void OnTriggerEnter(Collider other){
		//Find the tag of other.gameObject or its parent GameObjects
		GameObject go = Utils.FindTaggedParent (other.gameObject);
		//If there is a parent with a tag
		if (go != null) {
			//Make sure it's not the same triggering go as last time
			if (go ==lastTriggerGo){
				return;
			}
			lastTriggerGo = go;
			
			if(go.tag == "ProjectileHero"){
				//Destroy the asteroid
				Destroy(this.gameObject);
				// Destroy the enemy
				//Destroy(go);
				Instantiate(explosion, transform.position, transform.rotation);
			}else if (go.tag == "Asteroid") {
				//Destroy the asteroid
				Destroy(this.gameObject);
				
				Instantiate(explosion, transform.position, transform.rotation);
			}else if (go.tag == "Enemy") {
				Destroy(this.gameObject);
				//Main.S.AsteroidDestroyed(this);
				Instantiate(explosion, transform.position, transform.rotation);
			}else if (go.tag == "Hero") {
				//Destroy the asteroid
				//				Destroy(this.gameObject); Destroyed in other script
				
				Instantiate(explosion, transform.position, transform.rotation);
			}else{
				//Announce it
				print ("Triggered: " + go.name);
				//Make sure it's not the same triggering go as last time
			}
		}else {
			//Otherwise announce the original gameObject
			print ("Triggered: " + other.gameObject.name);
		}
	}
	
	
	void ShowDamage() {
		foreach (Material m in materials) {
			m.color = Color.red;
		}
		remainingDamageFrames = showDamageForFrames;
	}
	void UnShowDamage() {
		for (int i = 0; i < materials.Length; i++) {
			materials[i].color = originalColors[i];
		}
	}

	
}
