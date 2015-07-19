using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

	public GameObject Sphere;
	public GameObject Joystick;
	public GameObject FirePad;
	public GameObject NextButton;
	public GameObject PlayButton;
	public GameObject ReturnButton;
	public GameObject Asteroid;
	public GameObject Enemy;
	public Text Intro;
	public Text Step1;
	public Text Step2;
	public Text Step3;
	public Text Step4;
	public Text Step5;
	public Text Step6;
	public Text Step7;
	public Text Step8;
	public Text Step9;


	// Use this for initialization
	void Start () {
		Sphere.SetActive (false);
		Joystick.SetActive (false);
		FirePad.SetActive (false);
		Asteroid.SetActive (false);
		Enemy.SetActive (false);
		PlayButton.SetActive (false);
//		onStep1 = false;
		Intro.enabled = true;
		Step1.enabled = false;
		Step2.enabled = false;
		Step3.enabled = false;
		Step4.enabled = false;
		Step5.enabled = false;
		Step6.enabled = false;
		Step7.enabled = false;
		Step8.enabled = false;
		Step9.enabled = false;

	}


	void OnTriggerEnter(Collider other){

	}

	public void onCommand(string str)
	{
		if(str.Equals("Next"))
		{
//			Debug.Log ("Next button");
			if (Intro.enabled == true) {
//				onStep1 = true;
//				Debug.Log (onStep1);
				Intro.enabled = false;
				Step1.enabled = true;
				Sphere.SetActive(true);
				Joystick.SetActive (true);
				NextButton.SetActive(false);
			}

			else if (Step3.enabled == true) {
//				Debug.Log ("leaving step 3");
				Step3.enabled = false;
				Step4.enabled = true;
			}
			else if (Step4.enabled == true) {
				Step4.enabled = false;
				Step5.enabled = true;
				Asteroid.SetActive (true);
				Enemy.SetActive (true);
				NextButton.SetActive(false);
				InvokeRepeating ("WaitUntilLevelEmpties", 0.0f, 0.5f);
			}
			else if (Step6.enabled == true) {
				Step6.enabled = false;
				Step7.enabled = true;
				}
			else if (Step7.enabled == true) {
				Step7.enabled = false;
				Step8.enabled = true;
			}
			else if (Step8.enabled == true) {
				Step8.enabled = false;
				Step9.enabled = true;
				PlayButton.SetActive(true);
				NextButton.SetActive(false);
			}
		}

		if (str.Equals ("LevelSelect")) {
			Application.LoadLevel("LevelManager");
		}

		if (str.Equals ("MainMenu")) {
			Application.LoadLevel("MainMenu");
		}
			}


	
	public void WaitUntilLevelEmpties(){
		

		GameObject[] AsteroidsRemaining = GameObject.FindGameObjectsWithTag ("Asteroid");
		//		Debug.Log ("Asteroids remaining: " + AsteroidsRemaining.Length);
		GameObject[] EnemiesRemaining = GameObject.FindGameObjectsWithTag ("Enemy");
		//		Debug.Log ("Enemies remaining: " + EnemiesRemaining.Length);
		if (EnemiesRemaining.Length == 0 && AsteroidsRemaining.Length == 0) {
			Step6.enabled = true;
			Step5.enabled = false;
			NextButton.SetActive(true);
			CancelInvoke("WaitUntilLevelEmpties");
		}
		
	}

}
