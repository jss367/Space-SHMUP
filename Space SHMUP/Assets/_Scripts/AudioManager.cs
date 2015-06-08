using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MadLevelManager;

// AudioManager gives you various options for audio playback.
// by BitsAlive
[AddComponentMenu("BitsAlive/Audio/AudioManager")]
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour {
	
	public enum SoundType {
		ambient,
		music,
		sound
	}
	public SoundType soundType;						// type of sound, is used in conjunction with the GlobalVolumeManager
	
	public List<AudioClip> audioClip;				// list of AudioClips
	public float playbackVolume = 0.5f;				// playback volume, the actual volume will be combination of playbackVolume and the volume from the GlobalVolumeManager
	public bool playOnAwake = true;					// start playback in the Awake function
	public bool loop = false;						// loop the AudioClips
	public bool destroyWhenFinished = false;		// the GameObject will be destoyed after playback has finished
	public bool randomOrder = false;				// if true: AudioClips are played in random order, otherwise they are played in the order of audioClip (the list)
	public bool avoidRepetition = false;			// if randomOrder is true, don't play the same AudioClip multiple times in a row
	public float intervalFrom = 0f;					// interval in seconds, before playing the next AudioClip (e.g. from 5 to 10 seconds)
	public float intervalTo = 0f;					// interval in seconds, before playing the next AudioClip (e.g. from = 5 and to = 5: every 5 seconds)
	public float overlayPercentage = 0f;			// percentage of the length of the AudioClip, at which the next AudioClip should start (measured from the end of the clip, like a cross-fade without the fading; e.g. 0.1f means that at 90% of current AudioClip the next AudioClip will start playing)
	
	public float clipStart = 0f;					// time when the audioclip starts
	public float clipEnd = 0f;						// time when the audioclip ends
	
	public bool randomPosition = false;				// sets the GameObject to a random position (Scale of the GameObject is used for the boundaries)
	private Vector3 fromPosition;
	private Vector3 toPosition;
	
	public float crossFadeTime = 0f;				// time for fading between two AudioClips (the first clip is faded out while the second clip is faded in)
	public float fadeInTime = 0f;					// time for the fade-in, this is done on every start of a AudioClip
	public bool fadeInOnlyOnce = false;				// time for the fade-in, this is done only once at the start of the first AudioClip
	public float fadeOutTime = 0f;					// time for the fade-out, this is done on every end of a AudioClip
	public bool fadeOutOnlyOnce = false;			// time for the fade-out, this is done only once at the end of the first AudioClip
	
	public AudioSource audio1;                      // AudioManager needs two AudioSources for effects like cross-fade and overlaping
	public AudioSource audio2;
	private float audioVolume;
	private float internalVolume;
	private int clipIndex;
	private int lastIndex = -1;
	private bool firstSound = true;
	private bool stop = false;
	private Color gizmoColor = new Color(0x75/255f, 0xa7/255f, 0xc3/255f);

	public string currentLevel;

	public float timeLimit;
	
	void Awake() {
//		Debug.Log ("AudioManager.cs has awakened");
		// check, if there are two AudioSources
		AudioSource[] a = gameObject.GetComponents<AudioSource>();
		if ((a == null) || (a.Length < 2)) {
			Debug.LogError("AudioManager (" + gameObject.name + "): There should be two AudioSources on the AudioManager!");
			return;
		}
		audio1 = a[0];
		audio2 = a[1];
		audio1.loop = false;
		audio2.loop = false;

		currentLevel = MadLevel.currentLevelName;

		switch (currentLevel) {
		case "Level 1":
			audioClip [0] = Resources.Load ("Music/1Mix_48") as AudioClip;
			break;
		case "Level 2":
			audioClip [0] = Resources.Load ("Music/2_90-12Remix_48") as AudioClip;
			break;
		case "Level 3":
			audioClip [0] = Resources.Load ("Music/3ChecksForFree_48") as AudioClip;
			break;
		case "Level 4":
			audioClip [0] = Resources.Load ("Music/4Sci-Fi_48") as AudioClip;
			break;
		case "Level 5":
			audioClip [0] = Resources.Load ("Music/5Ectoplasm_48") as AudioClip;
			break;
		case "Level 6":
			audioClip [0] = Resources.Load ("Music/6Thumpette_48") as AudioClip;
			break;
		case "Level 7":
			audioClip [0] = Resources.Load ("Music/7FunkyJunky_48") as AudioClip;
			break;
		case "Level 8":
			audioClip [0] = Resources.Load ("Music/8_120_48") as AudioClip;
			break;
		case "Level 9":
			audioClip [0] = Resources.Load ("Music/9Roboskater_48") as AudioClip;
			break;
		case "Level 10":
			audioClip [0] = Resources.Load ("Music/10Cresc_48") as AudioClip;
			break;
		}

		
		// check, the clips are assigned
		if ((audioClip != null) && (audioClip.Count == 0)) {
			Debug.LogWarning("AudioManager (" + gameObject.name + "): There are no audioClips defined!");
			return;
		}
		for (int i = 0; i < audioClip.Count; i++)
		if (audioClip[i] == null) {
			Debug.LogError("AudioManager (" + gameObject.name + "): audioClip[" + i + "] is not defined!");
			return;
		}


		// check of the settings
		CheckSettings();
		
		// from-to-positions for the random positons
		if (randomPosition) {
			fromPosition = new Vector3(transform.position.x - transform.localScale.x * 0.5f, transform.position.y - transform.localScale.y * 0.5f, transform.position.z - transform.localScale.z * 0.5f);
			toPosition = new Vector3(transform.position.x + transform.localScale.x * 0.5f, transform.position.y + transform.localScale.y * 0.5f, transform.position.z + transform.localScale.z * 0.5f);
		}
		
		// playback of the first AudioClip
		clipIndex = -1;
		if (playOnAwake) { 
			Play ();
//			Debug.Log("AudioManager.cs is starting the audioClip");
		}
		timeLimit = audioClip [0].length;
//		Debug.Log ("The song length is " + timeLimit);
	}


	void OnDrawGizmosSelected() {
		// if the current GameObject is selected: display a blue gizmo for the range of the random positions (Scale of the GameObject)
		if (randomPosition) {
			Gizmos.color = gizmoColor;
			Gizmos.DrawWireCube(transform.position, transform.localScale);
		}

	}

	
	
	/// <summary>
	/// Checks the settings.
	/// </summary>
	public void CheckSettings() {
		if ((crossFadeTime > 0f) && (overlayPercentage > 0f)) {
			Debug.LogWarning("AudioManager: OverlayPercentage not allowed in conjunction with CrossFadeTime! OverlayPercentage (" + overlayPercentage + ") is now zero.");
			overlayPercentage = 0f;
		}
		if ((crossFadeTime > 0f) && ((intervalFrom > 0f) || (intervalTo > 0f))) {
			Debug.LogWarning("AudioManager: Interval not allowed in conjunction with CrossFadeTime! IntervalFrom (" + intervalFrom + ") and IntervalTo (" + intervalTo + ") are now zero.");
			intervalFrom = 0f;
			intervalTo = 0f;
		}
		if (intervalTo < intervalFrom) {
			Debug.LogWarning("AudioManager: IntervalTo (" + intervalTo + ") < IntervalFrom (" + intervalFrom + ")! IntervalTo is now equal to IntervalFrom.");
			intervalTo = intervalFrom;
		}
		if ((crossFadeTime > 0f) && (fadeInTime > 0f) && (!fadeInOnlyOnce)) {
			fadeInOnlyOnce = true;
			Debug.LogWarning("AudioManager: FadeInTime in conjunction with CrossFadeTime is only allowed with FadeInOnlyOnce! FadeInOnlyOnce is now true.");
		}
		if ((crossFadeTime > 0f) && (fadeOutTime > 0f)) {
			Debug.LogWarning("AudioManager: FadeOutTime not allowed in conjunction with CrossFadeTime! FadeOutTime is now zero.");
			fadeOutTime = 0f;
		}
		if ((overlayPercentage > 0f) && ((intervalFrom > 0f) || (intervalTo > 0f))) {
			Debug.LogWarning("AudioManager: Interval not allowed in conjunction with OverlayPercentage! IntervalFrom (" + intervalFrom + ") and IntervalTo (" + intervalTo + ") are now zero.");
			intervalFrom = 0f;
			intervalTo = 0f;
		}
		if ((overlayPercentage > 0f) && (fadeInTime > 0f) && (!fadeInOnlyOnce)) {
			fadeInOnlyOnce = true;
			Debug.LogWarning("AudioManager: FadeInTime in conjunction with OverlayPercentage is only allowed with FadeInOnlyOnce! FadeInOnlyOnce is now true.");
		}
		if ((overlayPercentage > 0f) && (fadeOutTime > 0f)) {
			Debug.LogWarning("AudioManager: FadeOutTime not allowed in conjunction with OverlayPercentage! FadeOutTime is now zero.");
			fadeOutTime = 0f;
		}
	}
	
	
	/// <summary>
	/// Sets the audio volume: a combination of playbackVolume and the volume from the GlobalVolumeManager.
	/// </summary>
	public void SetVolume() {
		switch (soundType) {
		case SoundType.ambient:
			audioVolume = playbackVolume * GlobalVolumeManager.Instance.ambientVolume;
			break;
		case SoundType.music:
			audioVolume = playbackVolume * GlobalVolumeManager.Instance.musicVolume;
			break;
		default:
			audioVolume = playbackVolume * GlobalVolumeManager.Instance.soundVolume;
			break;
		}
		internalVolume = audioVolume;
		if (audio1)
			audio1.volume = internalVolume;
		if (audio2)
			audio2.volume = internalVolume;
	}
	
	
	// returns the index of the next AudioClip
	private int GetNextClipIndex() {
		if (randomOrder) {
			if ((avoidRepetition) && (audioClip.Count > 1)) {
				do {
					clipIndex = Random.Range(0, audioClip.Count);
				} while (clipIndex == lastIndex);
				lastIndex = clipIndex;
			}
			else
				clipIndex = Random.Range(0, audioClip.Count);
		}
		else {
			clipIndex++;
			if (clipIndex >= audioClip.Count)
				clipIndex = 0;
		}
		return clipIndex;
	}
	
	
	private IEnumerator FadeIn(AudioSource a, float timeToFade) {
		float startTime = Time.time;
		float elapsedTime = 0f;
		do {
			elapsedTime = Time.time - startTime;
			a.volume = internalVolume * elapsedTime / timeToFade;
			yield return null;
		} while ((elapsedTime < timeToFade) && (!stop));
		a.volume = internalVolume;
	}
	
	
	private IEnumerator FadeOut(AudioSource a, float timeToFade) {
		float startTime = Time.time;
		float elapsedTime = 0f;
		do {
			elapsedTime = Time.time - startTime;
			a.volume = internalVolume * (1f - elapsedTime / timeToFade);
			yield return null;
		} while ((elapsedTime < timeToFade) && (!stop));
		a.volume = 0f;
	}
	
	
	private IEnumerator PlaySound() {
		float timeToWait;
		float overlayTime = 0f;
		
		while (!stop) {			
			// wait before the next AudioClip is played
			if (intervalTo > 0f) {
				yield return new WaitForSeconds(Random.Range(intervalFrom, intervalTo));
				if (stop) break;
			}
			
			if ((clipStart > 0f) && (Time.time < clipStart)) {
				yield return new WaitForSeconds(clipStart);
				if (stop) break;
			}
			
			if ((clipEnd > 0f) && (Time.time > clipEnd)) {
				break;
			}
			
			
			
			
			// playback of the 1. AudioClip
			if (randomPosition)
				transform.position = new Vector3(Random.Range(fromPosition.x, toPosition.x), Random.Range(fromPosition.y, toPosition.y), Random.Range(fromPosition.z, toPosition.z));
			audio1.volume = 0f;
			audio1.clip = audioClip[GetNextClipIndex()];
			audio1.Play();
			
			// fade-in of the 1. AudioClip
			if ((fadeInTime > 0f) && ((!fadeInOnlyOnce) || (firstSound)))
				yield return StartCoroutine(FadeIn(audio1, fadeInTime));
			else {
				if ((crossFadeTime > 0f) && (!firstSound))  // cross-fade of the 1. AudioClips
					yield return StartCoroutine(FadeIn(audio1, crossFadeTime));
				else
					audio1.volume = internalVolume;
			}
			if (stop) break;
			
			// wait until the 1. AudioClip has almost finished
			timeToWait = audio1.clip.length - fadeInTime - fadeOutTime - crossFadeTime - crossFadeTime - 1f;
			if (overlayPercentage > 0f) {
				overlayTime = audio1.clip.length * overlayPercentage;
				timeToWait -= overlayTime;
			}
			if (timeToWait > 0f)
				yield return new WaitForSeconds(timeToWait);
			else
				yield return null;
			if (stop) break;
			
			// fade-out of the 1. AudioClip
			if ((fadeOutTime > 0f) && ((!fadeOutOnlyOnce) || (firstSound))) {
				while ((audio1.isPlaying) && (audio1.time + fadeOutTime < audio1.clip.length))
					yield return null;
				if (stop) break;
				yield return StartCoroutine(FadeOut(audio1, fadeOutTime));
			}
			else {
				if (crossFadeTime > 0f) {
					while ((audio1.isPlaying) && (audio1.time + crossFadeTime < audio1.clip.length))
						yield return null;
				}
				else if (overlayPercentage > 0f) {
					while ((audio1.isPlaying) && (audio1.time + overlayTime < audio1.clip.length))
						yield return null;
				}
				else {
					while (audio1.isPlaying)
						yield return null;
				}
			}
			firstSound = false;
			if (stop) break;
			
			// destroy the GameObject and exit
			if (destroyWhenFinished) {
				Destroy(gameObject);
				break;
			}
			
			// don't loop: exit
			if (!loop)
				break;
			
			// cross-fade to the 2. AudioClip
			if (crossFadeTime > 0f) {
				// fade-out the 1. AudioClip
				StartCoroutine(FadeOut(audio1, crossFadeTime));
				
				// and fade-in the 2. AudioClips at the same time
				if (randomPosition)
					transform.position = new Vector3(Random.Range(fromPosition.x, toPosition.x), Random.Range(fromPosition.y, toPosition.y), Random.Range(fromPosition.z, toPosition.z));
				audio2.volume = 0f;
				audio2.clip = audioClip[GetNextClipIndex()];
				audio2.Play();
				yield return StartCoroutine(FadeIn(audio2, crossFadeTime));
				if (stop) break;
				
				// wait until the 2. AudioClip has almost finished
				timeToWait = audio2.clip.length - crossFadeTime - crossFadeTime - 1f;
				if (timeToWait > 0f)
					yield return new WaitForSeconds(timeToWait);
				
				// wait until the the fade-out of the 2. AudioClip should start
				while ((audio2.isPlaying) && (audio2.time + crossFadeTime < audio2.clip.length))
					yield return null;
				if (stop) break;
				
				// fade-out the 2. AudioClip and fade-in the next AudioClip at the beginning of the while-loop
				StartCoroutine(FadeOut(audio2, crossFadeTime));
			}
			
			// overlay the 2. AudioClip
			if (overlayPercentage > 0f) {
				if (randomPosition)
					transform.position = new Vector3(Random.Range(fromPosition.x, toPosition.x), Random.Range(fromPosition.y, toPosition.y), Random.Range(fromPosition.z, toPosition.z));
				audio2.volume = 0f;
				audio2.clip = audioClip[GetNextClipIndex()];
				audio2.Play();
				
				// wait until the 2. AudioClip has almost finished
				overlayTime = audio2.clip.length * overlayPercentage;
				timeToWait = audio2.clip.length - overlayTime - 1f;
				if (timeToWait > 0f)
					yield return new WaitForSeconds(timeToWait);
				
				// wait until the 2. AudioClip has finished and playback the next AudioClip at the beginning of the while-loop
				while ((audio2.isPlaying) && (audio2.time + overlayTime < audio2.clip.length))
					yield return null;
			}
		}
	}
	
	
	/// <summary>
	/// Plays the AudioClip at the specified clipIdx.
	/// </summary>
	/// <param name='clipIdx'>
	/// Clip index.
	/// </param>
	public void Play(int clipIdx = -1) {
		SetVolume();
		stop = false;
		if ((clipIdx >= 0) && (clipIdx < audioClip.Count))
			clipIndex = clipIdx - 1;
		StartCoroutine(PlaySound());
	}
	
	
	/// <summary>
	/// Stops the playback.
	/// </summary>
	public void Stop() {
		stop = true;
		if (audio1)
			audio1.Stop();
		if (audio2)
			audio2.Stop();
	}
	
	
	/// <summary>
	/// Gets a value indicating whether this <see cref="AudioManager"/> is playing.
	/// </summary>
	/// <value>
	/// <c>true</c> if is playing; otherwise, <c>false</c>.
	/// </value>
	public bool isPlaying {
		get { return audio1.isPlaying || audio2.isPlaying; }
	}
	
	
	/// <summary>
	/// Plays the specified AudioClip.
	/// </summary>
	/// <param name='source'>
	/// AudioSource that plays the clip.
	/// </param>
	/// <param name='clip'>
	/// AudioClip.
	/// </param>
	/// <param name='volume'>
	/// Volume.
	/// </param>
	public void PlayClip(AudioSource source, AudioClip clip, float volume) {
		playbackVolume = volume;
		stop = false;
		SetVolume();
		source.clip = clip;
		source.Play();
	}
}