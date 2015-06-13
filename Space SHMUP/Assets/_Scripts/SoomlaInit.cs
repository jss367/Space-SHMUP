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

		int blasters = StoreInventory.GetItemBalance (Constants.BLASTER_WEAPON_ITEM_ID);
		if (blasters == 0) {
			try{
			
			StoreInventory.GiveItem(Constants.BLASTER_WEAPON_ITEM_ID, 1);
			StoreInventory.GiveItem(Constants.GALACTIC_CURRENCY_ITEM_ID, 1000);
			StoreInventory.GiveItem(Constants.SHIELD_ITEM_ID, 1);
			} catch (VirtualItemNotFoundException ex){
				SoomlaUtils.LogError("SOOMLA ExampleEventHandler", ex.Message);
		}


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