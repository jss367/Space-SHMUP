using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScriptHelper : MonoBehaviour {

//	public static Color green;
	public Text ScoreText;

	void Awake()
	{
//		ScoreText = 
	}

	// Use this for initialization
	void Start () {
//		Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

		ScoreText.color = Color.green;
		ScoreText.fontSize = 24;
		ScoreText.verticalOverflow = VerticalWrapMode.Overflow;
		ScoreText.rectTransform.anchoredPosition = Vector2.zero;
//		ScoreText.rectTransform.localPosition = Vector2(0,0);
//		ScoreText.font = Font;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
