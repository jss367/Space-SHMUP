using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int pointerID;
	private bool canFire; 
	//	private bool canLaunchMissile; 
	private bool canLaunch; 
	private bool canEnergy;
	//	private bool canDropMine;
	
	public float minSwipeDistY = 100;
	public float minSwipeDistX = 100;
	public float tinySwipeDistY = 10;
	public float tinySwipeDistX = 10;
	private Vector2 startPos;
	private Vector2 endPos;
	
	//	public GameObject bazookaArt;
	
	void Awake () {
		touched = false;
	}
	
	
	public void OnPointerDown (PointerEventData data) {
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canFire = true;
			//			Debug.Log("You have permission to fire");
			startPos = data.position;
			//			Touch touch = Input.touches[0];
			//			startPos = touch.position;
			//			Debug.Log(startPos);
			//			canDropMine = true;
		}
	}
	
	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
			endPos = data.position;
			//			Debug.Log(endPos);
			
			if (endPos.y - startPos.y > minSwipeDistY)
			{
				canLaunch = true;
				StartCoroutine("LaunchTimer");
			}
			
			if (endPos.x - startPos.x < tinySwipeDistX &&  endPos.y - startPos.y < tinySwipeDistY)
			{
				canEnergy = true;
			}
			
			//			if (!canLaunch && !canEnergy){
			//				canDropMine = false;
			//			}
		}
	}
	
	IEnumerator LaunchTimer(){
		yield return new WaitForSeconds (1);
		canLaunch = false;
	}
	
	
	
	
	public bool CanFire () {
		return canFire;
	}
	
	//	public bool CanLaunchMissile () {
	//		return canLaunchMissile;
	//	}
	
	public bool CanLaunch () {
		return canLaunch;
	}
	
	//	public bool CanDropMine () {
	//		return canDropMine;
	//	}
	
}