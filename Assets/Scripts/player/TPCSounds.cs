using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCSounds : MonoBehaviour {

	public AudioClip walk;
	public AudioClip run;
	public AudioClip hey;
	public AudioClip ok;
	public AudioClip no;
	public AudioClip what;
	public AudioClip yeah;
	public static AudioSource audio;
	public static AudioSource extraAudio;
	public static TPCSounds tpcSounds;

	// Use this for initialization
	void Start () {
		//Make sure the game object is this.
		if (tpcSounds == null)
			tpcSounds = this;

		if (tpcSounds != this) {
			Destroy (this);
			return;
		}

		audio = GetComponent<AudioSource> ();
		extraAudio = GameObject.Find ("ExtraSounds").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Play a sound once from any script
	public static void PlaySound(AudioClip AC, float vS) {
		if (audio.loop) {
			audio.loop = false;
		}
		audio.PlayOneShot (AC, vS);
	}

	// Play an extra sound once from any script
	public static void PlayExtraSound(AudioClip AC, float vS) {
		if (extraAudio.loop) {
			extraAudio.loop = false;
		}
		extraAudio.PlayOneShot (AC, vS);
	}

	// Start a loop of a sound
	public static void PlaySoundLoop(AudioClip AC, float vS) {
		if (audio.isPlaying) {
			audio.Stop ();
		}
		audio.loop = true;
		audio.clip = AC;
		audio.volume = vS;
		audio.Play ();
	}

	// Stop the loop of a sound
	public static void StopSoundLoop() {
		audio.Stop ();
		audio.clip = null;
		audio.loop = false;
	}
}
