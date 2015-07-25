using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
	public GameObject hero;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
	public bool isAiming = false;
	public Quaternion rotation = new Quaternion();//.Euler(-90, 180, 0);
	
	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
		hero = (GameObject)GameObject.FindWithTag ("Hero");
		rotation = Quaternion.Euler (90, 180, 0);
		this.transform.rotation = rotation;
	}

	void Update(){
		if (isAiming) {
			try {
			transform.LookAt (hero.transform.position, Vector3.forward);
			}
			catch (System.Exception e)
			{
				Debug.Log("Caught error: " + e);
				this.transform.rotation = rotation;
			}
		}
	}
	
	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	}
}
