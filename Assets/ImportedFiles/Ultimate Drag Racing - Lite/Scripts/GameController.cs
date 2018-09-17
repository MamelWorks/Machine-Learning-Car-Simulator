using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject gameOverPanel;
	public Text resultText;
	public int currentScene;

	public void ShowGameOverPanel(){
		gameOverPanel.SetActive (true);
		resultText.text = "FINISH";
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene (0);
		}
	}

	public void Restart(){
		SceneManager.LoadScene (currentScene);
	}
}
