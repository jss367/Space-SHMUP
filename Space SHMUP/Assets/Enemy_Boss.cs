using UnityEngine;
using System.Collections;

// Part is another serializable data storage class just like WeaponDefinition
[System.Serializable]
//public class Part{
//	// These three fields need to be defined in the Inspector pane
//	public string	name;		// The name of the part
//	public float	health;		// The amount of health this part has
//	public string[]	protectedBy;	// The other parts that protect this
//	
//	// These two fields are set automatically in Start()
//	// Caching like this makes it faster and easier to find these later
//	public GameObject go;		// The GameObject of this part
//	public Material mat;		// The Material to show damage
//}


public class Enemy_Boss : MonoBehaviour {
	// Enemy_4 will start offscreen and then pick a random point on screen to move to. Once it has arrived, it will pick another
	// random point and continue until that player has shot it down.

	public float				duration = 0.5f; // Duration of the time between points
//	public float		speed = 20f; //The speed in m/s
	public float		fireRate = 0.3f; // Seconds per shot (Unused)
	public float		health = 10;
	public int			score = 100; //Points earned for destroying this
	
	public int			showDamageForFrames = 2; // # of frames to show damage
	public float		powerUpDropChance = 1f; // Chance to drop a power-up
	public bool _________________;
	
	public GameObject	impact;
	
	public Color[]		originalColors;
	public Material[]	materials; //All the Materials of this & its children
	public int			remainingDamageFrames = 0; // Damage frames left
	
	public Bounds bounds; //The Bounds of this and its children
	public Vector3 boundsCenterOffset; //Distance of bounds.center from position
	
	private float tMultiplier;
	private Main main;
	
	public GameObject enemyExplosion;
	public GameObject popText;
	
	public Vector3[]			points; // Stores the p0 and p1 for interpolation
	public float				timeStart; // Birth time for this Enemy_4

	
	public Part[]				parts;		// The array of ship Parts

	void Awake() {
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	void Start () {

		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}

		points = new Vector3[2];
		// There is already an initial position chosen by Main.SpawnEnemy()
		// so add it to points as the initial p0 & p1
		points [0] = pos;
		points [1] = pos;
		
		InitMovement ();
		
		// Cache GameObject and Material of each Part in parts
		Transform t;
		foreach (Part prt in parts) {
			t = transform.Find(prt.name);
			if (t != null) {
				prt.go = t.gameObject;
				prt.mat = prt.go.GetComponent<Renderer>().material;
			}
		}
	}

	void Update(){
		Move();
	}

	//This is a Property: A method that acts like a field
	public Vector3 pos{
		get{
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}

	void InitMovement()
	{
		// Pick a new point to move to that is on screen
		Vector3 p1 = Vector3.zero;
		float esp = Main.S.enemySpawnPadding;
		Bounds cBounds = Utils.camBounds;
		p1.x = Random.Range (cBounds.min.x + esp, cBounds.max.x - esp);
		p1.y = Random.Range (cBounds.min.y + esp, cBounds.max.y - esp);
		
		points [0] = points [1];	// Shift points[1] to points[0]
		points [1] = p1;			// Add p1 as points[1]
		
		// Reset the time
		timeStart = Time.time;
	}
	
	public void Move() {
		// This completely overrides Enemy.Move() with a linear interpolation
		
		float u = (Time.time - timeStart) / duration;
		if (u >= 1) { // if u >=1
			InitMovement ();	// .. the initialize movement to a new point
			u = 0;
		}
		
		u = 1 - Mathf.Pow (1 - u, 4);		// Apply Ease Out easing to u
		
		pos = (1 - u) * points [0] + u * points [1];	// Simple linear interpolation
	}

	void CheckOffscreen(){
		//If bounds are still their default value...
		if (bounds.size == Vector3.zero) {
			//then set them
			bounds = Utils.CombineBoundsOfChildren (this.gameObject);
			//Also find the diff between bounds.center & transform.position
			boundsCenterOffset = bounds.center - transform.position;
		}
		
		//Every time, update the counds to the current position
		bounds.center = transform.position + boundsCenterOffset;
		//Check to see whether the bounds are completely offscreen
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
		if (off != Vector3.zero) {
			//If this enemy has gone off the bottom edge of the screen
			if (off.y < 0) {
				//then destroy it
				Destroy (this.gameObject);
			}
		}
	}

	void OnTriggerEnter( Collider coll) {
		Debug.Log ("Enemy boss trigger");
		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ProjectileHero":
			Debug.Log ("Hero projectile hit enemy boss");
			Projectile p = other.GetComponent<Projectile> ();
			Destroy (other);
			Instantiate(impact, transform.position, transform.rotation);
			// Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= Main.W_DEFS [p.type].damageOnHit;
			if (health <= 0) {
				// Destroy this Enemy
				Destroy (this.gameObject);
				Instantiate(enemyExplosion, transform.position, transform.rotation);
			}

			break;
//		case default:
//			Debug.Log("EnemyBoss has been hit");
		}
	}

//	}

	
	bool Destroyed(Part prt) {
		if (prt == null) { // If no real Part was passed in
			return (true);	// Return true (meaning yes, it was destroyed)
		}
		// Returns the result of the comparison: prt.health <= 0
		// If prt.health is 0 or less, returns true (yes, it was destroyed)
		return (prt.health <= 0);
	}
	
	// This changes the color of just one Part to red instead of the while ship
	void ShowLocalizedDamage(Material m) {
		m.color = Color.red;
		remainingDamageFrames = showDamageForFrames;
	}
	
	
}
