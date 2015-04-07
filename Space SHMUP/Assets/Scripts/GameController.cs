
//this is not currently attached to anything
//this is not currently attached to anything
//this is not currently attached to anything
//this is not currently attached to anything
//this is not currently attached to anything

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public Text scoreText;
	public GUIText restartText;  //change these to Text
	public GUIText gameOverText;
	
	private bool gameOver;
	private bool restart;
	private int score;

	public float gameRestartDelay = 2f;

	public float timeMultiplier;
	
		
	void Start ()
	{
		gameOver = false;
		restart = false;
//		restartText.text = "";
//		gameOverText.text = "";
		score = 0;
		UpdateScore ();

	}

	void Update ()
	{
		 timeMultiplier = Time.time / 4;  // Subtract the time of the most recent death

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
		scoreText.text = "Score: " + score;  // should this be score.ToString()?
	}

	public void GameOver ()
	{
		gameOver = true;
	}
	
	public void RestartGame()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
	
}