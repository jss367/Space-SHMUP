using UnityEngine;
using System.Collections;

public class Projectile_Tutorial : MonoBehaviour {
	
	[SerializeField] //The book ends this with a ")", assuming it's a typo
	private WeaponType_Tutorial _type;
	
	//This public property masks the field _type & takes action when it is set
	public WeaponType_Tutorial type {
		get {
			return (_type);
		}
		set {
			SetType (value);
		}
	}
	
	void Awake(){
		//Test to see whether this has passed off screen every 2 seconds
		InvokeRepeating ("CheckOffscreen", 2f, 2f);
	}
	
	
	public void	SetType ( WeaponType_Tutorial eType) {
		//set the type
		_type = eType;
		WeaponDefinition_Tutorial def = Main_Tutorial.GetWeaponDefinition( _type);
		GetComponent<Renderer>().material.color = def.projectileColor;
	}
	void CheckOffscreen() {
		if (Utils.ScreenBoundsCheck (GetComponent<Collider>().bounds, BoundsTest.offScreen) != Vector3.zero) {
			Destroy (this.gameObject);
		}
	}
}
