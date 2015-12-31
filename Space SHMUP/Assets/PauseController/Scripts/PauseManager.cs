using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using System.Collections;
using MadLevelManager;

public class PauseManager : MonoBehaviour {
	public GameObject pausable;
	public Canvas pauseCanvas;

	private bool isPaused = false;
	private Animator anim;
	private Component[] pausableInterfaces;
	private Component[] quittableInterfaces;

	public AudioMixerSnapshot paused;
	public AudioMixerSnapshot unPaused;

	void Start() 
	{
		// PauseManager requires the EventSystem - make sure there is one
		if (FindObjectOfType<EventSystem>() == null)
		{
			var es = new GameObject("EventSystem", typeof(EventSystem));
			es.AddComponent<StandaloneInputModule>();
		}

		pausableInterfaces = pausable.GetComponents (typeof(IPausable));
		quittableInterfaces = pausable.GetComponents (typeof(IQuittable));
		anim = pauseCanvas.GetComponent<Animator> ();

		pauseCanvas.enabled = false;
	}
	
	void Update () {

		pauseCanvas.enabled = isPaused;
		anim.SetBool( "IsPaused", isPaused );
	}


	/// <summary>
	/// TESTING THE BELOW	/// </summary>
	
//	void OnApplicationPause(bool pauseStatus){
//		OnPause ();
//			
//	}
	
	///TESTING ABOVE
	///

		
	public void OnQuit() {
//		Debug.Log ("PauseManager.OnQuit");
//		Application.LoadLevel ("MainMenu");
		foreach (var pausableComponent in pausableInterfaces) {		
			IPausable pausableInterface = (IPausable)pausableComponent;
			if( pausableInterface != null )
				pausableInterface.OnUnPause ();
		}
		MadLevelManager.MadLevel.LoadLevelByName ("MainMenu");
//		foreach (var quittableComponent in quittableInterfaces) {		
//			IQuittable quittableInterface = (IQuittable)quittableComponent;
//			if( quittableInterface != null )
//				quittableInterface.OnQuit ();
//		}		
	}
	
	public void OnUnPause() {
//		Debug.Log ("PauseManager.OnUnPause");	
		isPaused = false;

		foreach (var pausableComponent in pausableInterfaces) {		
			IPausable pausableInterface = (IPausable)pausableComponent;
			if( pausableInterface != null )
				pausableInterface.OnUnPause ();
		}
	}

	public void OnPause() {
//		Debug.Log ("PauseManager.OnPause");
		isPaused = true;

		foreach (var pausableComponent in pausableInterfaces) {		
			IPausable pausableInterface = (IPausable)pausableComponent;
			if( pausableInterface != null )
				pausableInterface.OnPause ();
		}
	}
	public void OnRestart(){
		MadLevel.LoadLevelByName (MadLevel.currentLevelName);
	}
}
