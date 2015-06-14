﻿using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;
using MadLevelManager;


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

//		StoreEvents.OnSoomlaStoreInitialized += onSoomlaStoreInitialized;	
		SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets());
		Debug.Log ("Soomla has been initialized");

		MadLevel.LoadLevelByName ("MainMenu");
		Application.LoadLevel ("MainMenu");
	}

	public void onSoomlaStoreInitialized() {
		Debug.Log ("Confirmed that store is initialized");

		int blasters = StoreInventory.GetItemBalance (Constants.BLASTER_WEAPON_ITEM_ID);
		if (blasters == 0) {
			try {
			
				StoreInventory.GiveItem (Constants.BLASTER_WEAPON_ITEM_ID, 1);
				StoreInventory.GiveItem (Constants.GALACTIC_CURRENCY_ITEM_ID, 1000);
				StoreInventory.GiveItem (Constants.SHIELD_ITEM_ID, 1);
			} catch (VirtualItemNotFoundException ex) {
				SoomlaUtils.LogError ("SOOMLA ExampleEventHandler", ex.Message);
			}
		}

		MadLevel.LoadLevelByName ("MainMenu");
		Application.LoadLevel ("MainMenu");
		Debug.Log ("Loaded the main menu");

	}

	void Update(){



//		if(StoreInventory.IsVirtualGoodEquipped (BLASTER_WEAPON_ITEM_ID)){
//			Debug.Log("Blaster is equipped");
//		}
		
//		if(StoreInventory.IsVirtualGoodEquipped (SPREAD_WEAPON_ITEM_ID)){
//			Debug.Log("Spread is equipped");
//		}
}
}