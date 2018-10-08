using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperatorChefAto : MonoBehaviour {

	public TextAsset file;
	private DialogController dialogController;
	private Transform myCameraFacePosition;
	public Transform[] mainCameraPositions;
	public float[] mainCameraLerp;
	private int indiceMainCameraLerp = 0;

	private Atos myAto;

	void Awake () {
		myCameraFacePosition = GetComponentInChildren<CameraFacePosition> ().transform;

		dialogController = FindObjectOfType<DialogController> ();
		myAto = GetComponent<Atos> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if (myAto.ato < 3) {
				myAto.ato = 3;
				dialogController.SetCameraCutsceneaSize (10);
				dialogController.SetCameraCutsceneaAnimator (false, false);
				dialogController.SetCameraCutscenePosition (mainCameraPositions [indiceMainCameraLerp].position);
			} else {
				myAto.ato = 4;
			}
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			PressedEscape ();
		}

		switch (myAto.ato) {
		case 1:
			dialogController.ChangeLerpPosition (mainCameraPositions[indiceMainCameraLerp], mainCameraLerp[indiceMainCameraLerp]);
			myAto.ato++;
			break;
		case 2:
			if (dialogController.NearTheTarget (3)) {
				myAto.ato++;
			}
			break;
		case 3:
			for (int i = 0; i < 2; i++) {
				dialogController.GetNextButton (i).onClick.RemoveAllListeners ();
				dialogController.GetBackButton (i).onClick.RemoveAllListeners ();
				dialogController.GetJumpButton (i).onClick.RemoveAllListeners ();

				dialogController.GetNextButton (i).onClick.AddListener (NextButonPress);
				dialogController.GetBackButton (i).onClick.AddListener (PressedEscape);
				dialogController.GetJumpButton (i).onClick.AddListener (JumpButonPress);
			}

			dialogController.ChangeTargetAndEnable (myCameraFacePosition);
			dialogController.ChangeMyFile (file);
			dialogController.ChangeCanvasEnable (true);
			dialogController.ShowLine (true);
			break;
		case 4:
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeCanvasEnable (false);
			FindObjectOfType<Tori> ().enabled = true;
			Destroy (GetComponent<OperatorChefAto> ());
			break;
		}
	}

	private void NextButonPress () {
		myAto.ato++;
	}

	private void JumpButonPress () {
		myAto.ato++;
	}

	private void PressedEscape () {
		myAto.ato = 0;
		dialogController.SetCameraCutsceneaAnimator (true, true);
		dialogController.ChangeCanvasEnable (false);
	}
}
