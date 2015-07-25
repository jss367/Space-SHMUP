using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour {

	public AudioMixer masterMixer;

	public void SetMasterLevel(float masterLevel)
	{
		masterMixer.SetFloat ("masterVol", masterLevel);
	}
//
//	public void SetMusicLevel(float musicLevel)
//	{
//		masterMixer.SetFloat ("musicVol", musicLevel);
//	}
}
