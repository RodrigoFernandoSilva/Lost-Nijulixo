using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFace : MonoBehaviour {

	private Transform target;

	public void ChangeTarget (Transform newTarget) {
		target = newTarget;
		GetComponent<Camera> ().orthographicSize = target.GetComponent<CameraFacePosition> ().cameraSize;
	}

	public void ChangeTargetAndEnable (Transform newTarget) {
		ChangeTarget (newTarget);
		GetComponent<CameraFace> ().enabled = true;
	}

	// Update is called once per frame
	void Update () {
		transform.position = target.position;
	}

}
