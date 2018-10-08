using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class DialogController : MonoBehaviour {

	private CameraCutscene cameraCutscene;
	private CameraFace cameraFace;
	public Button[] nextButton;
	public Button[] backButton;
	public Button[] jumpButton;
	public RawImage altoFalante;
	public Canvas myCanvas;
	public Canvas dialog;
	public Text d_TextName;
	public Text d_TextMessage;
	public Canvas Interfone;
	public Text i_TextName;
	public Text i_TextMessage;

	public int line = 0;

	public string[] fileText;

	void Awake () {
		cameraCutscene = FindObjectOfType<CameraCutscene> ();
		cameraFace = FindObjectOfType<CameraFace> ();
		ChangeCanvasEnable (false);
	}

	public void ChangeMyFile (TextAsset file) {
		fileText = (file.text.Split ('\n'));
		
		line = 0;
	}

	/// <summary>
	/// Show the dialog txt line.
	/// </summary>
	public void ShowLine (bool itsDialog) {
		myCanvas.enabled = true;

		string[] nameAndText;

		nameAndText = fileText[line].Split ("||".ToCharArray ());

		if (itsDialog) {
			dialog.enabled = true;
			Interfone.enabled = false;

			d_TextName.text = nameAndText[0] + ":";
			d_TextMessage.text = nameAndText[nameAndText.Length - 1];
		} else {
			dialog.enabled = false;
			Interfone.enabled = true;

			i_TextName.text = nameAndText[0] + ":";
			i_TextMessage.text = nameAndText[nameAndText.Length - 1];
		}

		line++;
	}

	public void SubLine (int value) {
		line -= value;
	}

	public void ChangeLine (int newLine) {
		line = newLine;
	}

	public void ChangeCanvasEnable (bool state) {
		myCanvas.enabled = state;
	}

	public bool CanvasEnable () {
		return myCanvas.enabled;
	}

	public Button GetNextButton (int indice) {
		return nextButton[indice];
	}

	public Button GetBackButton (int indice) {
		return backButton[indice];
	}

	public Button GetJumpButton (int indice) {
		return jumpButton[indice];
	}

	public void SetCameraCutsceneaSize (float newSize) {
		cameraCutscene.GetComponent<Camera> ().orthographicSize = newSize;
	}

	public void SetCameraCutsceneaAnimator (bool state, bool backButton) {
		cameraCutscene.GetComponent<Animator> ().enabled = state;

		if (backButton) {
			cameraCutscene.GetComponent<Animator> ().SetTrigger ("BackButton");
		}
	}

	public void SetCameraCutscenePosition (Vector3 newPosition) {
		cameraCutscene.transform.position = newPosition;
	}

	//Those are the <cameraFace> methods, they have the same name
	public void ChangeTargetAndEnable (Transform newTarget) {
		cameraFace.ChangeTargetAndEnable (newTarget);
	}

	//Those are the <CameraCutscene> methods, they have the same name
	public void ChangeLerpPosition (Transform newLerpPosition, float newTimeLerp) {
		cameraCutscene.ChangeLerpPosition (newLerpPosition, newTimeLerp);
	}
	public bool NearTheTarget (float distance) {
		return cameraCutscene.NearTheTarget (distance);
	}
	public void ChangeCameraFollowEnable (bool state) {
		cameraCutscene.ChangeCameraFollowEnable (state);
	}
}
