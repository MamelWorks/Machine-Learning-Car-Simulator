using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour {

	public CarControllerPlayer carController;
	public Text countdownText;

	private int count = 3;

	private float time = 4f;

	// Use this for initialization
	void Start () {
		carController.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		time -= Time.deltaTime;
		if (time <= count) {
			count--;
			countdownText.text = count.ToString ();
			if (count == 0) {
				countdownText.gameObject.SetActive (false);
				carController.enabled = true;
			}
		}
	}
}
