using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour {

	private ColeteAllThings myColeteAllThings;

	public int fruitsTaked = 0;
	public int maxFruits;

	void Start () {
		myColeteAllThings = GetComponent<ColeteAllThings> ();
		maxFruits = GameObject.FindGameObjectsWithTag ("Fruits").Length;
	}

	// Update is called once per frame
	void Update () {
		if (fruitsTaked >= maxFruits) {
			myColeteAllThings.TakedAllFruits ();
			Destroy (GetComponent<Fruits> ());
		}
	}
}
