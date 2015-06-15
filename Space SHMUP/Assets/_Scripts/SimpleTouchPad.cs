//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using System.Collections;
//
//public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {
//	
//	public float smoothing = 0.1f; 
//	
//	private Vector2 origin;
//	private Vector2 direction;
//	private Vector2 smoothDirection;
//	private bool touched;
//	private int pointerID;
//	
//	void Awake() {
//		direction = Vector2.zero;
//		touched = false;
//	}
//	
//	public void OnPointerDown(PointerEventData data){
//		if (!touched) {
////			Debug.Log("The touchpad has been touched");
//			// set our start point
//			origin = data.position;
//			pointerID = data.pointerId;
//			touched = true;
//		}
//	}
//	
//	public void OnDrag(PointerEventData data){
//		// Compare the difference between our start point and current pointer position
//		if (data.pointerId == pointerID) {
//			Vector2 currentPosition = data.position;
//			Vector2 directionRaw = currentPosition - origin;
//			direction = directionRaw.normalized;
////			Debug.Log ("The direction in SimpleTouchPad is " + direction);
//		}
//	}
//	
//	public void OnPointerUp(PointerEventData data){
//		if (data.pointerId == pointerID) {
//			// Reset everything
//			direction = Vector2.zero;
//			touched = false;
//		}
//	}
//	
//	public Vector2 GetDirection() {
//		smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
////		Debug.Log ("The smoothDirection in SimpleTouchPad is " + smoothDirection);
//		return smoothDirection;
//	}
//}
