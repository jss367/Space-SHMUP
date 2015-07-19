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
	
	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
		hero = (GameObject)GameObject.FindWithTag ("Hero");
	}

	void Update(){
		if (isAiming) {
			transform.LookAt (hero.transform.position, Vector3.forward);
		}
	}
	
	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	}
}
