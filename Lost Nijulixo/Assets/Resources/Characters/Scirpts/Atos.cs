using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atos : MonoBehaviour {

	private Components myComponents;
	public Transform finalPosition;
	private bool isOnLeftPosiion;

	public bool isActing = false;

	private float velocity;

	public int ato = 0;

	void Awake () {
		myComponents = GetComponent<Components> ();
	}

	/*
	 * 0: Walk;
	 */
	public int kindOfAto;

	void Update () {
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
			StartCoroutine ("timerOverJumpDelay");
			myComponents.myAnimator.SetTrigger ("TouchFloor");
			myComponents.isFalling = false;
			myComponents.canJump = true;
		}
	}

	void FixedUpdate () {
		if (isActing && !myComponents.jumpDelay) {
			if (kindOfAto == 0) {
				myComponents.myRigidbody2D.velocity = new Vector2 (velocity, myComponents.myRigidbody2D.velocity.y);

				if (isOnLeftPosiion) {
					if (transform.position.x < finalPosition.position.x) {
						isActing = false;
						myComponents.myRigidbody2D.velocity = new Vector2 (0f, myComponents.myRigidbody2D.velocity.y);
					}
				} else if (transform.position.x > finalPosition.position.x) {
					isActing = false;
					myComponents.myRigidbody2D.velocity = new Vector2 (0f, myComponents.myRigidbody2D.velocity.y);
				}
			}
		}
	}

	public void ChangeIsCating (bool state) {
		isActing = state;

		myComponents.myRigidbody2D.velocity = Vector2.zero;
	}

	public void TeleporToEnd () {
		transform.position = finalPosition.position;
	}

	public void StartActionWalk (float newVelocity, Transform newFinalPosition) {
		kindOfAto = 0;
		isActing = true;
		velocity = newVelocity;
		finalPosition = newFinalPosition;

		if (transform.position.x > finalPosition.position.x) {
			isOnLeftPosiion = true;
		} else {
			isOnLeftPosiion = false;
		}
	}

	/// <summary>
	/// Time to Tori come back to walk.
	/// </summary>
	/// <returns>The over jump delay.</returns>
	IEnumerator timerOverJumpDelay () {
		myComponents.jumpDelay = true;
		yield return new WaitForSeconds (0.5f);
		myComponents.jumpDelay = false;
	}

	/// <summary>
	/// Time to Tori come back to walk.
	/// </summary>
	/// <returns>The over jump delay.</returns>
	IEnumerator timerBeforeJumpDelay () {
		if (myComponents.myAnimator.GetBool("Running")) {
			myComponents.myRigidbody2D.velocity /= 1.5f;
		}
		myComponents.jumpDelay = true;
		yield return new WaitForSeconds (0.5f);
		myComponents.jumpDelay = false;
	}
}
