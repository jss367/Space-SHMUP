	using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereTutorial : MonoBehaviour {

	public Text Step1;
	public Text Step2;
	public Text Step3;
	public GameObject NextButton;
	public GameObject FirePad;


	void OnTriggerEnter(Collider other){
	//		Debug.Log (other);
		if (other.ToString() == "Shield (UnityEngine.SphereCollider)") {
			Step1.enabled = false;
			Step2.enabled = true;
			FirePad.SetActive (true);
			Vector3 pos = new Vector3 (15.0f, 14.0f, 0.0f);
			transform.position = pos;
		} else if (other.ToString() == "ProjectileHero_Tutorial(Clone) (UnityEngine.BoxCollider)") {
			Step2.enabled = false;
			Step3.enabled = true;
			NextButton.SetActive(true);
			Vector3 pos2 = new Vector3 (30.0f, 30.0f, 30.0f);
			transform.position = pos2;
		}
	}
}
