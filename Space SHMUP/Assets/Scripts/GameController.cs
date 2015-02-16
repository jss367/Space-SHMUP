//this is not currently attached to anything

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	
	private bool gameOver;
	private bool restart;
	private int score;
	
	private bool itsOver;
	
	public float gameRestartDelay = 2f;
	
		
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		
		Debug.Log (itsOver + "itsOver");

	}

	void Update ()
	{
		Debug.Log (itsOver + "itsOver");

		if (restart) {
			Debug.Log ("You are in restart mode");
			{
				if (Input.touchCount != 0 || Input.GetKeyDown (KeyCode.R)) {
					Application.LoadLevel (Application.loadedLevel);
				}
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	

	
}