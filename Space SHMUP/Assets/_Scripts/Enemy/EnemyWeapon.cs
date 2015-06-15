using UnityEngine;
using System.Collections;

public class EnemyWeapon : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
	
	void Start ()
	{
		InvokeRepeating ("Fire", delay, fireRate);
	}
	
	void Fire ()
	{
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//		Debug.Log ("Enemy ship has fired");
//		GetComponent<AudioSource>().Play();
	}
}
