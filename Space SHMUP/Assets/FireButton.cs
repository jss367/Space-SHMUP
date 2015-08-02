using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int pointerID;
	private bool canFire; 
	private bool canLaunch; 
	private bool canEnergy;

	public float minSwipeDistY = 100;
	public float minSwipeDistX;
	private Vector2 startPos;
	private Vector2 endPos;

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
		}
	}
	
	public void OnPointerUp (PointerEventData data) {
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
			endPos = data.position;
			Debug.Log(endPos);

			if (endPos.y - startPos.y > minSwipeDistY)
			{
				canLaunch = true;
			}

			if (endPos.y - startPos.y > minSwipeDistY)
			{
				canEnergy = true;
			}
		}
	}
	
	public bool CanFire () {
		return canFire;
	}

	public bool CanLaunch () {
		return canLaunch;
	}

}