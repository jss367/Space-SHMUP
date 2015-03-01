using UnityEngine;
using System.Collections;
using Prime31;

public class BeatsController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		MediaPlayerTrack track = MediaPlayerBinding.getCurrentTrack ();
		Debug.Log( "MediaPlayer track: " + track );

		MediaPlayerBinding.play();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
