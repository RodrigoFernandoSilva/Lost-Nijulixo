using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCutscene : MonoBehaviour {

	public Atos[] actorsAtos;
	public Transform lerpPosition;
	public float timeLerp = 0;

	void Update () {
		transform.position = Vector3.Slerp (transform.position, lerpPosition.position, timeLerp * Time.deltaTime);
	}

	public void DisableAnimator () {
		GetComponent<Animator> ().enabled = false;
	}

	public void AddActorAto (int actorIndice) {
		actorsAtos[actorIndice].ato++;
	}

	public void ChangeLerpPosition (Transform newLerpPosition, float newTimeLerp) {
		GetComponent<CameraCutscene> ().enabled = true;
		lerpPosition = newLerpPosition;
		timeLerp = newTimeLerp;
	}

	public bool NearTheTarget (float distance) {
		return Vector3.Distance(transform.position, lerpPosition.position) < distance;
	}

	public void ChangeCameraFollowEnable (bool state) {
		GetComponent<CameraFollow> ().ChangeCameraFollowEnable (state);
		if (state) {
			GetComponent<CameraCutscene> ().enabled = false;
		}
	}
}
