using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacoDePancadas : MonoBehaviour {

	private AudioSource myAudioSource;
	private Animator myAnimator;

	void Awake () {
		myAnimator = GetComponent<Animator> ();
		myAudioSource = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "swordNinja") {
			myAnimator.SetTrigger ("perderVida");
			myAudioSource.Play ();
		}
	}
}
