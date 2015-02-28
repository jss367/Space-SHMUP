using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Prime31;
namespace FMG
{
	public class ClickSelect : MonoBehaviourGUI, IPointerClickHandler {
		
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log ("ClickEvent" );
			MediaPlayerBinding.showMediaPicker();
		}
	}
}
