
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class LeveMmanager : MonoBehaviour {

	public void CarregarLevel(int indice) {
		SceneManager.LoadScene ("Level" + indice);
	}

	public void CarregarCena (string nomeDaCena) {
		SceneManager.LoadScene (nomeDaCena);
	}

	public void SairDoJogo () {
		Application.Quit ();
	}
}
