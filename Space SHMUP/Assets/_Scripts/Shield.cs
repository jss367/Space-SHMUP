using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
	public float rotationsPerSecond = 0.1f;
	public bool					shieldUpgradeOwned = false;
	public bool _________;
	public int levelShown = 0;
	
	void Start ()
	{
		CheckInventory ();
	}

	// Update is called once per frame
	void Update ()
	{
		//Read the current shield level from the Hero Singleton
		int currLevel = Mathf.FloorToInt (Hero.S.shieldLevel);
		//If this is different from levelShown...
		if (levelShown != currLevel) {
			levelShown = currLevel;
			Material mat = this.GetComponent<Renderer> ().material;
			//Adjust the texture offset to show different shield level
			mat.mainTextureOffset = new Vector2 (0.2f * levelShown, 0);
		}
		//Rotate the shield a bit every second
		float rZ = (rotationsPerSecond * Time.time * 360) % 360f;
		transform.rotation = Quaternion.Euler (0, 0, rZ);
	}

	void CheckInventory ()
	{
		try {
			int balance = Soomla.Store.StoreInventory.GetItemBalance (Constants.BASESHIELD_ITEM_ID);
//			Debug.Log("Shield upgrade balance is " + balance);
			if (balance > 0) {
//				Debug.Log("Player has shield upgrade");
				shieldUpgradeOwned = true;
				rotationsPerSecond = 0.25f;
			}
		} catch (System.Exception e) {
			Debug.Log ("Caught error: " + e);
		}
	}
}