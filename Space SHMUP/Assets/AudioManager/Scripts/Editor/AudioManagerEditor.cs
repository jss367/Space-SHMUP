using UnityEngine;
using UnityEditor;
using System.Collections;

// Editor-Script for the AudioManager
// by BitsAlive
[CustomEditor (typeof(AudioManager))]
public class AudioManagerEditor : Editor {
	
	public static bool audioClipFoldout = true;
	public static bool fadeOptionsFoldout = false;
	public static bool zoneOptionsFoldout = false;
	
	private static GUIStyle smallButtonStyle = null;
	public static GUIStyle SmallButtonStyle {
		get	{
			if (smallButtonStyle == null) {
				smallButtonStyle = new GUIStyle("Button");
				smallButtonStyle.fontSize = 8;
				smallButtonStyle.alignment = TextAnchor.MiddleCenter;
				smallButtonStyle.margin.left = 1;
				smallButtonStyle.margin.right = 1;
				smallButtonStyle.padding = new RectOffset(2, 4, 2, 2);
			}
			return smallButtonStyle;
		}
	}
	
	private AudioManager am;
	
	
	public void OnEnable() {
		am = (AudioManager)target;
	}
	
	
	public override void OnInspectorGUI() {
		GUILayout.BeginVertical();
		// display the list of AudioClips with it's buttons
		audioClipFoldout = EditorGUILayout.Foldout(audioClipFoldout, "Audio Clip");
		if (audioClipFoldout) {
			EditorGUI.indentLevel++;
			if (am.audioClip != null) {
				for (int i = 0; i < am.audioClip.Count; i++) {
					GUILayout.BeginHorizontal();
					am.audioClip[i] = (AudioClip)EditorGUILayout.ObjectField(am.audioClip[i], typeof(AudioClip), false);
					if (GUILayout.Button("+", SmallButtonStyle, GUILayout.MinWidth(15), GUILayout.MaxWidth(15), GUILayout.MinHeight(15))) {
						am.audioClip.Insert(i, null);
						EditorUtility.SetDirty(am);
					}
					if (GUILayout.Button("-", SmallButtonStyle, GUILayout.MinWidth(15), GUILayout.MaxWidth(15), GUILayout.MinHeight(15))) {
						am.audioClip.RemoveAt(i);
						EditorUtility.SetDirty(am);
					}
					if (GUILayout.Button("^", SmallButtonStyle, GUILayout.MinWidth(15), GUILayout.MaxWidth(15), GUILayout.MinHeight(15))) {
						if (i > 0) {
							am.audioClip.Insert(i - 1, am.audioClip[i]);
							am.audioClip.RemoveAt(i + 1);
							EditorUtility.SetDirty(am);
						}
					}
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.BeginHorizontal();
			GUILayout.Space(13f);
			if (GUILayout.Button("Add a Clip", GUILayout.MinWidth(80), GUILayout.MaxWidth(80))) {
				am.audioClip.Add(null);
				EditorUtility.SetDirty(am);
			}
			GUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}
		
		am.soundType = (AudioManager.SoundType)EditorGUILayout.EnumPopup("Sound Type", am.soundType);
		am.playbackVolume = EditorGUILayout.Slider("Playback Volume", am.playbackVolume, 0f, 1f);
		am.SetVolume();
		am.playOnAwake = EditorGUILayout.Toggle("Play On Awake", am.playOnAwake);
		am.loop = EditorGUILayout.Toggle("Loop", am.loop);
		am.destroyWhenFinished = EditorGUILayout.Toggle("Destroy When Finished", am.destroyWhenFinished);
		am.randomOrder = EditorGUILayout.Toggle("Random Order", am.randomOrder);
		if (am.randomOrder) {
			EditorGUI.indentLevel++;
			am.avoidRepetition = EditorGUILayout.Toggle("Avoid Repetition", am.avoidRepetition);
			EditorGUI.indentLevel--;
		}
		am.intervalFrom = EditorGUILayout.FloatField("Interval From", am.intervalFrom);
		am.intervalTo = EditorGUILayout.FloatField("Interval To", am.intervalTo);
		am.clipStart = EditorGUILayout.FloatField("Clip Start", am.clipStart);
		am.clipEnd = EditorGUILayout.FloatField("Clip End", am.clipEnd);
		am.overlayPercentage = EditorGUILayout.Slider("Overlay Time", am.overlayPercentage, 0f, 1f);
		am.randomPosition = EditorGUILayout.Toggle("Random Position", am.randomPosition);
		if (am.randomPosition)
			ShowInfo("Use Scale of the GameObject to define the Random Zone.");
		
		// display the fade options
		fadeOptionsFoldout = EditorGUILayout.Foldout(fadeOptionsFoldout, "Fade Options");
		if (fadeOptionsFoldout) {
			EditorGUI.indentLevel++;
			am.crossFadeTime = EditorGUILayout.FloatField("Cross Fade Time", am.crossFadeTime);
			am.fadeInTime = EditorGUILayout.FloatField("Fade In Time", am.fadeInTime);
			am.fadeInOnlyOnce = EditorGUILayout.Toggle("Fade In Only Once", am.fadeInOnlyOnce);
			am.fadeOutTime = EditorGUILayout.FloatField("Fade Out Time", am.fadeOutTime);
			am.fadeOutOnlyOnce = EditorGUILayout.Toggle("Fade Out Only Once", am.fadeOutOnlyOnce);
			EditorGUI.indentLevel--;
		}
		GUILayout.EndVertical();
		
		if (GUI.changed) {
			am.CheckSettings();
			EditorUtility.SetDirty(am);
		}
	}
	
	
	void ShowInfo(string info) {
		GUILayout.BeginHorizontal();
		GUILayout.Space(20);
		EditorGUILayout.HelpBox(info, MessageType.Info);
		GUILayout.Space(20);
		GUILayout.EndHorizontal();
	}
}