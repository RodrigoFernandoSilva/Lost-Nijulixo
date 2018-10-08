using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tori : MonoBehaviour {

	private GameObject fruit;

	public Components myComponents;
	public float walkingVelocity = 2f;
	public float runningVelocity = 2f;
	private float runningVeloPass = 1;
	public float jumpForce = 8;

	private float horizontalForward;

	private bool camTakeFruit = false;
	private bool takingFruit = false;

	void Awake () {
		myComponents = GetComponent<Components> ();
	}

	void OnDisable () {
		horizontalForward = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Z) && camTakeFruit && myComponents.canJump) {
			myComponents.myRigidbody2D.velocity = Vector2.zero;
			myComponents.myAnimator.SetTrigger ("TakeFruit");
			takingFruit = true;
		}

		Move ();
		FLip ();

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
	}

	void FixedUpdate () {
		if (myComponents.jump) {
			myComponents.myRigidbody2D.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
			myComponents.jump = false;
		}

		if (!myComponents.jumpDelay && !takingFruit) {
			myComponents.myRigidbody2D.velocity = new Vector2 (walkingVelocity * runningVeloPass * horizontalForward, myComponents.myRigidbody2D.velocity.y);
		}
	}

	private void  Move () {
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
		if (Input.GetKeyDown (KeyCode.UpArrow) && myComponents.canJump && !takingFruit) {
			StartCoroutine ("TimerBeforeJumpDelay");
			myComponents.myAnimator.SetTrigger ("Jump");
			myComponents.canJump = false;
		}
	}

	/// <summary>
	/// Flip tori with his width, (localScale.x).
	/// </summary>
	private void FLip () {
		if (transform.localScale.x > 0 && Input.GetKey (KeyCode.LeftArrow)) {
			transform.localScale = new Vector3 (-1, transform.localScale.y, transform.localScale.z);
		}
		if (transform.localScale.x < 0 && Input.GetKey (KeyCode.RightArrow)) {
			transform.localScale = new Vector3 (1, transform.localScale.y, transform.localScale.z);
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

	IEnumerator TimerTakingFruit () {
		yield return new WaitForSeconds (0.6f);
		takingFruit = false;
	}


	public void DisableThisScript () {
		myComponents.myAnimator.SetFloat ("Velocity", 0f);
		GetComponent<Tori> ().enabled = false;
	}

	public void TakeFruit () {
		StartCoroutine ("TimerTakingFruit");
		FindObjectOfType <Fruits> ().fruitsTaked++;
		Destroy (fruit);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Fruits") {
			camTakeFruit = true;

			fruit = col.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag == "Fruits") {
			camTakeFruit = false;
		}
	}
}

