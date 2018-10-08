using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Ogro : MonoBehaviour {

	public AudioSource myAudioSource;
	public Animator fadeOut;
	public Slider vida;
	private Animator myAnimator;
	private Rigidbody2D myRigidbody2D;
	private Transform ninja;

	private bool atk = false;
	private bool atking = false;
	private bool walking = false;

	public Vector2 timeAtk;
	public Vector2 atkVelocidade;
	public Vector2 distanceTelepor;

	private Vector2 counter;
	private float counterGenerated;
	public Vector2 timerTelepor;

	private float larguraInicial;

	// Use this for initialization
	void Awake () {
		myAudioSource = GetComponent <AudioSource> ();
		myRigidbody2D = GetComponent <Rigidbody2D> ();
		myAnimator = GetComponentInChildren<Animator> ();
		ninja = FindObjectOfType<ToriNinja> ().transform;

		larguraInicial = transform.localScale.x;

		counterGenerated = Random.Range (timerTelepor.x, timerTelepor.y);
	}
	
	// Update is called once per frame
	void Update () {
		if (vida.value <= 0) {
			fadeOut.SetTrigger ("fadeOut");
			Destroy (gameObject);
		}

		if (!atk) {
			atk = true;
			StartCoroutine ("Atk");
		}

		counterGenerated -= Time.deltaTime;
		if (counterGenerated <= 0) {
			if (Random.Range (0f, 100f) <= 25f) {
				Teleport ();
			}

			counterGenerated = Random.Range (timerTelepor.x, timerTelepor.y);
		}

		Flip ();
	}

	private void Teleport () {
		transform.position = new Vector3 (Random.Range (distanceTelepor.x, distanceTelepor.y) ,transform.position.y, transform.position.z);
	}

	private void Flip () {
		AnimatorStateInfo animatorState = myAnimator.GetCurrentAnimatorStateInfo (0);
		if (!animatorState.IsTag ("Idle")) {
			return;
		}

		if (!walking) { 
			//Faz o inimigo olhar sempre em direção ao jogador
			if (transform.localScale.x > 0 && transform.position.x > ninja.position.x) {
				transform.localScale = new Vector3 (-larguraInicial, transform.localScale.y, transform.localScale.z);
			} else if (transform.localScale.x < 0 && transform.position.x < ninja.position.x) {
				transform.localScale = new Vector3 (larguraInicial, transform.localScale.y, transform.localScale.z);
			}
		} else {
			//Faz o inimigo apontar para a direção em q ele estiver se movendo
			if (transform.localScale.x > 0 && myRigidbody2D.velocity.x < 0) {
				transform.localScale = new Vector3 (-larguraInicial, transform.localScale.y, transform.localScale.z);
			} else if (transform.localScale.x < 0 && myRigidbody2D.velocity.x > 0) {
				transform.localScale = new Vector3 (larguraInicial, transform.localScale.y, transform.localScale.z);
			}
		}
	}

	IEnumerator Atk () {
		float time = Random.Range (timeAtk.x, timeAtk.y);
		yield return new WaitForSeconds (time);
		myAnimator.SetInteger ("indiceAtk", Random.Range(0, 4));
		myAnimator.SetFloat ("atkVelocidade", Random.Range (atkVelocidade.x, atkVelocidade.y));
		myAnimator.SetTrigger ("atk");

		atk = false;

		atking = true;
		StartCoroutine ("Atking");
	} 

	IEnumerator Atking () {
		yield return new WaitForSeconds (2);

		if (Random.Range (0f, 100f) <= 500f) {
			if (transform.localScale.x > 0 && transform.position.x > ninja.position.x) {
				transform.localScale = new Vector3 (-larguraInicial, transform.localScale.y, transform.localScale.z);
			} else if (transform.localScale.x < 0 && transform.position.x < ninja.position.x) {
				transform.localScale = new Vector3 (larguraInicial, transform.localScale.y, transform.localScale.z);
			}
		}

		yield return new WaitForSeconds (8);
		atking = false;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "swordNinja") {
			myAudioSource.Play ();
			vida.value -= 10f;

			if (Random.Range (0f, 100f) <= 5f) {
				Teleport ();
			}
		}
	}
}
