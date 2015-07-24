using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace FMG
{
	public class MainMenu : MonoBehaviour {
		public GameObject mainMenu;
		public GameObject levelSelectMenu;
		public GameObject settingsMenu;
		public GameObject creditsMenu;
		public GameObject storeMenu;

		public bool useLevelSelect = true;
		public bool useExitButton = true;

		public GameObject exitButton;


		public void Awake()
		{
			if(useExitButton==false)
			{
				exitButton.SetActive(false);
			}
		}

		public void onCommand(string str)
		{
			if(str.Equals("Start"))
			{
				Debug.Log ("Start");
//				Constants.fadeInFadeOut(storeMenu,mainMenu);
				Application.LoadLevel("LevelManager");
				/*
				if(useLevelSelect)
				{
					Constants.fadeInFadeOut(levelSelectMenu,mainMenu);
				}else{
					Application.LoadLevel(1);
				}
				*/
			}


			if(str.Equals("Store"))
			{
				Constants.fadeInFadeOut(storeMenu,mainMenu);
				/*
				if(useLevelSelect)
				{
					Constants.fadeInFadeOut(levelSelectMenu,mainMenu);
				}else{
					Application.LoadLevel(1);
				}
				*/
			}

			if(str.Equals("Tutorial"))
			   {
				Application.LoadLevel("TutorialScene");
			}

			if(str.Equals("LevelSelectBack"))
			{
				Constants.fadeInFadeOut(mainMenu,levelSelectMenu);

			}
			if(str.Equals("Exit"))
			{
				Application.Quit();
			}
			if(str.Equals("Credits"))
			{
				Constants.fadeInFadeOut(creditsMenu,mainMenu);

			}
			if(str.Equals("CreditsBack"))
			{
				Constants.fadeInFadeOut(mainMenu,creditsMenu);
			}

			if(str.Equals("StoreBack"))
			{
				Constants.fadeInFadeOut(mainMenu,storeMenu);
				storeMenu.SetActive (false);
				
			}
			
			if(str.Equals("SettingsBack"))
			{
				Constants.fadeInFadeOut(mainMenu,settingsMenu);

			}
			if(str.Equals("Settings"))
			{
				Debug.Log("Bringing out settings panel");
				Constants.fadeInFadeOut(settingsMenu,mainMenu);
			}

			if(str.Equals("MainMenu"))
			{
//				Constants.fadeInFadeOut(mainMenu, 
				Application.LoadLevel("MainMenu");
			}


		}
	}
}
