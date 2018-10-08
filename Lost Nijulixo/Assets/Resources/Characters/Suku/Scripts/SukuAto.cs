using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SukuAto : MonoBehaviour {

	public Texture altoFalante;
	public TextAsset file;
	private DialogController dialogController;
	public Renderer[] fruits;
	public Transform[] toriPositions;
	private int toriIndice = 0;
	public float toriVelocity;
	public Atos suku;
	public Transform[] sukuPositions;
	private int sukuIndice = 0;
	public float sukuVelocity;
	private Tori tori;
	public int ato;

	private float timeToWait = 0;
	private float timerCount = 0;

	private Vector3[] initialPosition = new Vector3[2];

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < fruits.Length; i++) {
			fruits [i].enabled = false;
		}

		tori = FindObjectOfType <Tori> ();
		dialogController = FindObjectOfType <DialogController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			PressedEnter ();
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			PressedEscape ();
		}

		if (timeToWait > timerCount) {
			switch (ato) {
			case 0:
				ato++;
				break;
			case 1:
				if (!tori.GetComponent<Atos> ().isActing) {
					tori.myComponents.myAnimator.SetTrigger ("TakeFruit");
					if (!tori.GetComponent<Atos> ().isActing) {
						for (int i = 0; i < fruits.Length; i++) {
							fruits [i].enabled = true;
						}

						timeToWait = 0;
						timerCount = 1.2f;
						ato++;
					}
				}
				break;
			case 2: //Wait timer over
				tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ChangeCanvasEnable (true);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 4:
				tori.transform.localScale = new Vector3 (-1f, tori.transform.localScale .y, tori.transform.localScale.z);
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 6: //Tori: Veio chato.
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 8:
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 10:
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 12: //Suku: Sei q vc quer ir conhecer a terras proibidas
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 14:
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 16:
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 18:
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 20: //Alto-falante chata Tori
				dialogController.altoFalante.texture = altoFalante;
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (false);
				ato++;
				break;
			case 22:
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 24:
				dialogController.ChangeTargetAndEnable (suku.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 26:
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 28:
				dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
				dialogController.ShowLine (true);
				ato++;
				break;
			case 30:
				dialogController.ChangeCameraFollowEnable (true);
				dialogController.ChangeCanvasEnable (false);
				tori.myComponents.myRigidbody2D.velocity = Vector2.zero;
				tori.enabled = true;
				Destroy(GetComponent<SukuAto> ());
				break;
			}
		} else{
			timeToWait += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Tori") {
			initialPosition [0] = tori.transform.position;
			initialPosition [1] = suku.transform.position;

			ato = 0;
			tori.DisableThisScript ();
			GetComponent<SukuAto> ().enabled = true;
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			tori.GetComponent<Atos> ().StartActionWalk (toriVelocity, toriPositions[toriIndice]);
			suku.StartActionWalk (sukuVelocity, sukuPositions[sukuIndice]);
			suku.GetComponent<Atos> ().enabled = true;
			tori.GetComponent<Atos> ().enabled = true;
			dialogController.ChangeMyFile (file);
			for (int i = 0; i < 2; i++) {
				dialogController.GetNextButton (i).onClick.RemoveAllListeners ();
				dialogController.GetBackButton (i).onClick.RemoveAllListeners ();
				dialogController.GetJumpButton (i).onClick.RemoveAllListeners ();

				dialogController.GetNextButton (i).onClick.AddListener (NextButonPress);
				dialogController.GetBackButton (i).onClick.AddListener (PressedEscape);
				dialogController.GetJumpButton (i).onClick.AddListener (JumpButonPress);
			}
		}
	}

	private void JumpButonPress () {
		if (ato == 0 || ato == 1) {
			tori.GetComponent<Atos> ().TeleporToEnd ();
			suku.GetComponent<Atos> ().TeleporToEnd ();
			timeToWait = timerCount;
			for (int i = 0; i < fruits.Length; i++) {
				fruits [i].enabled = true;
			}
		}

		ato = 30;
	}

	private void NextButonPress () {
		ato++;
	}

	private void PressedEnter () {
		if (ato == 0 || ato == 1) {
			
			tori.GetComponent<Atos> ().TeleporToEnd ();
			suku.GetComponent<Atos> ().TeleporToEnd ();
			timeToWait = timerCount;
			for (int i = 0; i < fruits.Length; i++) {
				fruits [i].enabled = true;
			}
			ato = 2;

		} else if (ato >= 3 && ato <= 29) {
			ato++;
		}
	}

	private void PressedEscape () {
		if (ato >= 0 && ato <= 4) {

			dialogController.ChangeCanvasEnable (false);
			tori.transform.position = initialPosition[0];
			suku.transform.position = initialPosition[1];
			timeToWait = timerCount;
			for (int i = 0; i < fruits.Length; i++) {
				fruits [i].enabled = false;
			}
			ato = 0;
		} else if (ato >= 5 && ato <= 29) {
			dialogController.SubLine (2);
			ato -= 3;
		}
	}
}
