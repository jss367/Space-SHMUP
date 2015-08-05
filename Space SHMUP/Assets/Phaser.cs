using UnityEngine;
using System.Collections;

public class Phaser : MonoBehaviour {

	public GameObject PhaserBullet;
	private GameObject[]		enemies;
	private GameObject			closestObject;
	public Quaternion		r01, r02;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Instantiate(PhaserBullet, transform.position, transform.rotation);
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GetClosestEnemy ();
		if (closestObject != null) {
			r02 = Quaternion.LookRotation (closestObject.transform.position);
		
//		u = (Time.time - timeStart) / timeDuration;
		float u = 1;
		r01 = Quaternion.Slerp (this.transform.rotation, r02, u);
		transform.rotation = r01;
		}
	}



	void GetClosestEnemy(){
		
		for (int i = 0; i < enemies.Length; i++) {
			if (Vector3.Distance(
				Camera.main.gameObject.transform.position, 
				enemies [i].transform.position) < 
			    Vector3.Distance(Camera.main.gameObject.transform.position, 
			                 closestObject.transform.position)){
				closestObject = enemies[i];
			}
			// might compare old value of closest body!
		}
		//		Debug.Log("Closest obejct is " + closestObject);
		
	}


}
