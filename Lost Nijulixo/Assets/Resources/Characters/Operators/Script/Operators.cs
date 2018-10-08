using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operators : MonoBehaviour {

	public int kindAnimation = 0;

	// Use this for initialization
	void Start () {
		GetComponentInChildren<Animator> ().SetInteger ("kindAnimation", kindAnimation);
	}
}
