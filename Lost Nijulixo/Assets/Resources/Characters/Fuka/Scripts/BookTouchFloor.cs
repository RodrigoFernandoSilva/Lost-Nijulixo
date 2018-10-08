using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookTouchFloor : MonoBehaviour {

	private AudioSource myAudioSource;

	// Use this for initialization
	void Start () {
		myAudioSource = GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter2D (Collision2D other) {
		myAudioSource.Play ();
		Destroy (GetComponent<BookTouchFloor> ());
	}
}
