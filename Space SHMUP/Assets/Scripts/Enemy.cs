﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	
	public float		speed = 20f; //The speed in m/s
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
	private GameController gameController;
	private Main main;
	
	void Awake() {
		materials = Utils.GetAllMaterials (gameObject);
		originalColors = new Color[materials.Length];
		for (int i = 0; i < materials.Length; i++) {
			originalColors [i] = materials [i].color;
		}
		InvokeRepeating ("CheckOffscreen", 0f, 2f);
	}

	void Start() {
		//gameController = GameObject.Find("GameController").GetComponent<GameController>();
		GameObject gameControllerObject = GameObject.Find("GameController");
	//	Debug.Log("gameControllerObject is: " + gameControllerObject);
		if (gameControllerObject != null) {
			gameController = gameControllerObject.GetComponent<GameController> ();
		}
	//	Debug.Log("gameController is: " + gameController);

		GameObject mainObject = GameObject.FindWithTag("MainCamera");
		if (mainObject != null) {
			main = mainObject.GetComponent<Main> ();
		}
		 

	}

	//Update is called once per frame
	void Update(){
		if (gameController != null) {
		//	Debug.Log("gameController does exist");
			tMultiplier = gameController.timeMultiplier;
			//Debug.Log (tMultiplier);
		}
		if (gameController == null) {
			//Debug.Log ("Cannot find 'gameController'");
		}

		if (main != null) {
			//	Debug.Log("gameController does exist");
			tMultiplier = main.timeMultiplier;
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
		tempPos.y -= (tMultiplier + speed) * Time.deltaTime;
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

	void OnCollisionEnter (Collision coll) {
		GameObject other = coll.gameObject;
		switch (other.tag) {
		case "ProjectileHero":
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
			health -= Main.W_DEFS [p.type].damageOnHit;
			if (health <= 0) {
				// Tell the Main singleton that this ship has been destroyed
				Main.S.ShipDestroyed(this);
				// Destroy this Enemy
				Destroy (this.gameObject);
			}
			Destroy (other);
			break;
		case "Asteroid":
			Debug.Log("Hit an asteroid");
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
			health -= 1; // Asteroids do 10 worth of damage
			if (health <= 0) {
				// Tell the Main singleton that this ship has been destroyed
			//	Main.S.ShipDestroyed(this);
				// Destroy this Enemy
				Destroy (this.gameObject);
			}
			Destroy (other);
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