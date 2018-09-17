using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tachometer : MonoBehaviour {

	public Transform arrow;
	public float CurrentTacho = 0f;

	static float minAngle = 117f;
	static float maxAngle = -119f;

	public CarControllerPlayer car;

	void Start(){
		car = GetComponent<CarControllerPlayer> ();
	}

	public void CalculateTacho(float maxTransferSpeed, float currentSpeed){
		/*FORMULA
		// maxTransferSpeed == 6
		// currentSpeed     == x
		// 
		// (maxTransferSpeed * x) == (currentSpeed * 6)
		// x == (currentSpeed * 6) / maxTransferSpeed
		*/
		//float tacho = (currentSpeed * 6) / maxTransferSpeed;
	}

	public float GetArrowAngle(float maxTransferSpeed, float currentSpeed)
	{
		float ang = Mathf.Lerp (minAngle, maxAngle, Mathf.InverseLerp (0, maxTransferSpeed, currentSpeed));
		return (ang = (ang > 180) ? ang - 360 : ang);
	}

	public float GetMaxAngle(){
		return maxAngle;
	}

	public void ShowArrowAngle(float maxTransferSpeed, float currentSpeed)
	{
		if (arrow != null) {
			float ang = Mathf.Lerp (minAngle, maxAngle, Mathf.InverseLerp (0, maxTransferSpeed, currentSpeed));
			if (currentSpeed < maxTransferSpeed) {
				arrow.eulerAngles = new Vector3 (0, 0, ang);
			} else {
				/* sharp arrow effect */
				arrow.eulerAngles = new Vector3 (0, 0, ang - Random.Range(-1f, 2f));
			}

			/* check slippage*/
			ang = (ang > 180) ? ang - 360 : ang;
			if (ang >= maxAngle + 40f && car != null) {
				car.soundController.SlippagePlay ();
			} 
		}
	}
}
