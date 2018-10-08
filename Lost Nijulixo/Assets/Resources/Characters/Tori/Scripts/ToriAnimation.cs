using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToriAnimation : MonoBehaviour {

	private Tori fatherTori;

	void Awake () {
		fatherTori = GetComponentInParent<Tori> ();
	}

	private void TakeFruit () {
		fatherTori.TakeFruit ();
	}
}
