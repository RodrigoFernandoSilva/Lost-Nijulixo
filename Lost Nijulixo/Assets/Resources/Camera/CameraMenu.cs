using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenu : MonoBehaviour {

	public Transform[] leftRight;
	public float minDistance;
	public Transform target;
	public Vector2 smoothSleep;
	private Vector2 position;
	public float centerY;

	private bool nearFromWall;
	private int indice;

	public float smooth = 0.5f;

	// Update is called once per frame
	void Update () {
		position.x = Mathf.SmoothDamp (position.x, target.position.x, ref smoothSleep.x, smooth);
		position.y = Mathf.SmoothDamp (position.y, target.position.y + centerY, ref smoothSleep.y, smooth);

		NearFromWall ();
		if (nearFromWall) {
			if (indice == 1) {
				transform.position = new Vector3 (leftRight[1].position.x - minDistance, position.y, transform.position.z);

				if (Mathf.Abs (leftRight[1].position.x - position.x) < minDistance) {
					nearFromWall = false;
				}
			} else {
				transform.position = new Vector3 (leftRight[0].position.x + minDistance, position.y, transform.position.z);

				if (Mathf.Abs (leftRight[0].position.x - position.x) < minDistance) {
					nearFromWall = false;
				}
			}
		} else {
			transform.position = new Vector3 (position.x, position.y, transform.position.z);
		}
	}

	private void NearFromWall () {
		for (int i = 0; i < leftRight.Length; i++) {
			if (Mathf.Abs (leftRight[i].position.x - position.x) < minDistance) {
				indice = i;
				nearFromWall = true;
				return;
			}
		}
	}

	public void ChangeCameraFollowEnable (bool state) {
		if (state) {
			position.Set (transform.position.x, transform.position.y);
		}

		GetComponent <CameraFollow> ().enabled = state;
	}
}
