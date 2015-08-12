using UnityEngine;
using System.Collections;

public class Zapper : MonoBehaviour {
	
	public GameObject ZapperBullet;
	private GameObject[]		enemies;
	//	private GameObject			closestEnemy;
	public Quaternion		r01, r02;
	
	
	void Update () {
		Instantiate(ZapperBullet, transform.position, transform.rotation);
		//		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		//		GameObject GetClosestEnemy(){
		//			
		//
		//			enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		//			GameObject closestEnemy = null;
		//			float distance = Mathf.Infinity;
		//			Vector3 position = transform.position;  // should this change to the hero's position?
		//			foreach (GameObject go in enemies) {
		//				Vector3 difference = go.transform.position - position;
		//				float curDistance = difference.sqrMagnitude;
		//				if (curDistance < distance) {
		//					closestEnemy = go;
		//					distance = curDistance;
		//				}
		//			}
		//			return closestEnemy;
		//		}
		
		
		GameObject targetEnemy = GetClosestEnemy ();
		Debug.Log ("The target is " + targetEnemy);
		if (targetEnemy != null) {
			r02 = Quaternion.LookRotation (targetEnemy.transform.position);
			
			//		u = (Time.time - timeStart) / timeDuration;
			float u = 1;
			r01 = Quaternion.Slerp (this.transform.rotation, r02, u);
			r01 *= Quaternion.Euler(0,0,90);
			//			transform.rotation = r01;
			transform.rotation = r02;
			Debug.Log(r02);
		}
	}
	
	
	
	GameObject GetClosestEnemy(){
		
		
		enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		GameObject closestEnemy = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;  // should this change to the hero's position?
		foreach (GameObject go in enemies) {
			Vector3 difference = go.transform.position - position;
			float curDistance = difference.sqrMagnitude;
			if (curDistance < distance) {
				closestEnemy = go;
				distance = curDistance;
			}
		}
		return closestEnemy;
	}
	//		for (int i = 0; i < enemies.Length; i++) {
	//			if (Vector3.Distance(
	//				//Camera.main.gameObject.transform.position, 
	//				enemies [i].transform.position,
	//				enemies [i].transform.position) < 
	//			    Vector3.Distance(
	//				//Camera.main.gameObject.transform.position, 
	//				enemies [i].transform.position,
	//				enemies [i].transform.position)){
	//				//            closestEnemy.transform.position)){
	//				closestEnemy = enemies[i];
	//			}
	//			// might compare old value of closest body!
	//		}
	//		//		Debug.Log("Closest obejct is " + closestEnemy);
	//		
	//	}
	
	
}
