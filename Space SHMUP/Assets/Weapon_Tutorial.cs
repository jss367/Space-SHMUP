﻿using UnityEngine;
using System.Collections;

//This is an enum of the various possible weapon types
//It also includes a "shield" type to allow a shield power-up
//Items marked [NI] below are Not Implemented
public enum WeaponType_Tutorial {
	none, //The default / no weapon
	blaster, //A simple blaster
	spread, //Two shots simultaneously
	phaser, //Shots that move in waves [NI]
	missile, //Homing missiles [NI]
	laser, //Damage over time [NI]
	shield, //Raise sheidLevel
	ball,
	plasma,
	speed
	
}

//The WeaponDefinition class allows you to set the properties
//of a specific weapon in the Inspector. Main has an array
// of WeaponDefinitions that makes this possible.
//[System.Serializable] tells Unity to try to view WeaponDefinition
//in the Inspector pane. It doesn't work for everything, but it
//will work for simple classes like this!
[System.Serializable]
public class WeaponDefinition_Tutorial {
	
	public WeaponType_Tutorial type = WeaponType_Tutorial.none;
	public string letter; //The letter to show on the power-up
	public Color color = Color.white; //Color of the Collar & power-up
	public GameObject projectilePrefab;  //Prefab for projectiles
	public Color projectileColor = Color.white;
	public float damageOnHit = 0; //Amount of damage caused
	public float continuousDamage = 0; //Damage per second (Laser)
	public float delayBetweenShots = 0;
	public float velocity = 20; //Speed of projectiles
	
}

//Note: Weapon prefabs, colors, and so on are set in the class Main.



public class Weapon_Tutorial : MonoBehaviour {
	
	static public Transform PROJECTILE_ANCHOR;
	
	public bool _______________;
	[SerializeField]
	private WeaponType_Tutorial _type = WeaponType_Tutorial.blaster;
	public WeaponDefinition_Tutorial def;
	public GameObject collar;
	public float lastShot; //Time last shot was fired
	
	void Awake() {
		collar = transform.Find ("Collar").gameObject;
		//		projectilePrefab = GameObject.Find
	}
	
	void Start() {
		//Call SetType() properly for the default _type
		SetType (_type);
		
		if (PROJECTILE_ANCHOR == null) {
			GameObject go = new GameObject ("_Projectile_Anchor");
			PROJECTILE_ANCHOR = go.transform;
		}
		//Find the fireDelegate of the parent
		GameObject parentGO = transform.parent.gameObject;
		if (parentGO.tag == "Hero") {
			Hero_Tutorial.S.fireDelegate += Fire;
		}
	}
	
	public WeaponType_Tutorial type {
		get { return(_type); }
		set { SetType (value); }
	}
	
	public void SetType (WeaponType_Tutorial wt) {
		_type = wt;
		if (type == WeaponType_Tutorial.none) {
			this.gameObject.SetActive (false);
			return;
		} else {
			this.gameObject.SetActive (true);
		}
		def = Main_Tutorial.GetWeaponDefinition (_type);
		collar.GetComponent<Renderer>().material.color = def.color;
		lastShot = 0; //You can always fire immediately after _type is set
	}
	
	public void Fire() {
		// If this.gameObject is inactive, return
		if (!gameObject.activeInHierarchy)
			return;
		//If it hasn't been enough time between shots, return
		if (Time.time - lastShot < def.delayBetweenShots) {
			return;
		}
		Projectile_Tutorial p;
//				Debug.Log("Weapon is of type " + type);
		switch (type) {
		case WeaponType_Tutorial.blaster:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
//						Debug.Log("Weapon was created with velocity " + def.velocity);
			break;
			
		case WeaponType_Tutorial.spread:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (-.2f, 0.9f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (.2f, 0.9f, 0) * def.velocity;
			break;
			
		case WeaponType_Tutorial.ball:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (-.2f, 0.9f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (.2f, 0.9f, 0) * def.velocity;
			break;
			
		case WeaponType_Tutorial.laser:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (-.2f, 0.9f, 0) * def.velocity;
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = new Vector3 (.2f, 0.9f, 0) * def.velocity;
			break;
			
		case WeaponType_Tutorial.plasma:
			p = MakeProjectile ();
			p.GetComponent<Rigidbody>().velocity = Vector3.up * def.velocity;
			break;
		}
	}
	public Projectile_Tutorial MakeProjectile() {
		//		Debug.Log ("Making projectile for " + transform.parent.gameObject.tag);
		GameObject go = Instantiate (def.projectilePrefab) as GameObject;
		if (transform.parent.gameObject.tag == "Hero") {
			go.tag = "ProjectileHero";
			//			Debug.Log("go.tag is " + go.tag);
			go.layer = LayerMask.NameToLayer ("ProjectileHero");
			//			Debug.Log("go.layer is " + go.layer);
		} else {
			go.tag = "ProjectileEnemy";
			go.layer = LayerMask.NameToLayer ("ProjectileEnemy");
		}
		go.transform.position = collar.transform.position;
		//		Debug.Log ("go.transform.position is " + go.transform.position);
		go.transform.parent = PROJECTILE_ANCHOR;
		Projectile_Tutorial p = go.GetComponent<Projectile_Tutorial> ();
		p.type = type;
		lastShot = Time.time;
		//		Debug.Log ("Last shot was at " + lastShot);
		return (p);
	}
}