using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public Vector2 smoothSleep;
	private Vector2 position;
	public Vector2 centerDistance;

	public float smooth = 0.5f;

	// Update is called once per frame
	void Update () {
		if (target.localScale.x > 0) {
			position.x = Mathf.SmoothDamp (position.x, target.position.x + centerDistance.x, ref smoothSleep.x, smooth);
		} else {
			position.x = Mathf.SmoothDamp (position.x, target.position.x - centerDistance.x, ref smoothSleep.x, smooth);
		}

		position.y = Mathf.SmoothDamp (position.y, target.position.y + centerDistance.y, ref smoothSleep.y, smooth);

		transform.position = new Vector3 (position.x, position.y, transform.position.z);
	}

	public void ChangeCameraFollowEnable (bool state) {
		if (state) {
			position.Set (transform.position.x, transform.position.y);
		}

		GetComponent <CameraFollow> ().enabled = state;
	}
}
