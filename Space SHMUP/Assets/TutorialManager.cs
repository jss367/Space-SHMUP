using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

//	public TutorialManager		S;
	public GameObject Sphere;
	public GameObject Joystick;
	public GameObject FirePad;
	public GameObject NextButton;
	public GameObject Asteroid;
	public GameObject Enemy;
	public Text Intro;
	public Text Step1;
	public Text Step2;
	public Text Step3;
	public Text Step4;
	public Text Step5;
	public Text Step6;
	private bool onStep1;

//	void Awake(){
//		
//		S = this;
//
//	}

	// Use this for initialization
	void Start () {
		Sphere.SetActive (false);
		Joystick.SetActive (false);
		FirePad.SetActive (false);
		Asteroid.SetActive (false);
		Enemy.SetActive (false);
//		onStep1 = false;
		Intro.enabled = true;
		Step1.enabled = false;
		Step2.enabled = false;
		Step3.enabled = false;
		Step4.enabled = false;
		Step5.enabled = false;
		Step6.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){

	}

	public void onCommand(string str)
	{
		if(str.Equals("Next"))
		{
			Debug.Log ("Next button");
			if (Intro.enabled == true) {
//				onStep1 = true;
				Debug.Log (onStep1);
				Intro.enabled = false;
				Step1.enabled = true;
				Sphere.SetActive(true);
				Joystick.SetActive (true);
				NextButton.SetActive(false);
			}

			else if (Step3.enabled == true) {
				Step3.enabled = false;
				Step4.enabled = true;
				Asteroid.SetActive (true);
				Enemy.SetActive (true);
				NextButton.SetActive(false);
				InvokeRepeating ("WaitUntilLevelEmpties", 0.0f, 0.5f);
			}
			else{
				Application.LoadLevel("LevelManager");
			}
		}



		
	}

	public void WaitUntilLevelEmpties(){
		

		GameObject[] AsteroidsRemaining = GameObject.FindGameObjectsWithTag ("Asteroid");
		//		Debug.Log ("Asteroids remaining: " + AsteroidsRemaining.Length);
		GameObject[] EnemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy");
		//		Debug.Log ("Enemies remaining: " + EnemiesRemaining.Length);
		if (EnemiesRemaining.Length == 0 && AsteroidsRemaining.Length == 0) {
			Step5.enabled = true;
			Step4.enabled = false;
			NextButton.SetActive(true);
		}
		
	}

//	public void SphereTrigger(){
//
//		Debug.Log (onStep1);
//		// If other is hero
//		//		if (onStep1) {
//		Step1.enabled = false;
//		Step2.enabled = true;
//		//		FirePad.SetActive (true);	
//		//		}
//		// Is other is projectile
//
//	}
}
