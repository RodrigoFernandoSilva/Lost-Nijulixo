using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColeteAllThings : MonoBehaviour {

	public TextAsset file;
	public Transform toriLimitatorPosition;
	public float toriVelocity;
	public Transform myCameraFacePosition;
	private DialogController dialogController;
	private Tori tori;
	public int messageLine;
	private int ato;

	// Use this for initialization
	void Awake () {
		dialogController = FindObjectOfType<DialogController> ();
		tori = FindObjectOfType<Tori> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			ato = 2;
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
			tori.myComponents.myRigidbody2D.velocity = Vector2.zero;
			tori.enabled = true;
			GetComponent<ColeteAllThings> ().enabled = false;

			if (messageLine == 3) {
				Destroy (gameObject);
			}
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.transform.tag == "Tori") {
			ato = 0;
			tori.DisableThisScript ();
			GetComponent<ColeteAllThings> ().enabled = true;
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			tori.GetComponent<Atos> ().StartActionWalk (toriVelocity, toriLimitatorPosition);
			tori.GetComponent<Atos> ().enabled = true;
		}
	}

	public void TakedAllFruits () {
		ato = 0;
		messageLine = 3;
		tori.DisableThisScript ();
		GetComponent<ColeteAllThings> ().enabled = true;
	}

	private void NextButonPress () {
		ato = 2;
	}
}