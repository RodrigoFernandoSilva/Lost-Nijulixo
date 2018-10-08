using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPrefab : MonoBehaviour {

	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
	}

	void Update () {
		if (!myAudioSource.isPlaying) {
			Destroy (gameObject);
		}
	}
}
