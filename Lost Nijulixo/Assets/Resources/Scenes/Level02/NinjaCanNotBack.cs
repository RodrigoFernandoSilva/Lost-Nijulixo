using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCanNotBack : MonoBehaviour {

	public TextAsset file;
	public Transform toriLimitatorPosition;
	public float toriVelocity;
	public Transform myCameraFacePosition;
	private DialogController dialogController;
	private ToriNinja toriNinja;
	public int messageLine;
	private int ato;

	// Use this for initialization
	void Awake () {
		dialogController = FindObjectOfType<DialogController> ();
		toriNinja = FindObjectOfType<ToriNinja> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			NextButonPress ();
		}

		switch (ato) {
		case 0:
			for (int i = 0; i < 2; i++) {
				dialogController.GetNextButton (i).onClick.RemoveAllListeners ();
				dialogController.GetBackButton (i).onClick.RemoveAllListeners ();
				dialogController.GetJumpButton (i).onClick.RemoveAllListeners ();

				dialogController.GetNextButton (i).onClick.AddListener (NextButonPress);
				dialogController.GetJumpButton (i).onClick.AddListener (NextButonPress);
			}

			dialogController.ChangeTargetAndEnable (myCameraFacePosition);
			dialogController.ChangeMyFile (file);
			dialogController.ChangeLine (messageLine);
			dialogController.ChangeCanvasEnable (true);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 2:
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeCanvasEnable (false);
			toriNinja.myComponents.myRigidbody2D.velocity = Vector2.zero;
			toriNinja.enabled = true;
			GetComponent<NinjaCanNotBack> ().enabled = false;
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Tori") {
			ato = 0;
			toriNinja.DisableThisScript ();
			GetComponent<NinjaCanNotBack> ().enabled = true;
			toriNinja.transform.localScale = new Vector3 (1, toriNinja.transform.localScale.y, toriNinja.transform.localScale.z);
			toriNinja.GetComponent<Atos> ().StartActionWalk (toriVelocity, toriLimitatorPosition);
			toriNinja.GetComponent<Atos> ().enabled = true;

			for (int i = 0; i < 2; i++) {
				dialogController.GetNextButton (i).onClick.RemoveAllListeners ();
				dialogController.GetBackButton (i).onClick.RemoveAllListeners ();
				dialogController.GetJumpButton (i).onClick.RemoveAllListeners ();

				dialogController.GetNextButton (i).onClick.AddListener (NextButonPress);
				dialogController.GetJumpButton (i).onClick.AddListener (NextButonPress);
			}
		}
	}

	private void NextButonPress () {
		ato = 2;
	}
}