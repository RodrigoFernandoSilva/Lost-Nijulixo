using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Components : MonoBehaviour {

	public Rigidbody2D myRigidbody2D;
	public Animator myAnimator;
	public JumpSystem myJumpSystem;

	public bool jump = false;
	public bool canJump = true;
	public bool isFalling = false;
	public bool jumpDelay = false;

	public float yPosition = -10f;

	// Use this for initialization
	void Awake () {
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponentInChildren<Animator> ();
		myJumpSystem = GetComponentInChildren<JumpSystem> ();
	}
}
