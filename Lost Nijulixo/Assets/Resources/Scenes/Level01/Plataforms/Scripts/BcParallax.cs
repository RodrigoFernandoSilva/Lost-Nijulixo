using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BcParallax : MonoBehaviour {

	private Transform tori;
	private Vector2 distanceInitial;

	public float parallax = 2;

	// Use this for initialization
	void Start () {
		tori = GameObject.FindGameObjectWithTag ("Tori").transform;

		distanceInitial.x = tori.position.x - transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector2 ((distanceInitial.x + tori.position.x) * parallax, transform.position.y);
	}
}
