using UnityEngine;
using System.Collections;
using Soomla;
using Soomla.Store;

public class SoomlaHelper : MonoBehaviour {
	static public SoomlaHelper		S;
	public bool spreadOwned = false;
	public bool blasterOwned = false;
	public bool shieldUpgradeOwned = false;

	private static SoomlaHelper instance = null;
	
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
//		SoomlaStore.Initialize(new Soomla.Store.Example.GalacticAssets());
//		Debug.Log ("Soomla has been initialized");
	}
	
	public void onSoomlaStoreInitialized() {
//		Debug.Log ("Confirmed that store is initialized");

		if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.BLASTER_WEAPON_ITEM_ID)){
			Debug.Log("Blaster is equipped");
			//			spreadOwned = false;
		}
		if(Soomla.Store.StoreInventory.IsVirtualGoodEquipped (Constants.SPREAD_WEAPON_ITEM_ID)){
			Debug.Log("Spread is equipped");
			spreadOwned = true;
		}

				int shieldUpgrade = Soomla.Store.StoreInventory.GetItemBalance (Constants.SHIELD_ITEM_ID);
		//		Debug.Log ("Shield upgrade: " + shieldUpgrade);
				if ((shieldUpgrade >= 1))	{
		shieldUpgradeOwned = true;
					Debug.Log("Player owns shield upgrade");
				}
		
	}

}
