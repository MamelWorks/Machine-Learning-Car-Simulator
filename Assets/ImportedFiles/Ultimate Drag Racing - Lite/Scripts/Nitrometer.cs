using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitrometer : MonoBehaviour {


	public Transform arrow;
	public float FullNitro = 4f;

	[HideInInspector]
	public float CurrentNitro = 0f;

	static float minAngle = -87f;
	static float maxAngle = 82.5f;

	// Use this for initialization
	void Start () {
		CurrentNitro = FullNitro;
	}

	public void CalculateNitro(){
		CurrentNitro -= Time.deltaTime;
	}

	public void ShowArrowAngle()
	{
		if (arrow != null) {
			float ang = Mathf.Lerp(maxAngle, minAngle, Mathf.InverseLerp(0, FullNitro, CurrentNitro));
			arrow.eulerAngles = new Vector3 (0, 0, ang);
		}

	}
}
