using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukaAnimation : MonoBehaviour {

	public GameObject soundPrefab;
	public AudioClip[] sounds;
	public float[] soundsVelume;

	private void PlayChesting () {
		PlaySoundEffect (0);
	}

	public void PlayChestOping () {
		PlaySoundEffect (1);
	}

	public void PlayThown () {
		PlaySoundEffect (2);
	}

	private void PlaySoundEffect (int indice) {
		AudioSource prefabAudioSource = Instantiate (soundPrefab, transform).GetComponent<AudioSource> ();
		prefabAudioSource.clip = sounds [indice];
		prefabAudioSource.volume = soundsVelume [indice];

		prefabAudioSource.Play ();
	}
}
