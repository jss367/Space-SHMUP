using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public GameObject Sphere;
	public GameObject Joystick;
	public GameObject FirePad;
	public GameObject NextButton;
	public Text Intro;
	public Text Step1;
	public Text Step2;
	public Text Step3;
	public Text Step4;
	public Text Step5;
	private bool onStep1;


	// Use this for initialization
	void Start () {
		Sphere.SetActive (false);
		Joystick.SetActive (false);
		FirePad.SetActive (false);
		onStep1 = false;
		Intro.enabled = true;
		Step1.enabled = false;
		Step2.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		Debug.Log (onStep1);
		// If other is hero
//		if (onStep1) {
		Step1.enabled = false;
		Step2.enabled = true;
		FirePad.SetActive (true);	
//		}
		// Is other is projectile
	}

	public void onCommand(string str)
	{
		if(str.Equals("Next"))
		{
			Debug.Log ("Next button");
			if (Intro.enabled == true) {
				onStep1 = true;
				Debug.Log (onStep1);
				Intro.enabled = false;
				Step1.enabled = true;
				Sphere.SetActive(true);
				Joystick.SetActive (true);
				NextButton.SetActive(false);
			}
		}



		
	}

	public void Next(){



	}
}
