using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToriNinja : MonoBehaviour {

	private SoundEffects mySoundEffects;
	public float teleportDistance;
	public Components myComponents;
	public float walkingVelocity = 2f;
	public float runningVelocity = 2f;
	private float runningVeloPass = 1;
	public float jumpForce = 8;

	private float horizontalForward;

	public float maxTimerAtk;
	private float timerAtk;

	void Awake () {
		mySoundEffects = FindObjectOfType <SoundEffects> ();
		timerAtk = maxTimerAtk + 1;
		myComponents = GetComponent<Components> ();
	}

	void OnDisable () {
		horizontalForward = 0;
	}

	// Update is called once per frame
	void Update () {
		Move ();
		Attack ();
		TryFLip ();

		myComponents.myAnimator.SetFloat ("Velocity", Mathf.Abs (myComponents.myRigidbody2D.velocity.x));

		float yPositionOld = myComponents.yPosition;
		myComponents.yPosition = transform.position.y;

		//Change the animation to fall when Tori start to falling.
		if (yPositionOld > myComponents.yPosition && !myComponents.isFalling && !myComponents.myJumpSystem.isOnFloor) {
			myComponents.myAnimator.SetTrigger ("Fall");
			myComponents.isFalling = true;
			myComponents.canJump = false;
		}

		//Change the animation to "jump recover" afther Tori collided with floor.
		if (myComponents.isFalling && myComponents.myJumpSystem.isOnFloor) {
			myComponents.myRigidbody2D.velocity /= 2.5f;
			StartCoroutine ("TimerOverJumpDelay");
			myComponents.myAnimator.SetTrigger ("TouchFloor");
			myComponents.isFalling = false;
			myComponents.canJump = true;
		}

		myComponents.myAnimator.SetBool ("isOnFloor", myComponents.myJumpSystem.isOnFloor);
	}

	void FixedUpdate () {
		AnimatorStateInfo temp_State = myComponents.myAnimator.GetCurrentAnimatorStateInfo (1);

		if (temp_State.IsTag ("attacking") || timerAtk <= maxTimerAtk + 0.5f) {
			return;
		}

		if (myComponents.jump) {
			myComponents.myRigidbody2D.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
			myComponents.jump = false;
		}

		if (!myComponents.jumpDelay) {
			myComponents.myRigidbody2D.velocity = new Vector2 (walkingVelocity * runningVeloPass * horizontalForward, myComponents.myRigidbody2D.velocity.y);
		}
	}

	private void Attack () {
		if (myComponents.jumpDelay) {
			return;
		}

		//Vê qual vai ser o tipo de ataque combo
		int indiceAtk = 0;
		if (Input.GetKey (KeyCode.UpArrow)) {
			indiceAtk = 1;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			if (transform.localScale.x > 0) {
				indiceAtk = 2;
			} else {
				indiceAtk = 4;
			}
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			if (transform.localScale.x < 0) {
				indiceAtk = 2;
			} else {
				indiceAtk = 4;
			}
		}  else if (Input.GetKey (KeyCode.DownArrow)) {
			indiceAtk = 3;
		}

		if (Input.GetKeyDown (KeyCode.X)) {
			mySoundEffects.PlaySound (14, 15);
			
			if (transform.localScale.x > 0) {
				transform.position = new Vector2 (transform.position.x + teleportDistance, transform.position.y);
				transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
			} else {
				transform.position = new Vector2 (transform.position.x - teleportDistance, transform.position.y);
				transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
			}
		}

		if (Input.GetKeyDown (KeyCode.Z)) {
			timerAtk = 0;
			myComponents.myAnimator.SetBool ("attacking", true);
			myComponents.jump = false;
		}

		timerAtk += Time.deltaTime;
		if (timerAtk > maxTimerAtk && myComponents.myAnimator.GetBool ("attacking")) {
			myComponents.myAnimator.SetBool ("attacking", false);
		}
		
		myComponents.myAnimator.SetInteger ("indiceAtk", indiceAtk);
	}

	private void  Move () {
		AnimatorStateInfo temp_State = myComponents.myAnimator.GetCurrentAnimatorStateInfo (1);

		if (temp_State.IsTag ("attacking") || timerAtk <= maxTimerAtk + 0.5f) {
			return;
		}

		horizontalForward = Input.GetAxis ("Horizontal");

		//Running system
		if (Input.GetKey (KeyCode.LeftShift)) {
			myComponents.myAnimator.SetBool ("Running", true);
			runningVeloPass = Mathf.Lerp (runningVeloPass, runningVelocity, 2f * Time.deltaTime);
		} else {
			myComponents.myAnimator.SetBool ("Running", false);
			runningVeloPass = Mathf.Lerp (runningVeloPass, 1f, 2f * Time.deltaTime);
		}

		//Jump system
		if (Input.GetKeyDown (KeyCode.UpArrow) && myComponents.canJump) {
			StartCoroutine ("TimerBeforeJumpDelay");
			myComponents.myAnimator.SetTrigger ("Jump");
			myComponents.canJump = false;
		}
	}

	/// <summary>
	/// Flip tori with his width, (localScale.x).
	/// </summary>
	private void TryFLip () {
		AnimatorStateInfo tmp_State = myComponents.myAnimator.GetCurrentAnimatorStateInfo (1);
		if (tmp_State.IsTag ("Vazio") && timerAtk > maxTimerAtk) {
			if (transform.localScale.x > 0 && Input.GetKey (KeyCode.LeftArrow)) {
				transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
			}
			if (transform.localScale.x < 0 && Input.GetKey (KeyCode.RightArrow)) {
				transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
			}
		}
	}


	/// <summary>
	/// Time to Tori come back to walk.
	/// </summary>
	/// <returns>The over jump delay.</returns>
	IEnumerator TimerOverJumpDelay () {
		myComponents.jumpDelay = true;
		yield return new WaitForSeconds (0.5f);
		myComponents.jumpDelay = false;
	}

	/// <summary>
	/// Time to Tori come back to walk.
	/// </summary>
	/// <returns>The over jump delay.</returns>
	IEnumerator TimerBeforeJumpDelay () {
		if (runningVeloPass > 1.3f) {
			myComponents.myRigidbody2D.velocity /= 1.5f;
		}
		myComponents.jumpDelay = true;
		yield return new WaitForSeconds (0.5f);
		myComponents.jumpDelay = false;
	}

	public void DisableThisScript () {
		myComponents.myAnimator.SetFloat ("Velocity", 0f);
		GetComponent<ToriNinja> ().enabled = false;
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "swordOgro") {
			FindObjectOfType<LeveMmanager> ().CarregarCena ("GameOver");
		}
	}
}

