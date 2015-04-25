using UnityEngine;
using System.Collections;
using Soomla.Store;
using System;
using System.Collections.Generic;


public class GUI_IAP : MonoBehaviour
{
/*	
	public Font font;
	public GUIStyle labelStyle;
	private Rect removeRect;
	private float midX,midY;
	
	
	public void Start() {
		
		midX = Screen.width/2;
		midY = Screen.height /2;
		removeRect = new Rect(midX - 320*0.5f,midY-256,320,128);
		labelRect = removeRect; labelRect.y -= 100; 
		labelStyle = new GUIStyle ();
		labelStyle.fontSize = 28;
		labelStyle.font = font;
	}
	
	
	void OnGUI() {
		
		
		
		if (GUI.Button(removeRect, "Remove ads")){
			IAPHandler.errorMsg ="Waiting...Connecting to store.";
			
			if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ) {
				if (!StoreInventory.NonConsumableItemExists(GameAssets.NO_ADS.ItemId)) {
					StoreInventory.BuyItem(GameAssets.NO_ADS.ItemId);
				}else{
					SoomlaHandler.errorMsg ="You have already \n bought that item.";
				}
			}else {
				//Testing on the computer e.g
				SoomlaHandler.errorMsg ="Thanks for your purchase. \n It means a lot!";
			}
		}					
		
		
		
		
		
		
		//Handles error message
		GUI.skin.label = labelStyle;
		Vector3 dim = GUI.skin.label.CalcSize(new GUIContent(IAPHandler.errorMsg));
		labelRect.x = Screen.width*0.5f - dim.x*0.5f;
		GUI.Label (labelRect, IAPHandler.errorMsg,labelStyle);
		
		
		
	}
	
	
	
*/	
	
}