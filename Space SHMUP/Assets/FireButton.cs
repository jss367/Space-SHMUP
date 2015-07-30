using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class FireButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	
	private bool touched;
	private int pointerID;
	private bool canFire; 
	private bool canLaunch; 

	public float minSwipeDistY = 100;
	public float minSwipeDistX;
	private Vector2 startPos;
	private Vector2 endPos;

	void Awake () {
		touched = false;
	}


		
//		void Update()
//		{
//			if (Input.touchCount > 0) 
//			{	
//			Debug.Log("Been touched");
//			canFire = true;
//				Touch touch = Input.touches[0];
//
//				switch (touch.phase) 
//					
//				{
//				case TouchPhase.Began:
////					startPos = touch.position;
//					break;
//				case TouchPhase.Ended:
//					
////					float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
//					
////					if (swipeDistVertical > minSwipeDistY) 
//						
//					{
//						
////						float swipeValue = Mathf.Sign(touch.position.y - startPos.y);
//						
////						if (swipeValue > 0)//up swipe
//					{
//						Debug.Log("Swipe up");//Jump ();
//					}		
////							else if (swipeValue < 0)//down swipe
//					{	
//						Debug.Log("Swipe down");//Shrink ();
//					}			
//					}
//					
////					float swipeDistHorizontal = (new Vector3(touch.position.x,0, 0) - new Vector3(startPos.x, 0, 0)).magnitude;
//					
////					if (swipeDistHorizontal > minSwipeDistX) 
//						
//					{
//						
////						float swipeValue = Mathf.Sign(touch.position.x - startPos.x);
//						
//						if (swipeValue > 0)//right swipe
//					{
//							MoveRight ();
//					}		
//							else if (swipeValue < 0)//left swipe
//					{			
								//MoveLeft ();
//					}			
//					}
////					break;
//				}
//			}
//		}


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
		}
	}
	
	public bool CanFire () {
		return canFire;
	}

	public bool CanLaunch () {
		return canLaunch;
	}

}