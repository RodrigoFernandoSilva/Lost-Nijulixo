using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FukaAtos : MonoBehaviour {

	public Animator fadeOut;
	public Transform anciaoPosition;
	public Transform anciaoSpaw;
	public GameObject anciao;
	private GameObject anciaoThatWasSpaw;
	public Transform badBullCameraPosition;
	public Transform bookSpaw;
	public GameObject book;
	private GameObject bookThatWasSpaw;
	public SpriteRenderer chest;
	public SpriteRenderer[] pocoes;
	public Sprite[] chests; 
	public TextAsset file;
	private DialogController dialogController;
	public Transform[] toriPositions;
	private int toriIndice = 0;
	public float toriVelocity;
	public Transform rainha;
	public Transform operatorDead;
	public Atos fuka;
	public Transform[] fukaPositions;
	private int fukaIndice = 0;
	public float fukaVelocity;
	private float fukaScaleX;
	private Tori tori;
	public int ato;

	private Vector3[] initialPosition = new Vector3[2];
	private float timeToWait;
	private float timerCount;

	// Use this for initialization
	void Awake () {
		for (int i = 0; i < pocoes.Length; i++) {
			pocoes [i].enabled = false;
		}

		tori = FindObjectOfType <Tori> ();
		dialogController = FindObjectOfType <DialogController> ();

		fukaScaleX = fuka.transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return)) {
			PressedEnter ();
		} else if (Input.GetKeyDown (KeyCode.Escape)) {
			PressedEscape ();
		}

		if (timeToWait > timerCount) {
			timerCount += Time.deltaTime;
			return;
		}

		switch (ato) {
		case 0:
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 2:
			dialogController.ChangeCanvasEnable (false);
			fuka.transform.localScale = new Vector3 (fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			fuka.GetComponent<Atos> ().StartActionWalk (-fukaVelocity, fukaPositions [fukaIndice]);
			fukaIndice++;
			timeToWait = 1f;
			timerCount = 0f;
			ato++;
			break;
		case 3:
			tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions [toriIndice]);
			toriIndice++;
			ato++;
			break;
		case 4:
			if (!fuka.GetComponent<Atos> ().isActing) {
				fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
				dialogController.ChangeCanvasEnable (true);
				dialogController.ShowLine (true);
				ato++;
			}
			break;
		case 6: //Fuka começá a fuças o baú
			fuka.transform.localScale = new Vector3 (fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			dialogController.ShowLine (true);
			fuka.GetComponent<Components> ().myAnimator.SetTrigger ("Chest");
			fuka.GetComponent<Components> ().myAnimator.SetBool ("ChestOver", false);
			ato++;
			break;
		case 8:
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 10:
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			fuka.GetComponent<Components> ().myAnimator.SetBool ("ChestOver", true);
			fuka.GetComponentInChildren<FukaAnimation> ().PlayChestOping ();
			fuka.GetComponent<Atos> ().StartActionWalk (-fukaVelocity, fukaPositions [fukaIndice]);
			fukaIndice++;
			chest.sprite = chests[1];
			ato++;
			break;
		case 12: //Fuka reclama da falta de segurança
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			fuka.GetComponent<Components> ().myAnimator.SetTrigger ("TakingInChast");
			fuka.GetComponent<Components> ().myAnimator.SetBool ("TakingInChastOver", false);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 14:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 16:
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 18:
			fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			fuka.GetComponent<Components> ().myAnimator.SetBool ("TakingInChastOver", true);
			pocoes [0].enabled = true;
			dialogController.ShowLine (true);
			ato++;
			break;
		case 20: //Fuka pede para Tori seguir ele
			fuka.transform.localScale = new Vector3 (fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			dialogController.ShowLine (true);
			fuka.GetComponent<Atos> ().StartActionWalk (-fukaVelocity, fukaPositions [fukaIndice]);
			fukaIndice++;
			timeToWait = 1;
			timerCount = 0;
			ato++;
			break;
		case 21:
			tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions [toriIndice]);
			toriIndice++;
			ato++;
			break;
		case 22:
			ato++;
			break;
		case 23: //A rainha da um bem vindo a Tori
			if (!fuka.GetComponent<Atos> ().isActing) {
				fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
				dialogController.ChangeTargetAndEnable (rainha);
				fuka.GetComponent<Components> ().myAnimator.SetTrigger ("PutPocao");
				StartCoroutine ("PutPocao");
				dialogController.ShowLine (true);
				ato++;
			}
			break;
		case 25:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 27:
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 29: //Tori fala q vai em bora
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 31:
			dialogController.ChangeTargetAndEnable (rainha);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 33:
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 35: //Fuka joga o livro
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			fuka.GetComponent<Components> ().myAnimator.SetTrigger ("ThrownBook");
			StartCoroutine ("ThrownBook");
			ato++;
			break;
		case 37:
			dialogController.ShowLine (true);
			ato++;
			break;
		case 39:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 41: //Mostra o redbull
			dialogController.ChangeCameraFollowEnable (false);
			dialogController.ChangeLerpPosition (badBullCameraPosition, 2f);
			dialogController.ChangeTargetAndEnable (rainha);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 43:
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 45:
			dialogController.ShowLine (true);
			ato++;
			break;
		case 47:
			dialogController.ShowLine (true);
			ato++;
			break;
		case 49:
			dialogController.ShowLine (true);
			ato++;
			break;
		case 51:
			dialogController.ChangeTargetAndEnable (operatorDead);
			dialogController.ChangeLerpPosition (operatorDead, 2);
			dialogController.ChangeCameraFollowEnable (false);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 53:
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 55:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 57:
			anciaoThatWasSpaw = Instantiate (anciao, anciaoSpaw.position, anciaoSpaw.rotation);
			dialogController.ChangeTargetAndEnable (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ChangeLerpPosition (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform, 2);
			dialogController.ChangeCameraFollowEnable (false);
			anciaoThatWasSpaw.GetComponent<Atos> ().StartActionWalk (-2f, anciaoPosition);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 59:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 61:
			dialogController.ChangeTargetAndEnable (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ChangeLerpPosition (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform, 2);
			dialogController.ChangeCameraFollowEnable (false);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 63:
			dialogController.ShowLine (true);
			ato++;
			break;
		case 65:
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ShowLine (true);
			ato++;
			break;
		case 67:
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeCanvasEnable (false);
			tori.myComponents.myRigidbody2D.velocity = Vector2.zero;
			Destroy (anciaoThatWasSpaw.GetComponent<Atos> ());
			Destroy (fuka.GetComponent<Atos> ());
			anciaoThatWasSpaw.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			anciaoThatWasSpaw.GetComponentInChildren<Animator> ().SetFloat ("Velocity", 0);
			fadeOut.SetTrigger ("fadeOut");
			GetComponent<FukaAtos> ().enabled = false;
			break;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.tag == "Tori" && !GetComponent<FukaAtos> ().enabled) {
			initialPosition [0] = tori.transform.position;
			initialPosition [1] = fuka.transform.position;

			ato = 0;
			timerCount = 0f;
			timeToWait = 2f;
			tori.DisableThisScript ();
			GetComponent<FukaAtos> ().enabled = true;
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions[toriIndice]);
			toriIndice++;
			tori.GetComponent<Atos> ().enabled = true;
			dialogController.ChangeMyFile (file);

			fuka.GetComponent<Atos> ().enabled = true;
			fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);

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

	private void NextButonPress () {
		if (timeToWait > timerCount) {
			return;
		}
		if (ato >= 21 && ato <= 23) {
			return;
		}
		ato++;
	}

	private void JumpButonPress () {
		tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
		fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);

		tori.transform.position = new Vector3 (toriPositions [toriPositions.Length - 1].position.x,
			toriPositions [toriPositions.Length - 1].position.y,
			toriPositions [toriPositions.Length - 1].position.z);

		fuka.transform.position = new Vector3 (fukaPositions [fukaPositions.Length - 1].position.x,
			toriPositions [fukaPositions.Length - 1].position.y,
			toriPositions [fukaPositions.Length - 1].position.z);

		if (bookThatWasSpaw == null) {
			bookThatWasSpaw = Instantiate (book, bookSpaw.position, bookSpaw.rotation);
		}

		if (!pocoes[1].enabled) {
			StopCoroutine ("PutPocao");
			pocoes [0].enabled = false;
			pocoes [1].enabled = true;
		}

		if (anciaoThatWasSpaw == null) {
			anciaoThatWasSpaw = Instantiate (anciao, anciaoSpaw.position, anciaoSpaw.rotation);
			anciaoThatWasSpaw.transform.position = new Vector3 (anciaoPosition.position.x, anciaoPosition.position.y, anciaoPosition.position.z);
		}

		ato = 67;
	}

	private void PressedEnter () {
		if (ato == 0) {
			timerCount = timeToWait + 1f;
		} else if (ato == 1) {
			tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions [toriIndice]);
			toriIndice++;
			fuka.GetComponent<Atos> ().StartActionWalk (-fukaVelocity, fukaPositions [fukaIndice]);
			fukaIndice++;
			tori.GetComponent<Atos> ().TeleporToEnd ();
			fuka.GetComponent<Atos> ().TeleporToEnd ();
			ato = 4;
		} else if (ato >= 2 && ato <= 4) {
				tori.GetComponent<Atos> ().TeleporToEnd ();
				fuka.GetComponent<Atos> ().TeleporToEnd ();
				ato = 4;
		} else if (ato >= 5 && ato <= 19 || ato >= 24 && ato <= 67) {
			ato++;
		} else if (ato >= 20 && ato <= 23) {
			if (ato == 21) {
				timerCount = timeToWait + 1;
				tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions [toriIndice]);
				toriIndice++;
			}

			tori.GetComponent<Atos> ().TeleporToEnd ();
			fuka.GetComponent<Atos> ().TeleporToEnd ();

			ato = 23;
		}
	}

	private void PressedEscape () {
		if (ato <= 2) {
			ato = 0;

			tori.transform.position = initialPosition [0];
			fuka.transform.position = initialPosition [1];

			timerCount = 0f;
			timeToWait = 2f;
			dialogController.ChangeCanvasEnable (false);

			toriIndice = 0;
			fukaIndice = 0;

			dialogController.ChangeLine (0);

			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			tori.GetComponent<Atos> ().StartActionWalk (-toriVelocity, toriPositions [toriIndice]);
			toriIndice++;
		} else if (ato >= 3 && ato <= 5) {
			tori.GetComponent<Atos> ().ChangeIsCating (false);
			fuka.GetComponent<Atos> ().ChangeIsCating (false);

			tori.transform.position = new Vector3 (toriPositions [0].position.x,
				toriPositions [0].position.y,
				toriPositions [0].position.z);
			fuka.transform.position = initialPosition [1];
			toriIndice = 1;
			fukaIndice = 0;

			dialogController.ChangeLine (0);
			dialogController.ShowLine (true);

			ato = 1;
		} else if (ato == 7) {
			fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			fuka.GetComponent<Components> ().myAnimator.SetBool ("ChestOver", true);

			dialogController.SubLine (2);
			dialogController.ShowLine (true);
			ato = 5;
		} else if (ato == 9) {
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);

			dialogController.SubLine (3);
			dialogController.ShowLine (true);
			ato = 6;

		} else if (ato == 11) {
			fuka.GetComponent<Components> ().myAnimator.SetTrigger ("Chest");
			fuka.GetComponent<Components> ().myAnimator.SetBool ("ChestOver", false);

			chest.sprite = chests [0];
			fuka.GetComponent<Atos> ().ChangeIsCating (false);

			fuka.transform.position = new Vector3 (fukaPositions [0].position.x,
				fukaPositions [0].position.y,
				fukaPositions [0].position.z);

			fukaIndice--;

			dialogController.SubLine (3);
			dialogController.ShowLine (true);
			ato = 8;
		} else if (ato == 13) {
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);
			fuka.GetComponent<Components> ().myAnimator.SetBool ("TakingInChastOver", true);
			ato = 11;
		} else if (ato == 15) {
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);
			ato = 13;
		} else if (ato == 17) {
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);
			ato = 15;
		} else if (ato == 19) {
			fuka.GetComponent<Components> ().myAnimator.SetTrigger ("TakingInChast");
			fuka.GetComponent<Components> ().myAnimator.SetBool ("TakingInChastOver", false);
			pocoes [0].enabled = false;
			dialogController.SubLine (2);
			dialogController.ShowLine (true);
			ato = 17;
		} else if (ato >= 21 && ato <= 24) {
			fuka.transform.localScale = new Vector3 (-fukaScaleX, fuka.transform.localScale.y, fuka.transform.localScale.z);
			fuka.GetComponent<Atos> ().ChangeIsCating (false);

			fukaIndice -= 2;
			fuka.transform.position = new Vector3 (fukaPositions [fukaIndice].position.x,
				fukaPositions [fukaIndice].position.y,
				fukaPositions [fukaIndice].position.z);
			fukaIndice++;

			if (ato >= 23) {
				tori.GetComponent<Atos> ().ChangeIsCating (false);

				toriIndice -= 2;
				tori.transform.position = new Vector3 (toriPositions [toriIndice].position.x,
					toriPositions [toriIndice].position.y,
					toriPositions [toriIndice].position.z);
				toriIndice++;
			}
			
			if (ato >= 24) {
				dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
				pocoes [0].enabled = true;
				pocoes [1].enabled = false;
				dialogController.SubLine (3);
				StopCoroutine ("PutPocao");
				ato = 20;
			} else {
				ato = 19;
				dialogController.SubLine (2);
			}

			dialogController.ShowLine (true);
		} else if (ato == 26) {
			dialogController.ChangeTargetAndEnable (rainha);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 24;
		} else if (ato == 28) {
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 26;
		} else if (ato == 30) {
			tori.transform.localScale = new Vector3 (-1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 28;
		} else if (ato == 32) {
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 30;
		} else if (ato == 34) {
			tori.transform.localScale = new Vector3 (1, tori.transform.localScale.y, tori.transform.localScale.z);
			dialogController.ChangeTargetAndEnable (rainha);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 32;
		} else if (ato == 36) {
			Destroy (bookThatWasSpaw);
			StopCoroutine ("ThrownBook");
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 34;
		} else if (ato == 38) {
			StopCoroutine ("ThrownBook");
			Destroy (bookThatWasSpaw);
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (3);
			dialogController.ShowLine (true);

			ato = 35;
		} else if (ato == 40) {
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 38;
		} else if (ato == 42) {
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 40;
		} else if (ato == 44) {
			dialogController.SubLine (3);
			dialogController.ShowLine (true);

			ato = 41;
		} else if (ato >= 44 && ato <= 50) {
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato -= 2;
		} else if (ato == 52) {
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeTargetAndEnable (rainha);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 50;
		} else if (ato == 54) {
			dialogController.SubLine (3);
			dialogController.ShowLine (true);

			ato = 51;
		} else if (ato == 56) {
			dialogController.ChangeTargetAndEnable (fuka.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 54;
		} else if (ato == 58) {
			Destroy(anciaoThatWasSpaw);
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 56;
		} else if (ato == 60) {
			Destroy(anciaoThatWasSpaw);
			anciaoThatWasSpaw.transform.position = new Vector3 (anciaoPosition.position.x,
				anciaoPosition.position.y,
				anciaoPosition.position.z);
			dialogController.SubLine (3);
			dialogController.ShowLine (true);

			ato = 57;
		} else if (ato == 62) {
			dialogController.ChangeCameraFollowEnable (true);
			dialogController.ChangeTargetAndEnable (tori.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 60;
		} else if (ato == 64) {
			dialogController.SubLine (2);
			dialogController.ShowLine (true);

			ato = 62;
		}  else if (ato == 66) {
			dialogController.ChangeTargetAndEnable (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform);
			dialogController.ChangeLerpPosition (anciaoThatWasSpaw.GetComponentInChildren<CameraFacePosition> ().transform, 2);
			dialogController.ChangeCameraFollowEnable (false);
			dialogController.SubLine (3);
			dialogController.ShowLine (true);

			ato = 64;
		}
	}

	IEnumerator ThrownBook () {
		yield return new WaitForSeconds (0.8f);
		bookThatWasSpaw = Instantiate (book, bookSpaw.position, bookSpaw.rotation);
	}


	IEnumerator PutPocao () {
		yield return new WaitForSeconds (0.9f);
		pocoes [0].enabled = false;
		pocoes [1].enabled = true;
	}
}
