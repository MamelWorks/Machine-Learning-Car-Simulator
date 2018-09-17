using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotator : MonoBehaviour {

	public Transform[] wheels;

	private CarControllerPlayer car;

	// Use this for initialization
	void Start () {
		car = GetComponent<CarControllerPlayer> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Transform wheel in wheels) {
			if (car != null) {
				wheel.Rotate (Vector3.right * car.GetCurrentSpeed () * 0.3f);
			}
		}
	}

}
