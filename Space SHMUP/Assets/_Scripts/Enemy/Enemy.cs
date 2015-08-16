using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float		speed = 20f; //The speed in m/s
	public float		fireRate = 0.3f; // Seconds per shot (Unused)
	public float		health = 10;
	public int			score = 100; //Points earned for destroying this

	public int			showDamageForFrames = 2; // # of frames to show damage
	public float		powerUpDropChance = 1f; // Chance to drop a power-up
	public float		missileDropChance = .1f; // Chance to drop a missile if possible
	public bool _________________;
	private float lastTimeDestroyed = 0.0f;
	public float comboTime = 1.0f;

	public GameObject	impact;

	public Color[]		originalColors;
	public Material[]	materials; //All the Materials of this & its children
	public int			remainingDamageFrames = 0; // Damage frames left

	public Bounds bounds; //The Bounds of this and its children
	public Vector3 boundsCenterOffset; //Distance of bounds.center from position

//	private float tMultiplier;
	private Main main;

	public GameObject enemyExplosion;
	public GameObject popText;
	public GameObject comboPopText;
	
	void Awake() {
//		materials = Utils.GetAllMaterials (gameObject);
//		originalColors = new Color[materials.Length];
//		for (int i = 0; i < materials.Length; i++) {
//			originalColors [i] = materials [i].color;
//		}
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	void Start() {
		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}
	}

	//Update is called once per frame
	void Update(){

		if (main != null) {
			//	Debug.Log("gameController does exist");
			//Debug.Log (tMultiplier);
		}
		if (main == null) {
			//Debug.Log ("Cannot find 'main'");
		}

		Move();
		if (remainingDamageFrames > 0) {
			remainingDamageFrames--;
			if (remainingDamageFrames == 0) {
				UnShowDamage ();
			}
		}
	}

	
	public virtual void Move(){
		Vector3 tempPos = pos;
//		float mod = Mathf.Sqrt
		tempPos.y -= (speed/5*2*Mathf.Sqrt(.5f+(float)health)) * Time.deltaTime;
		pos = tempPos;
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

	void CheckOffscreen(){
		//If bounds are still their default value...
		if (bounds.size == Vector3.zero) {
			//then set them
			bounds = Utils.CombineBoundsOfChildren (this.gameObject);
			//Also find the diff between bounds.center & transform.position
			boundsCenterOffset = bounds.center - transform.position;
		}
		
		//Every time, update the bounds to the current position
		bounds.center = transform.position + boundsCenterOffset;
//		Debug.Log ("bounds.center is " + bounds.center);
		//Check to see whether the bounds are completely offscreen
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen);
//		Debug.Log ("off is " + off);
		if (off != Vector3.zero) {
			//If this enemy has gone off the bottom edge of the screen
			if (off.y < 0) {
				//then destroy it
				Destroy (this.gameObject);
			}
		}
	}

	public void ReceiveDamage(float damage){
//		Debug.Log ("Enemy has received damage");
		health -= damage;
		if (health <= 0) {
			// Destroy this Enemy
			Destroy (this.gameObject);
			if (Time.time - lastTimeDestroyed < comboTime)
			{
				// Tell the Main singleton that this ship has been destroyed
				Main.S.EnemyDestroyed(this, true);
				Instantiate(comboPopText, transform.position, Quaternion.identity);
			}
			else {
				// Tell the Main singleton that this ship has been destroyed
				Main.S.EnemyDestroyed(this, false);
				Instantiate(popText, transform.position, Quaternion.identity);
			}
			lastTimeDestroyed = Time.time;
			//				Debug.Log("lastTimeDestroyed is " + lastTimeDestroyed);
			Instantiate(enemyExplosion, transform.position, transform.rotation);
			
		}
	}


	void OnCollisionEnter (Collision coll) {
		GameObject other = coll.gameObject;
//		Debug.Log ("Enemy hit a " + other.tag);
		switch (other.tag) {
		case "ProjectileHero":
//			Debug.Log ("Projectile has hit enemy");
			Projectile p = other.GetComponent<Projectile> ();
			// Enemies don't take damage unless they're onscreen
			// This stops the player from shooting them before they are visible
			bounds.center = transform.position + boundsCenterOffset;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) {

				Destroy (other);
				break;
			}
			// Hurt this Enemy
			ShowDamage();
			Instantiate(impact, transform.position, transform.rotation);
			// Get the damage amount from the Projectile.type & Main.W_DEFS
			ReceiveDamage(Main.W_DEFS [p.type].damageOnHit);

			Destroy (other);
			break;
		case "Asteroid":
//			Debug.Log("Hit an asteroid");
			// Enemies don't take damage unless they're onscreen
			// This stops the player from shooting them before they are visible
			bounds.center = transform.position + boundsCenterOffset;
			if (Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) { // removed "bounds.extents == Vector3.zero || "
				Destroy (other);
				break;
			}
			// Hurt this Enemy
			ShowDamage();
			// Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= 1; // Asteroids do 1 worth of damage
			if (health <= 0) {
				// Tell the Main singleton that this ship has been destroyed
				//	Main.S.EnemyDestroyed(this);
				// Destroy this Enemy
				Destroy (this.gameObject);
			}
			Destroy (other);
			break;

		case "Hero":
			Instantiate(enemyExplosion, transform.position, transform.rotation);
			//Destroyed in the hero script
			break;

		}

	}

	void OnTriggerEnter (Collider coll){
		GameObject other = coll.gameObject;
//		Debug.Log ("Enemy hit a " + other.tag);
		switch (other.tag) {
		case "Hero":
			//			Debug.Log ("Projectile has hit enemy");
			Projectile p = other.GetComponent<Projectile> ();
			// Enemies don't take damage unless they're onscreen
			// This stops the player from shooting them before they are visible
			bounds.center = transform.position + boundsCenterOffset;
			if (bounds.extents == Vector3.zero || Utils.ScreenBoundsCheck (bounds, BoundsTest.offScreen) != Vector3.zero) {

				break;
			}
			// Destroy this Enemy
			Destroy(this.gameObject);
			
			Instantiate(enemyExplosion, transform.position, transform.rotation);
			break;
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