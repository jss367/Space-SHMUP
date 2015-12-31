using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace FMG
{
	public class ClickEvent : MonoBehaviour, IPointerClickHandler {

		public virtual void OnPointerClick(PointerEventData eventData)
		{
			Debug.Log ("ClickEvent - is this needed?" );
			if(GetComponent<AudioSource>())
				GetComponent<AudioSource>().Play();
		}
	}
}
