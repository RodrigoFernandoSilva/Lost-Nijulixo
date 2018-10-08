using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour {

	private DATA_SOUNDS_EFFECTS WORLD_DATA_SOUNDS_EFFECTS;

	public GameObject soundPrefab;
	private JumpSystem myJumpSystem;
	private Components fatherComponents;
	public BoxCollider2D sword;

	private int lastSoundEnable;

	private int indice;

	// Use this for initialization
	void Awake () {
		//I am goint to father and after I look for the <JumpSystem> in children.
		myJumpSystem = GetComponentInParent <Components> ().GetComponentInChildren<JumpSystem> ();

		WORLD_DATA_SOUNDS_EFFECTS = FindObjectOfType <DATA_SOUNDS_EFFECTS> ();

		fatherComponents = GetComponentInParent<Components> ();
	}

	private void PlayPickup () {
		indice = 5;

		AudioSource prefabAudioSource = Instantiate (soundPrefab, transform).GetComponent<AudioSource> ();
		prefabAudioSource.clip = WORLD_DATA_SOUNDS_EFFECTS.sounds [indice];
		prefabAudioSource.volume = WORLD_DATA_SOUNDS_EFFECTS.soundsVelume [indice];

		prefabAudioSource.Play ();
	}

	private void Jump () {
		fatherComponents.jump = true;
	}

	private void PlayWalking () {
		switch (myJumpSystem.kindOfFloor) {
		case 1:
			PlaySound (0, 3);
			break;
		case 2:
			PlaySound (5, 8);
			break;
		case 3:
			PlaySound (8, 10);
			break;
		}
	}

	private void PlayJump () {
		PlaySound (3, 5);
	}

	void PlayOgroAttack () {
		PlaySound (15, 18);
	}

	void PlayNinjaAttack () {
		if (Random.Range (0f, 100f) < 45f) {
			PlaySound (11, 14);
		}
	}

	private void ImpulseInX (float force) {
		if (fatherComponents.transform.localScale.x > 0) {
			fatherComponents.myRigidbody2D.AddForce (Vector2.right * force, ForceMode2D.Impulse);
		} else {
			fatherComponents.myRigidbody2D.AddForce (Vector2.right * -force, ForceMode2D.Impulse);
		}
	}

	private void Flip () {
		fatherComponents.transform.localScale = new Vector3 (-fatherComponents.transform.localScale.x,
			fatherComponents.transform.localScale.y,
			fatherComponents.transform.localScale.z);
	}

	private void ImpulseInY (float force) {
		fatherComponents.myRigidbody2D.AddForce (Vector2.up * force, ForceMode2D.Impulse);
	}

	public void PlaySound (int firstIndice, int lastIndice) {
		int newIndice = Random.Range (firstIndice, lastIndice);

		AudioSource prefabAudioSource = Instantiate (soundPrefab, transform).GetComponent<AudioSource> ();
		prefabAudioSource.clip = WORLD_DATA_SOUNDS_EFFECTS.sounds [newIndice];
		prefabAudioSource.volume = WORLD_DATA_SOUNDS_EFFECTS.soundsVelume [newIndice];

		prefabAudioSource.Play ();
	}

	private void ChangeSwordEnable (int state) {
		if (state == 0) {
			sword.enabled = false;
		} else {
			sword.enabled = true;
		}
	}
}
