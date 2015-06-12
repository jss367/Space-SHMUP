using UnityEngine;
using System.Collections;
using Soomla;

public class Hero : MonoBehaviour {

	static public Hero S; //S for singleton


	public float gameRestartDelay = 2f;
	
	//These fields control the movement of the ship
	public float				speed = 30;
	public float				rollMult = -45;
	public float				pitchMult = 30;
	
	//Ship status information
	[SerializeField]
	private float 				_shieldLevel = 1;

	// Weapon fields
	public Weapon[]				weapons;

	public bool _____________;
//
//	public const string BLASTER_WEAPON_ITEM_ID = "weapon_blaster";
//	public const string SPREAD_WEAPON_ITEM_ID = "weapon_spread";

		public bool spreadOwned = false;

	public Bounds				bounds;
	private int					weaponCount;
//	public int			showDamageForFrames = 2; // # of frames to show damage
//	public int			remainingDamageFrames = 0; // Damage frames left
//	public Color[]		originalColors;
//	public Material[]	materials; //All the Materials of this & its children
	//private int					recall;
	private int					blasterRecall;
	private int					spreadRecall;
	private int					ballRecall;

	private bool				shieldCounter = true;
	public bool					shieldUpgradeOwned = false;
//	public const string SHIELD_UPGRADE_1 = "shield_1";
	public bool isInvincible;

	
	//Declare a new delegate type WeaponFireDelegate
	public delegate void WeaponFireDelegate();
	// Create a WeaponFireDelegate field named fireDelegate.
	public WeaponFireDelegate fireDelegate;

	//Below is from Space Shooter
	public Vector3 target;
	public float tilt = 5;
	public float velocityLag = .3f;
	public float dampingRadius = 2.5f;
	//Above is from Space Shooter

	public SimpleTouchPad touchPad;
	public FireButton fireButton;

	public GameObject explosion;
	public GameObject enemyExplosion;

	void Awake(){
		S = this; //Set the singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	void Start() {
		//		spreadOwned = true; // comment out for builds
//		Soomla.Store.SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets()); // comment this out in builds

		// Reset the weapons to start _Hero with 1 blaster
		ClearWeapons ();
		CheckInventory ();

		if (spreadOwned == true) {
			weapons [0].SetType (WeaponType.spread);
		} else {

			weapons [0].SetType (WeaponType.blaster);
		}
			}

	

	// Update is called once per frame
	void Update () {
		//Pull in information from the Input class
		float xAxis = Input.GetAxis ("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		Vector3? touchPos = null;

		if (Input.mousePresent && Input.GetMouseButton (0)) 
		{
			touchPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
		} 
		else if (Input.touchCount > 0)  //should i add an if to see if the phase is stationary?
		{
			touchPos = new Vector3 (Input.touches [0].position.x, Input.touches [0].position.y, 0.0f);
		}
//		Debug.Log ("The touchPos is " + touchPos);

		if (touchPos != null)
		{
			target = Camera.main.ScreenToWorldPoint(touchPos.Value);
			target.z = GetComponent<Rigidbody>().position.z;
		}
		
		Vector3 offset = target - GetComponent<Rigidbody>().position;

//		Debug.Log ("The offset is " + offset);


		float magnitude = offset.magnitude;
		if(magnitude > dampingRadius)
		{
			magnitude = dampingRadius;
		}
		float dampening = magnitude / dampingRadius;
		
		Vector3 desiredVelocity = offset.normalized * speed * dampening; // commented to try touchpad

		//Trying touchpad below
//		Vector2 direction = touchPad.GetDirection ();
//		Debug.Log ("The direction in Hero is " + direction);
//		Debug.Log ("The x direction in Hero is " + direction.x);
//		Vector3 movement = new Vector3 (direction.x, direction.y, 0.0f);
//		Debug.Log ("The movement in Hero is " + movement);
//		GetComponent<Rigidbody>().velocity = movement * speed / 30;
		//Trying touchpad above

//		Debug.Log ("The velocity is " + GetComponent<Rigidbody> ().velocity);


		
		//Change transform.position based on the axes
		Vector3 pos = transform.position;
		pos.x += GetComponent<Rigidbody>().velocity.x * speed * Time.deltaTime;
		pos.y += GetComponent<Rigidbody>().velocity.y * speed * Time.deltaTime;
		transform.position = pos;

		//Rotate the ship to make it feel more dynamic
		GetComponent<Rigidbody>().rotation = Quaternion.Euler(GetComponent<Rigidbody>().velocity.y * +tilt, GetComponent<Rigidbody>().velocity.x * -tilt, 0.0f );

		bounds.center = transform.position;

		//Keep the ship constrained to the screen bounds
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero) {
			pos -= off;
			transform.position = pos;
		}
		
		//Rotate the ship to make it feel more dynamic
//		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);

		//Use the fireDelegate to fire Weapons
		//First, make sure the Axis("Jump") button is pressed
		//Then ensure that fireDelegate isn't null to avoid an error
//		Debug.Log ("CanFire is set to " + fireButton.CanFire ());
		if (fireButton.CanFire() && fireDelegate != null ) {
			fireDelegate ();
//			Debug.Log("fireDelegate has been called");
		}
	
//		if (remainingDamageFrames > 0) {
//			remainingDamageFrames--;
//			if (remainingDamageFrames == 0) {
//				UnShowDamage ();
//			}
//		}

		}

	void CheckInventory(){
//		Debug.Log ("Checking inventory");
		int shieldUpgrade = Soomla.Store.StoreInventory.GetItemBalance (Constants.SHIELD_ITEM_ID);
//		Debug.Log ("Shield upgrade: " + shieldUpgrade);
		if ((shieldUpgrade >= 1))	{
			shieldUpgradeOwned = true;
			Debug.Log("Player owns shield upgrade");
		}

		if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BLASTER_WEAPON_ITEM_ID)){
					Debug.Log("Blaster is equipped");
//			spreadOwned = false;
				}
		if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)){
			Debug.Log("Spread is equipped");
			spreadOwned = true;
		}
		//		//		Debug.Log ("At CheckInventory, spreadOwned is " + spreadOwned);
//		int spreads = Soomla.Store.StoreInventory.GetItemBalance (Constants.SPREAD_WEAPON_ITEM_ID);
////				Debug.Log ("Number of spreads: " + spreads);
//		if ((spreads >= 1))
//		{
//
////			//			Debug.Log("Player owns a spread");
//		}
//
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
			
			if(go.tag == "Enemy"){
				//If the shield was triggered by an enemy decrease the level of the shield by 1
				ReduceShield();
				//Destroy the enemy
//				Destroy(go);
//				Instantiate(enemyExplosion, transform.position, transform.rotation);
//				Enemy will do its own explosion
			}else if (go.tag == "Asteroid") {
				//If the shield was triggered by an asteroid decrease the level of the shield by 1
				ReduceShield();
				//Destroy the asteroid
				Destroy(go);
			}else if (go.tag == "ProjectileEnemy") {
				//If the shield was triggered by an enemy projectile decrease the level of the shield by 1
				ReduceShield();
				//Destroy the enemy projectile
				Destroy(go);
			}else if (go.tag == "PowerUp") {
				// If the sheild was triggered by a PowerUp
				AbsorbPowerUp(go);
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
	public float shieldLevel {
		get {
			return(_shieldLevel);
		}
		set {
			_shieldLevel = Mathf.Min (value, 4);
			//If the shield is going to be set to less than zero
			if (!isInvincible && value < 0) {
				DestroyHero();
			}
		}
	}

	public void ReduceShield(){
//		foreach (Material m in materials) {
//			m.color = Color.red;
//		}
//		remainingDamageFrames = showDamageForFrames;
//		Debug.Log ("The shield level is " + shieldLevel);
		if (shieldUpgradeOwned) {
			if ((shieldCounter == true) && (shieldLevel != 0)) {
//			Debug.Log("Shield absorped the hit");
				shieldCounter = false;
//			Debug.Log("The shield counter is now " + shieldCounter);
			} else {
//			Debug.Log("Decreasing shield level");
				shieldLevel--;
				shieldCounter = true;
			}
		} else {  //If they don't have the shield upgrade
			shieldLevel--;
		}
	}

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

		public void AbsorbPowerUp(GameObject go) {
			PowerUp pu = go.GetComponent<PowerUp>();
			switch (pu.type) {
			case WeaponType.shield: // If it's the shield
				shieldLevel++;
				break;

			case WeaponType.speed:
			speed += 5;
			break;

			default: // If it's any Weapon PowerUp
				// Check the current weapon type
				if (pu.type == weapons[0].type) {
					// then increase the number of weapons of this type
					Weapon w = GetEmptyWeaponSlot(); // Find an available weapon
					if (w != null) {
						// Set it to pu.type
						w.SetType(pu.type);
					}
				SaveWeaponCount(pu.type);
				} else {
					// If this is a different weapon
				int recall = RecallWeaponCount(pu.type);
				GetEmptyWeaponSlot();
					ClearWeapons();
					weapons[0].SetType(pu.type);
				// Fill up slots for all the old power ups you had
				for (int i = 0; i < recall; i++) {
					Weapon w = GetEmptyWeaponSlot(); // Find an available weapon
					if (w != null) {
						// Set it to pu.type
						w.SetType(pu.type);
					}
				}
				//SaveWeaponCount(pu.type);
				}
				break;
			}
			pu.AbsorbedBy(this.gameObject);
		}
		
		Weapon GetEmptyWeaponSlot() {
			for (int i = 0; i < weapons.Length; i++) {
				if (weapons[i].type == WeaponType.none) {
				//Debug.Log(i);
					return(weapons[i]);
				}
			}
			return(null);
		}
		
		void ClearWeapons() {
			foreach (Weapon w in weapons) {
			weaponCount++;

				w.SetType(WeaponType.none);
			}
		}


	void SaveWeaponCount(WeaponType wtype){

			Debug.Log("Looking into your history of " + wtype);
			for (int i = 0; i < weapons.Length; i++) {
				if (weapons[i].type == WeaponType.none) {
					Debug.Log("The previous weapon had " + i + " instance(s)");
					Debug.Log("weapons[i].type is " + weapons[i].type);
					switch(wtype.ToString()){
					case("blaster"):
						Debug.Log("The weapon was a blaster");
						blasterRecall = i;
						Debug.Log("Save blasterRecall as " + blasterRecall);
						return;
					case("spread"):
						spreadRecall = i;
						Debug.Log("Save spreadRecall as " + spreadRecall);
						return;
					}

				}
			}
	}

		int RecallWeaponCount(WeaponType wtype){
		//Debug.Log("Looking into your history of " + wtype);
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].type == WeaponType.none) {
				Debug.Log("The previous weapon had " + i + " instance(s)");
				Debug.Log("weapons[i].type is " + weapons[i].type);
			switch(wtype.ToString()){
				case("blaster"):
					//Debug.Log("The weapon was a blaster");
					//Debug.Log("Recall blasterRecall as " + blasterRecall);
					return(blasterRecall);
				case("spread"):
					//Debug.Log("Recall spreadRecall as " + spreadRecall);
					return(spreadRecall);
				}
				return(i);

			}
		}
		return(0);
	}




	void DestroyHero(){
		Destroy (this.gameObject);
		//Tell Main.S to restart the game after a delay
//		Main.S.DelayedRestart (gameRestartDelay);
		Main.S.PlayerLoss ();
		//Create an explosion
		Instantiate(explosion, transform.position, transform.rotation);
	}



}