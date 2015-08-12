using UnityEngine;
using UnityEngine.UI;
//using System.Strin
using System.Collections;
using Soomla;

public class Hero : MonoBehaviour {
	
	static public Hero S; //S for singleton
	
	//	public float gameRestartDelay = 2f;
	
	//These fields control the movement of the ship
	public float				speed = 30;
	public float				rollMult = -45;
	public float				pitchMult = 30;
	
	//Ship status information
	[SerializeField]
	private float 				_shieldLevel = 1;
	
	// Weapon fields
	public Weapon[]				weapons;
	public float missileSpeed = 10;
	
	public bool _____________;
	//
	
	public bool spreadEquipped = false;
	
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
	public bool					speedUpgradeOwned = false;
	public bool isInvincible;
	private bool launch1;
	
	public GameObject popText;
	
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
	
	//	public SimpleTouchPad touchPad;
	public FireButton fireButton;
	
	public bool laserEquipped;
	public bool bazookaEquipped;
	public bool missileEquipped;
	
	public CNAbstractController cNABstractController;
	
	public GameObject explosion;
	public GameObject enemyExplosion;
	public GameObject missile;
	public GameObject missileArt;
	public GameObject missileLaunchLocation;
	public GameObject mine;
	public GameObject mineDropLocation;
	public GameObject bazookaBullet;
	public GameObject bazookaBulletLocation1;
	public GameObject bazookaBulletLocation2;
	private float lastMineTime;
	public float mineDelay = 4;
	public float bazookaDelay = 5;
	
	public GameObject bazookaArt1;
	public GameObject bazookaArt2;
	private float lastBazooka;
	public GameObject Bazooka1;
	public GameObject Bazooka2;
	public GameObject MissileLauncher;
	public GameObject MineDropper;
	private GameObject Mine;
	//	public GameObject Laser;
	
	public bool weaponOff;
	
	void Awake(){
		S = this; //Set the singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}
	
	void Start() {
		//		shieldUpgradeOwned = false;
		//		spreadEquipped = true; // comment out for builds
		// Reset the weapons to start _Hero with 1 blaster
		ClearWeapons ();
		CheckInventory ();
		
		if (spreadEquipped == true) {
			//			Debug.Log ("Spread is owned");
			weapons [0].SetType (WeaponType.spread);
		} else if (laserEquipped) {
			weapons [0].SetType (WeaponType.laser);
			
			
		} else {
			//			Debug.Log ("Spread is not equipped");
			weapons [0].SetType (WeaponType.blaster);
		}
		
		if (speedUpgradeOwned == true) {
			speed = 40;
		}
		
		lastMineTime = -mineDelay;
		
		bazookaArt1.SetActive(false);
		bazookaArt2.SetActive(false);
		if (bazookaEquipped) {
			StartCoroutine ("ReturnBazookaArt");
		} else {
			//Hide to bazooka launchers
			Bazooka1.SetActive (false);
			Bazooka2.SetActive (false);
		}
		
		if (!missileEquipped) {
			missile.SetActive(false);
			missileArt.SetActive(false);
			missileLaunchLocation.SetActive(false);
			MissileLauncher.SetActive(false);
		}
	}
	
	
	// Update is called once per frame
	void Update () {
		//Pull in information from the Input class
		//		float xAxis = Input.GetAxis ("Horizontal");
		//		float yAxis = Input.GetAxis ("Vertical");
		
		Vector3? touchPos = null;
		
		//		if (Input.mousePresent && Input.GetMouseButton (0)) 
		//		{
		//			touchPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 0.0f);
		//		} 
		//		else if (Input.touchCount > 0)  //should i add an if to see if the phase is stationary?
		//		{
		//			touchPos = new Vector3 (Input.touches [0].position.x, Input.touches [0].position.y, 0.0f);
		//		}
		//		Debug.Log ("The touchPos is " + touchPos);
		
		//		if (touchPos != null)
		//		{
		//			target = Camera.main.ScreenToWorldPoint(touchPos.Value);
		//			target.z = GetComponent<Rigidbody>().position.z;
		//		}
		
		//		Vector3 offset = target - GetComponent<Rigidbody>().position;
		//
		////		Debug.Log ("The offset is " + offset);
		//
		//
		//		float magnitude = offset.magnitude;
		//		if(magnitude > dampingRadius)
		//		{
		//			magnitude = dampingRadius;
		//		}
		//		float dampening = magnitude / dampingRadius;
		//		
		//		Vector3 desiredVelocity = offset.normalized * speed * dampening; // commented to try touchpad
		
		//		Debug.Log ("The velocity is " + GetComponent<Rigidbody> ().velocity);
		
		
		
		//Change transform.position based on the axes
		Vector3 pos = transform.position;
		//		pos.x += GetComponent<Rigidbody>().velocity.x * speed * Time.deltaTime;
		//		pos.y += GetComponent<Rigidbody>().velocity.y * speed * Time.deltaTime;
		//		transform.position = pos;
		
		//Rotate the ship to make it feel more dynamic
		//		Debug.Log (GetComponent<Rigidbody> ().rotation);
		//		Debug.Log (GetComponent<Rigidbody> ().velocity.x);
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
		//		if (fireButton.CanFire() && fireDelegate != null ) {
		//			fireDelegate ();
		////			Debug.Log("fireDelegate has been called");
		//		}
		
		if (cNABstractController.CanFire() && fireDelegate != null && !weaponOff) {
			fireDelegate ();
			//			Debug.Log("fireDelegate has been called");
		}
		
		if (missileEquipped && fireButton.CanLaunch() && !launch1) {
			Instantiate(missile, missileLaunchLocation.transform.position, missileLaunchLocation.transform.rotation);
			launch1 = true;
			//			Missile.SendMessage("Fire");
			missileArt.SetActive(false);
			
			//			Debug.Log("fireDelegate has been called");
		}
		
		if (bazookaEquipped && fireButton.CanLaunch() && Time.time > lastBazooka + bazookaDelay) {
			
			Instantiate(bazookaBullet, bazookaBulletLocation1.transform.position, bazookaBulletLocation1.transform.rotation);
			Instantiate(bazookaBullet, bazookaBulletLocation2.transform.position, bazookaBulletLocation2.transform.rotation);
			lastBazooka = Time.time;
			
			bazookaArt1.SetActive(false);
			bazookaArt2.SetActive(false);
			StartCoroutine("ReturnBazookaArt");
			//			Debug.Log("fireDelegate has been called");
		}
		
		//		if (fireButton.CanDropMine()) {
		//			if (Time.time - lastMineTime < mineDelay) {
		//				return;
		//			}
		//			Instantiate(mine, mineDropLocation.transform.position, mineDropLocation.transform.rotation);
		//			lastMineTime = Time.time;
		//			//			Debug.Log("fireDelegate has been called");
		//		}
		
		//		if (remainingDamageFrames > 0) {
		//			remainingDamageFrames--;
		//			if (remainingDamageFrames == 0) {
		//				UnShowDamage ();
		//			}
		//		}
	}
	
	IEnumerator ReturnBazookaArt(){
		yield return new WaitForSeconds (bazookaDelay);
		bazookaArt1.SetActive(true);
		bazookaArt2.SetActive(true);
	}
	
	
	void CheckInventory(){
		//		Debug.Log ("Checking inventory");
		try
		{
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BLASTER_WEAPON_ITEM_ID)){
				//					Debug.Log("Blaster is equipped");
			}
			
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)){
				//			Debug.Log("Spread is equipped");
				spreadEquipped = true;
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}
		try{
			int balance = Soomla.Store.StoreInventory.GetItemBalance(Constants.BASESHIELD_ITEM_ID);
			//			Debug.Log("Shield upgrade balance is " + balance);
			if(balance > 0)  
			{
				//				Debug.Log("Player has shield upgrade");
				shieldUpgradeOwned = true;
			}
		}
		
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}
		
		try{
			int balance = Soomla.Store.StoreInventory.GetItemBalance(Constants.SPEED_ITEM_ID);
			//			Debug.Log("Speed upgrade balance is " + balance);
			if(balance > 0)   // This should be a switch with all the different upgrade levels
			{
				//				Debug.Log("Player has speed upgrade");
				speedUpgradeOwned = true;
			}
		}
		
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}
		
		try
		{
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.MISSILE_LAUNCHER_ITEM_ID)){
				missileEquipped = true;
			}
			
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BAZOOKA_LAUNCHER_ITEM_ID)){
				bazookaEquipped = true;
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}
		
		//		laserEquipped = true;
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
				//			print ("Triggered: " + go.name);
				//Make sure it's not the same triggering go as last time
			}
		}else {
			//Otherwise announce the original gameObject
			//			print ("Triggered: " + other.gameObject.name);
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
		Main.S.AddScore (10);
		Instantiate(popText, transform.position, Quaternion.identity);
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
		
		//			Debug.Log("Looking into your history of " + wtype);
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].type == WeaponType.none) {
				//					Debug.Log("The previous weapon had " + i + " instance(s)");
				//					Debug.Log("weapons[i].type is " + weapons[i].type);
				switch(wtype.ToString()){
				case("blaster"):
					//						Debug.Log("The weapon was a blaster");
					blasterRecall = i;
					//						Debug.Log("Save blasterRecall as " + blasterRecall);
					return;
				case("spread"):
					spreadRecall = i;
					//						Debug.Log("Save spreadRecall as " + spreadRecall);
					return;
				}
				
			}
		}
	}
	
	int RecallWeaponCount(WeaponType wtype){
		//Debug.Log("Looking into your history of " + wtype);
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons[i].type == WeaponType.none) {
				//				Debug.Log("The previous weapon had " + i + " instance(s)");
				//				Debug.Log("weapons[i].type is " + weapons[i].type);
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
		//		Debug.Log ("Hero has been destroyed");
		//Tell Main.S to restart the game after a delay
		//		Main.S.DelayedRestart (gameRestartDelay);
		Main.S.PlayerLoss ();
		//Create an explosion
		Instantiate(explosion, transform.position, transform.rotation);
	}
	
	
	
}