using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSystem : MonoBehaviour {

	public bool isOnFloor;
	public int kindOfFloor;

	void OnTriggerStay2D(Collider2D col) {
		if (col.tag != "Fruits" && col.tag != "CutsceneTrigger") {
			isOnFloor = true;

			switch (col.tag) {
			case "Florest":
				kindOfFloor = 1;
				break;
			case "Land":
				kindOfFloor = 2;
				break;
			case "Stone":
				kindOfFloor = 3;
				break;
			default:
				kindOfFloor = 0;
				break;
			}
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.tag != "Fruits" &&  col.tag != "CutsceneTrigger") {
			isOnFloor = false;
		}
	}
}
