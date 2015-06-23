using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SphereTutorial : MonoBehaviour {

	public Text Step1;
	public Text Step2;
	public GameObject FirePad;


	void OnTriggerEnter(Collider other){
//		TutorialManager.S.SphereTrigger ();
		Step1.enabled = false;
		Step2.enabled = true;
		FirePad.SetActive (true);
	}
}
