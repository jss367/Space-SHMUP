using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;



public class SoomlaInit : MonoBehaviour {

	private static SoomlaInit instance = null;

	void Awake(){
		if(instance == null){ 	//making sure we only initialize one instance.
			instance = this;
			GameObject.DontDestroyOnLoad(this.gameObject);
		} else {					//Destroying unused instances.
			GameObject.Destroy(this);
		}

	}

	void Start () {

		StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;	
		SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets());
		Debug.Log ("I have init'ed Soomla");
			}

	public void onSoomlaStoreInitialized() {
		Debug.Log ("Confirmed that store is init");
	}
}
