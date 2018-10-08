using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorChefLimiter : MonoBehaviour {

	public TextAsset file;
	public Transform toriLimitatorPosition;
	public float toriVelocity;
	public Transform myCameraFacePosition;
	private DialogController dialogController;
	public Transform mainCameraPositions;
	public float mainCameraLerp;
	private Tori tori;
	private int ato;

	void Awake () {
		dialogController = FindObjectOfType<DialogController> ();
		tori = FindObjectOfType<Tori> ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			ato = 2;
		}

		switch (ato) {
		case 0:
			if (dialogController.NearTheTarget (1)) {
				ato++;
			}
			break;
		case 1:
			for (int i = 0; i < 2; i++) {
				dialogController.GetNextButton (i).onClick.RemoveAllListeners ();
				dialogController.GetBackButton (i).onClick.RemoveAllListeners ();
				dialogController.GetJumpButton (i).onClick.RemoveAllListeners ();

				dialogController.GetNextButton (i).onClick.AddListener (NextButonPress);
				dialogController.GetJumpButton (i).onClick.AddListener (NextButonPress);
			}

			dialogController.ChangeTargetAndEnable (myCameraFacePosition);
			dialogController.ChangeMyFile (file);
			dialogController.ChangeLine (1);
			dialogController.ChangeCanvasEnable (true);
			dialogController.ShowLine (true);
			break;
		case 2:
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeCanvasEnable (false);
			tori.myComponents.myRigidbody2D.velocity = Vector2.zero;
			tori.enabled = true;
			GetComponent<OperatorChefLimiter> ().enabled = false;
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Tori") {
			ato = 0;
			dialogController.ChangeCameraFollowEnable (false);
			dialogController.ChangeLerpPosition (mainCameraPositions, mainCameraLerp);
			tori.DisableThisScript ();
			GetComponent<OperatorChefLimiter> ().enabled = true;
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			tori.GetComponent<Atos> ().StartActionWalk (toriVelocity, toriLimitatorPosition);
			tori.GetComponent<Atos> ().enabled = true;
		}
	}

	private void NextButonPress () {
		ato++;
	}
}
