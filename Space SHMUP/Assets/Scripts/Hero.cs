using UnityEngine;
using System.Collections;

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

	public Bounds				bounds;
	private int					weaponCount;

	//private int					recall;
	private int					blasterRecall;
	private int					spreadRecall;
	private int					ballRecall;
	
	//Declare a new delegate type WeaponFireDelegate
	public delegate void WeaponFireDelegate();
	// Create a WeaponFireDelegate field named fireDelegate.
	public WeaponFireDelegate fireDelegate;

	//Below is from Space Shooter
	public Vector3 target;
	public float tilt;
	public float velocityLag = .3f;
	public float dampingRadius = 2.5f;
	//Above is from Space Shooter

	//Below from adding touchpad
	public SimpleTouchPad touchPad;
	public SimpleTouchAreaButton areaButton;
	private Quaternion calibrationQuaternion;
	//Above from adding touchpad


	void Awake(){
		S = this; //Set the singleton
		bounds = Utils.CombineBoundsOfChildren (this.gameObject);
	}

	void Start() {
		// Reset the weapons to start _Hero with 1 blaster
		ClearWeapons ();
		weapons [0].SetType (WeaponType.blaster);
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





//		Debug.Log ("The desiredVelocity is " + desiredVelocity);
//		GetComponent<Rigidbody>().velocity += (desiredVelocity - GetComponent<Rigidbody>().velocity) * velocityLag;
		


		
		//Trying touchpad below
		Vector2 direction = touchPad.GetDirection ();
//		Debug.Log ("The direction in Hero is " + direction);
//		Debug.Log ("The x direction in Hero is " + direction.x);
		Vector3 movement = new Vector3 (direction.x, direction.y, 0.0f);
//		Debug.Log ("The movement in Hero is " + movement);
		GetComponent<Rigidbody>().velocity = movement * speed / 30;
		//Trying touchpad above

//		Debug.Log ("The velocity is " + GetComponent<Rigidbody> ().velocity);


		
		//Change transform.position based on the axes
		Vector3 pos = transform.position;
		pos.x += GetComponent<Rigidbody>().velocity.x * speed * Time.deltaTime;
		pos.y += GetComponent<Rigidbody>().velocity.y * speed * Time.deltaTime;
		transform.position = pos;





//		//Change trainsform.position based on the axes
//		Vector3 pos = transform.position;
//		pos.x += xAxis * speed * Time.deltaTime;
//		pos.y += yAxis * speed * Time.deltaTime;
//		transform.position = pos;

		bounds.center = transform.position;

		//Keep the ship constrained to the screen bounds
		Vector3 off = Utils.ScreenBoundsCheck (bounds, BoundsTest.onScreen);
		if (off != Vector3.zero) {
			pos -= off;
			transform.position = pos;
		}
		
		//Rotate the ship to make it feel more dynamic
		transform.rotation = Quaternion.Euler (yAxis * pitchMult, xAxis * rollMult, 0);

		//Use the fireDelegate to fire Weapons
		//First, make sure the Axis("Jump") button is pressed
		//Then ensure that fireDelegate isn't null to avoid an error
		Debug.Log ("CanFire is set to " + areaButton.CanFire ());
//		if (Input.GetAxis ("Jump") == 1 && fireDelegate != null ) {
		if (areaButton.CanFire() && fireDelegate != null ) {
			fireDelegate ();
		}
		/*
		//The below is from Space Shooter to incorporate mobile control
		bool triggered = false;
		if (Input.mousePresent && Input.GetMouseButton (0)) {
			triggered = true;
		} else if (Input.touchCount > 0) {
			triggered = true;
		}
		//The above is from Space Shooter to incorporate mobile control




		//The below is from Space Shooter to incorporate mobile control
		//		Debug.Log ("The touchCount is " + Input.touchCount);
		
		//Find whether the mouse button 0 was pressed or released this frame
		//		bool b0Down = Input.GetMouseButtonDown (0);
		
		Vector3? touchPos = null;
		//Return whether the given mouse button is held down.
		//button values are 0 for left button, 1 for right button, 2 for the middle button.
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
			target.y = GetComponent<Rigidbody>().position.y;
		}
		
		Vector3 offset = target - GetComponent<Rigidbody>().position;
		
		float magnitude = offset.magnitude;
		if(magnitude > dampingRadius)
		{
			magnitude = dampingRadius;
		}
		float dampening = magnitude / dampingRadius;
		
		Vector3 desiredVelocity = offset.normalized * speed * dampening;
		Debug.Log ("The desiredVelocity is " + desiredVelocity);
		GetComponent<Rigidbody>().velocity += (desiredVelocity - GetComponent<Rigidbody>().velocity) * velocityLag;
		
		Debug.Log ("The velocity is " + GetComponent<Rigidbody> ().velocity);
		
		//Change trainsform.position based on the axes
		Vector3 pos = transform.position;
		pos.x += GetComponent<Rigidbody>().velocity.x * speed * Time.deltaTime;
		pos.y += GetComponent<Rigidbody>().velocity.y * speed * Time.deltaTime;
		transform.position = pos;
*/		
		/*		GetComponent<Rigidbody>().position = new Vector3
		(
				Mathf.Clamp (GetComponent<Rigidbody>().position.x, bounds.xMin, boundary.xMax), 
		    0.0f, 
				Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
*/
//		GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
		//The above is from Space Shooter to incorporate mobile control


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
				shieldLevel--;
				//Destroy the enemy
				Destroy(go);
			}else if (go.tag == "Asteroid") {
				//If the shield was triggered by an asteroid decrease the level of the shield by 1
				shieldLevel--;
				//Destroy the asteroid
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
			if (value < 0) {
				Destroy (this.gameObject);
				//Tell Main.S to restart the game after a delay
				Main.S.DelayedRestart (gameRestartDelay);
			}
		}
	}
		public void AbsorbPowerUp(GameObject go) {
			PowerUp pu = go.GetComponent<PowerUp>();
			switch (pu.type) {
			case WeaponType.shield: // If it's the shield
				shieldLevel++;
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
		//return(null);
		return(0);
	}



	//Adding from Space Shooter to get touch screen!


	void FixedUpdate ()
	{
		//
		//Vector2 direction = touchPad.GetDirection ();
		
		//		Vector3 accelerationRaw = Input.acceleration;
		
		//		Vector3 acceleration = FixAcceleration (accelerationRaw);
		//		Debug.Log ("Your acceleration is " + acceleration);
//		Vector2 direction = touchPad.GetDirection ();
//		Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);
//		GetComponent<Rigidbody>().velocity = movement * speed;

	}






}