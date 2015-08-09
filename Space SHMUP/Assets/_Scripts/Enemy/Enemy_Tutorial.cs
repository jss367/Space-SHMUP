using UnityEngine;
using System.Collections;

public class Enemy_Tutorial : MonoBehaviour {
	
	public float		speed = 0.0f; //The speed in m/s

	public float		health = 10;


	public int			showDamageForFrames = 2; // # of frames to show damage
	public float		powerUpDropChance = 1f; // Chance to drop a power-up
	public bool _________________;

	public GameObject	impact;

	public Color[]		originalColors;
	public Material[]	materials; //All the Materials of this & its children
	public int			remainingDamageFrames = 0; // Damage frames left

	public Bounds bounds; //The Bounds of this and its children
	public Vector3 boundsCenterOffset; //Distance of bounds.center from position

//	private Main main;
	public GameObject			prefabPowerUp;
	public GameObject enemyExplosion;
	private bool			spreadEquipped = false;

	
	void Awake() {
		materials = Utils.GetAllMaterials (gameObject);
		originalColors = new Color[materials.Length];
		for (int i = 0; i < materials.Length; i++) {
			originalColors [i] = materials [i].color;
		}
	}

	void Start() {
		CheckInventory ();
	}

	void CheckInventory(){
		
		//		Debug.Log ("Checking inventory");
		try
		{
			
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BLASTER_WEAPON_ITEM_ID)){
				Debug.Log("Blaster is equipped");
				
			}
			if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)){
				Debug.Log("Spread is equipped");
				spreadEquipped = true;
			}
		}
		catch (System.Exception e)
		{
			Debug.Log("Caught error: " + e);
		}
		
	}

	//Update is called once per frame
	void Update(){

//		Move();
		if (remainingDamageFrames > 0) {
			remainingDamageFrames--;
			if (remainingDamageFrames == 0) {
				UnShowDamage ();
			}
		}
	}

	

	//This is a Property: A method that acts like a field
//	public Vector3 pos{
//		get{
//			return (this.transform.position);
//		}
//		set {
//			this.transform.position = value;
//		}
//	}


	void OnCollisionEnter (Collision coll) {
		GameObject other = coll.gameObject;
//		Debug.Log ("Enemy collidered with " + other.tag);
		switch (other.tag) {
		case "ProjectileHero":
//			Debug.Log ("Projectile has hit enemy");
			Projectile p = other.GetComponent<Projectile> ();
			// Hurt this Enemy
			ShowDamage();
			Instantiate(impact, transform.position, transform.rotation);
			// Get the damage amount from the Projectile.type & Main.W_DEFS
			health -= Main.W_DEFS [p.type].damageOnHit;
//			Debug.Log("Remaining health is " + health);
			if (health <= 0) {
				// Destroy this Enemy
				Destroy (this.gameObject);
//				int ndx = Random.Range(0, powerUpFrequency.Length);
//				WeaponType puType = powerUpFrequency[ndx];
				
				// Spawn a PowerUp
				GameObject go = Instantiate(prefabPowerUp) as GameObject;
				PowerUp pu = go.GetComponent<PowerUp>();
				// Set it to the proper WeaponTYpe
				if (spreadEquipped){
					pu.SetType(WeaponType.spread);
				}
				else{
				pu.SetType(WeaponType.blaster);
				}
				// Set it to the position of the destroyed ship
				pu.transform.position = this.transform.position;

//				Debug.Log("lastTimeDestroyed is " + lastTimeDestroyed);
				Instantiate(enemyExplosion, transform.position, transform.rotation);
			
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
//		Debug.Log ("Enemy was triggered by " + other.tag);
		switch (other.tag) {
		case "Hero":

			// Destroy this Enemy
			Destroy(this.gameObject);
			// Spawn a PowerUp
			GameObject go = Instantiate(prefabPowerUp) as GameObject;
			PowerUp pu = go.GetComponent<PowerUp>();
			// Set it to the proper WeaponTYpe
			if (spreadEquipped){
				pu.SetType(WeaponType.spread);
			}
			else{
				pu.SetType(WeaponType.blaster);
			}
			// Set it to the position of the destroyed ship
			pu.transform.position = this.transform.position;
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