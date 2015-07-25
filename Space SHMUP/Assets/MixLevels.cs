using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	void Awake(){
		DontDestroyOnLoad(masterMixer);
	}

	public void SetMasterLevel(float masterLevel)
	{
		masterMixer.SetFloat ("masterVol", masterLevel);
	}

	public void SetMusicLevel(float musicLevel)
	{
		masterMixer.SetFloat ("musicVol", musicLevel);
	}

	public void SetSFXLevel(float sfxLevel)
	{
		masterMixer.SetFloat ("sfxVol", sfxLevel);
	}
}
