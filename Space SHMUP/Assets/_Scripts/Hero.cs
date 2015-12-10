using UnityEngine;
using UnityEngine.UI;

//using System.Strin
using System.Collections;
using Soomla;

public class Hero : MonoBehaviour
{

	static public Hero S; //S for singleton
	
	//These fields control the movement of the ship
//	public float				speed = 30;
	public float				rollMult = -45;
	public float				pitchMult = 30;
	
	//Ship status information
	[SerializeField]
	private float
		_shieldLevel = 1;

	// Weapon fields
	public Weapon[]				weapons;
	public float missileSpeed = 10;
	public bool _____________;
//


	public Bounds				bounds;
	private int					weaponCount;
	private int					ballRecall;
	private bool				shieldCounter = true;
	public bool					shieldUpgradeOwned = false;
	public bool isInvincible;
	public bool launch1;
	public GameObject popText;
	
	//Declare a new delegate type WeaponFireDelegate
	public delegate void WeaponFireDelegate ();
	// Create a WeaponFireDelegate field named fireDelegate.
	public WeaponFireDelegate fireDelegate;

	//Below is from Space Shooter
	public Vector3 target;
	public float tilt = 5;
//	public float velocityLag = .3f;
//	public float dampingRadius = 2.5f;
	//Above is from Space Shooter

	private Vector3 oldPos = new Vector3(0f,-18.0f,0f);

	public FireButton fireButton;
	private bool spreadEquipped = false;
	private bool laserEquipped;
	private bool bazookaEquipped;
	private bool mineEquipped;
	private bool missileEquipped;
	private bool doubleBlaster;
	public CNAbstractController cNABstractController;
	public GameObject explosion;
	public GameObject enemyExplosion;
	public GameObject missile;
	public GameObject missileArt;
	public GameObject missileLaunchLocation;
//	public GameObject MineDropper;
	private GameObject Mine;
	public GameObject mine;
	public GameObject mineDropLeft;
	public GameObject mineDropRight;
	public GameObject mineArtLeft;
	public GameObject mineArtRight;
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
	public float maxTilt = .3f;
	public float rotationSpeed = 2.0f;
	public float rotateToNormal = .2f;

//	public GameObject Laser;
	public int autoShootOn = 1;
	public bool weaponOff;

	void Awake ()
	{
		S = this; //Set the singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	void Start ()
	{
		// Reset the weapons to start _Hero with 1 blaster
		ClearWeapons ();
		CheckInventory ();
		autoShootOn = PlayerPrefs.GetInt ("AutoShoot");
		laserEquipped = true;
//		Debug.Log ("autoShootOn is " + autoShootOn);
		if (spreadEquipped == true) {
//			Debug.Log ("Spread is owned");
			weapons [0].SetType (WeaponType.spread);
		} else if (laserEquipped) {
			weapons [0].SetType (WeaponType.laser);
		} else if (doubleBlaster) {
			weapons [0].SetType (WeaponType.doubleBlaster);
		} else {
			weapons [0].SetType (WeaponType.blaster);
		}

		lastMineTime = -mineDelay;

		bazookaArt1.SetActive (false);
		bazookaArt2.SetActive (false);
		if (bazookaEquipped) {
			StartCoroutine ("ReturnBazookaArt");
		} else {
			//Hide to bazooka launchers
			Bazooka1.SetActive (false);
			Bazooka2.SetActive (false);
		}

		if (!mineEquipped) {
			mineArtLeft.SetActive(false);
			mineArtRight.SetActive(false);
		}

		if (!missileEquipped) {
//			missile.SetActive(false);
			missileArt.SetActive (false);
			missileLaunchLocation.SetActive (false);
			MissileLauncher.SetActive (false);
		}

	}
	

	// Update is called once per frame
	void Update ()
	{
		Vector3? touchPos = null;
		//Change transform.position based on the axes
		Vector3 pos = transform.position;
		Vector3 movement = pos - oldPos;
//		Debug.Log (oldPos);

//		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (movement.y * +tilt, 100 * movement.x * -tilt, 0.0f);

//				Debug.Log (GetComponent<Rigidbody> ().rotation);
//				Debug.Log (GetComponent<Rigidbody> ().velocity.x);
		//GetComponent<Rigidbody> ().rotation = Quaternion.Euler (GetComponent<Rigidbody> ().velocity.y * +tilt, GetComponent<Rigidbody> ().velocity.x * -tilt, 0.0f);

		bounds.center = transform.position;

		//Keep the ship constrained to the screen bounds
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero) {
			pos -= off;
			transform.position = pos;
		}

		if (autoShootOn == 0) {
			if (fireButton.CanFire () && fireDelegate != null && !weaponOff) {
				fireDelegate ();
				//			Debug.Log("fireDelegate has been called");
			}

		} else {
			if ((fireButton.CanFire () || cNABstractController.CanFire ()) && fireDelegate != null && !weaponOff) {
				fireDelegate ();
				//			Debug.Log("fireDelegate has been called");
			}

		}

		if (missileEquipped && fireButton.CanLaunch () && !launch1) {
			Instantiate (missile, missileLaunchLocation.transform.position, missileLaunchLocation.transform.rotation);
			TakeMissile ();


			//			Debug.Log("fireDelegate has been called");
		}

		if (bazookaEquipped && fireButton.CanLaunch () && Time.time > lastBazooka + bazookaDelay) {

			Instantiate (bazookaBullet, bazookaBulletLocation1.transform.position, bazookaBulletLocation1.transform.rotation);
			Instantiate (bazookaBullet, bazookaBulletLocation2.transform.position, bazookaBulletLocation2.transform.rotation);
			lastBazooka = Time.time;

			bazookaArt1.SetActive (false);
			bazookaArt2.SetActive (false);
			StartCoroutine ("ReturnBazookaArt");
			//			Debug.Log("fireDelegate has been called");
		}

		if (mineEquipped && fireButton.CanMineLeft ()) {
			if (Time.time - lastMineTime < mineDelay) {
				return;
			}
			Instantiate (mine, mineDropLeft.transform.position, mineDropLeft.transform.rotation);
			lastMineTime = Time.time;
//			Debug.Log ("Mining left");
		}
	
		if (mineEquipped && fireButton.CanMineRight ()) {
			if (Time.time - lastMineTime < mineDelay) {
				return;
			}
			Instantiate (mine, mineDropRight.transform.position, mineDropRight.transform.rotation);
			lastMineTime = Time.time;
//			Debug.Log ("Mining right");
		}
		oldPos = pos;
		float rot = transform.rotation.y;
//		Debug.Log (transform.rotation);
//		Debug.Log (movement);
		//Do all the roll attitude effects
		if (rot < maxTilt && rot > -maxTilt) {
			transform.Rotate (0.0f, -movement.x * rotationSpeed, 0.0f);
		} else if (rot < maxTilt && movement.x < 0) {  // If rot is less than neg .2 in practice, but using pos .2 just to be safe

			transform.Rotate (0.0f, -movement.x * rotationSpeed, 0.0f);

		} else if (rot > -maxTilt && movement.x > 0){
			transform.Rotate (0.0f, -movement.x * rotationSpeed, 0.0f);

		}
//			Vector3 dest = new Vector3 (0.0f, -movement.x, 0.0f);
//		RotateShip (dest);
//		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);

		//slowly move it back if you are steering the ship
		if (movement.x == 0 && rot < 0) {
			transform.Rotate (0.0f, rotateToNormal * rotationSpeed, 0.0f);
		}
		else if (movement.x == 0 && rot > 0) {
			transform.Rotate (0.0f, -rotateToNormal * rotationSpeed, 0.0f);
		}

		//Do all the pitch attitude effects
		float rotz = transform.rotation.x;
//		Debug.Log (rotz);
		if (rotz < maxTilt && rotz > -maxTilt) {
			transform.Rotate (movement.y * rotationSpeed, 0.0f, 0.0f);
		} else if (rotz < maxTilt && movement.y > 0) {
			transform.Rotate (movement.y * rotationSpeed, 0.0f, 0.0f);
		
		} else if (rotz > -maxTilt && movement.y < 0) {
			transform.Rotate (movement.y * rotationSpeed, 0.0f, 0.0f);
		}

		//slowly move it back if you are steering the ship
		if (movement.y == 0 && rotz < 0) {
			transform.Rotate (rotateToNormal * rotationSpeed, 0.0f, 0.0f);
		}
		else if (movement.y == 0 && rotz > 0) {
			transform.Rotate (-rotateToNormal * rotationSpeed, 0.0f, 0.0f);
		}


		//keep z from rotating
		Quaternion rotx = transform.rotation;
		rotx.z = 0.0f;
		transform.rotation = rotx;

	}



	IEnumerator ReturnBazookaArt ()
	{
		yield return new WaitForSeconds (bazookaDelay);
		bazookaArt1.SetActive (true);
		bazookaArt2.SetActive (true);
	}
	
	void CheckInventory ()
	{
//		Debug.Log ("Checking inventory");
		try {

			if (Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)) {
//			Debug.Log("Spread is equipped");
				spreadEquipped = true;
			}

			else if (Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.LASER_WEAPON_ITEM_ID)) {
				//			Debug.Log("Spread is equipped");
				laserEquipped = true;
			}


		} catch (System.Exception e) {
			Debug.Log ("Caught error: " + e);
		}
		try {
			int balance = Soomla.Store.StoreInventory.GetItemBalance (Constants.BASESHIELD_ITEM_ID);
//			Debug.Log("Shield upgrade balance is " + balance);
			if (balance > 0) {
//				Debug.Log("Player has shield upgrade");
				shieldUpgradeOwned = true;
			}
		} catch (System.Exception e) {
			Debug.Log ("Caught error: " + e);
		}

		try {
			if (Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.MISSILE_LAUNCHER_ITEM_ID)) {
				missileEquipped = true;
			}
			
			if (Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BAZOOKA_LAUNCHER_ITEM_ID)) {
				bazookaEquipped = true;
			}

			if (Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.MINE_LAUNCHER_ITEM_ID)) {
				mineEquipped = true;
			}
		} catch (System.Exception e) {
			Debug.Log ("Caught error: " + e);
		}
		try {
			int balance = Soomla.Store.StoreInventory.GetItemBalance (Constants.DOUBLE_BLASTER_WEAPON_ITEM_ID);
			if (balance > 0) {
				doubleBlaster = true;
			}
		} catch (System.Exception e) {
			Debug.Log ("Caught error: " + e);
		}
	}

	//This variable holds a reference to the last triggering GameObject
	public GameObject lastTriggerGo = null;

	void OnTriggerEnter (Collider other)
	{
		//Find the tag of other.gameObject or its parent GameObjects
		GameObject go = Utils.FindTaggedParent (other.gameObject);
		//If there is a parent with a tag
		if (go != null) {
			//Make sure it's not the same triggering go as last time
			if (go == lastTriggerGo) {
				return;
			}
			lastTriggerGo = go;
			
			if (go.tag == "Enemy") {
				//If the shield was triggered by an enemy decrease the level of the shield by 1
				ReduceShield ();
				//Destroy the enemy
//				Destroy(go);
//				Instantiate(enemyExplosion, transform.position, transform.rotation);
//				Enemy will do its own explosion
			} else if (go.tag == "Asteroid") {
				//If the shield was triggered by an asteroid decrease the level of the shield by 1
				ReduceShield ();
				//Destroy the asteroid
				Destroy (go);
			} else if (go.tag == "ProjectileEnemy") {
				//If the shield was triggered by an enemy projectile decrease the level of the shield by 1
				ReduceShield ();
				//Destroy the enemy projectile
				Destroy (go);
			} else if (go.tag == "PowerUp") {
				// If the sheild was triggered by a PowerUp
				AbsorbPowerUp (go);
			} else {
				//Announce it
//				print ("Triggered: " + go.name);
				//Make sure it's not the same triggering go as last time
			}
		} else {
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
				DestroyHero ();
			}
		}
	}

	public void ReduceShield ()
	{
//		foreach (Material m in materials) {
//			m.color = Color.red;
//		}
//		remainingDamageFrames = showDamageForFrames;
//		Debug.Log ("The shield level is " + shieldLevel);
		if (shieldUpgradeOwned) {
			if ((shieldCounter == true) && (shieldLevel != 0)) {
//			Debug.Log("Shield absorbed the hit");
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

	public void AbsorbPowerUp (GameObject go)
	{
		Main.S.AddScore (10);
		Instantiate (popText, transform.position, Quaternion.identity);
		PowerUp pu = go.GetComponent<PowerUp> ();
//		Debug.Log("pu.type is " + pu.type);
		switch (pu.type) {

		case WeaponType.shield: // If it's the shield
			shieldLevel++;
			break;

		case WeaponType.missile:
//			Debug.Log("Hero absorbed a missile");
			GiveMissile ();
			break;

		default: // If it's any Weapon PowerUp
				// Check the current weapon type
			if (pu.type == weapons [0].type) {
				// then increase the number of weapons of this type
				Weapon w = GetEmptyWeaponSlot (); // Find an available weapon
				if (w != null) {
					// Set it to pu.type
					w.SetType (pu.type);
				}
			} 
			break;
		}
		pu.AbsorbedBy (this.gameObject);
	}

	public void GiveMissile ()
	{
		launch1 = false;
		missileArt.SetActive (true);
	}

	public void TakeMissile ()
	{
		launch1 = true;
		missileArt.SetActive (false);
	}
	
	Weapon GetEmptyWeaponSlot ()
	{
		for (int i = 0; i < weapons.Length; i++) {
			if (weapons [i].type == WeaponType.none) {
				//Debug.Log(i);
				return(weapons [i]);
			}
		}
		return(null);
	}
		
	void ClearWeapons ()
	{
		foreach (Weapon w in weapons) {
			weaponCount++;

			w.SetType (WeaponType.none);
		}
	}



	void DestroyHero ()
	{
		Destroy (this.gameObject);
//		Debug.Log ("Hero has been destroyed");
		Main.S.PlayerLoss ();
		//Create an explosion
		Instantiate (explosion, transform.position, transform.rotation);
	}



}