using System;
using UnityEngine;
using UnityEngine.UI;

public class SoomlaView : MonoBehaviour
{
	#region Enums
	public enum MenuState
	{
		MainMenu = 0,
		Profile = 1,
		Store = 2
	}
	#endregion
	
	#region Private Variables
	private MenuState currentMenuState = MenuState.Store;
	#endregion
	
	#region Editor Properties
//	public Button BackButton;
	public GameObject LoadingOverlay;
	#endregion
	
	#region Static
	private static SoomlaView instance; 
	private static bool? neededBeforeInit;
	#endregion
	
	public static void SetLoadingOverlayVisiblity(bool state)
	{
		if (instance != null)
		{
			var isActive = state && instance.currentMenuState != MenuState.MainMenu;
			
			if (isActive)
			{
				instance.LoadingOverlay.SetActive(true); 
			}
			else
			{
				if (Application.isEditor)
				{
					instance.WaitAndDo(.5f, () => instance.LoadingOverlay.SetActive(false));
				}
				else
				{
					instance.LoadingOverlay.SetActive(false);
				}
			}
		}
		else
		{
			Debug.Log("Not yet...");
			
			neededBeforeInit = state;
		}
	}
	
	protected void Start()
	{
		ToggleMenuGameObjects();
		
		//        BackButton.onClick.AddListener(NavigateToMainMenu);
		
		instance = this;
		
		SetLoadingOverlayVisiblity(neededBeforeInit.HasValue && neededBeforeInit.Value);
		
		//		NavigateToStore ();
	}
	
//	protected void Update()
//	{
//		if (currentMenuState != MenuState.MainMenu && Input.GetKey(KeyCode.Escape))
//		{
//			NavigateToMainMenu();
//		}
//		//		Debug.Log ("You have purchased:");
//		//		Debug.Log(Soomla.Store.StoreInventory.GetItemBalance("weapon_blaster"));
//	}

	public void onCommand(string str)
	{
		if (str.Equals ("MainMenu")) {

		Application.LoadLevel ("MainMenu");

		}
	}
	
//	public void NavigateToProfile()
//	{
//		currentMenuState = MenuState.Profile;
//		ToggleMenuGameObjects();
//	}
	
	public void NavigateToStore()
	{
		currentMenuState = MenuState.Store;
		ToggleMenuGameObjects();
	}
	
	public void NavigateToMainMenu()
	{
		LoadingOverlay.SetActive(false);
		
		currentMenuState = MenuState.MainMenu;
		ToggleMenuGameObjects();
	}
	
	private void ToggleMenuGameObjects()
	{
		foreach (var menuState in Enum.GetNames(typeof(MenuState)))
		{
			var menuStateObject = transform.FindChild(menuState);
			
			if (menuStateObject)
			{
				// Activate it first anyways to make sure it initializes
				menuStateObject.gameObject.SetActive(true);
				menuStateObject.gameObject.SetActive(string.Equals(menuState, currentMenuState.ToString(), StringComparison.CurrentCultureIgnoreCase)); 
			}
		}
		
//		BackButton.gameObject.SetActive(currentMenuState != MenuState.MainMenu);
	}
}
