using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	public void SetSFXLevel(float sfxLevel)
	{
		masterMixer.SetFloat ("sfxVol", sfxLevel);
	}

	public void SetMusicLevel(float musicLevel)
	{
		masterMixer.SetFloat ("musicVol", musicLevel);
	}
}
