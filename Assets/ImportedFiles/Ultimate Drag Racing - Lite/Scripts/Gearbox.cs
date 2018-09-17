using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gearbox : MonoBehaviour {

	public Text transferText;

	[HideInInspector]
	public int currentTransfer = 1;

	[HideInInspector]
	public float maxTransferSpeed;

	const int MAX_TRANSFER = 6;

	void Start(){
		CalculateTransferValues ();
	}

	public void NextTransfer(){
		if (currentTransfer < MAX_TRANSFER) {
			currentTransfer++;
		}
		CalculateTransferValues ();
		if (transferText != null) {
			transferText.text = currentTransfer.ToString ();
		}
	}

	public void PreviousTransfer()
	{
		if (currentTransfer > 1) {
			currentTransfer--;
		}
		CalculateTransferValues ();
		if (transferText != null) {
			transferText.text = currentTransfer.ToString ();
		}
	}

	private void CalculateTransferValues()
	{
		switch (currentTransfer) {
		case 1:
			maxTransferSpeed = 30f;
			break;
		case 2:
			maxTransferSpeed = 60f;
			break;
		case 3:
			maxTransferSpeed = 90f;
			break;
		case 4:
			maxTransferSpeed = 120f;
			break;
		case 5:
			maxTransferSpeed = 160;
			break;
		case 6:
			maxTransferSpeed = 200;
			break;
		}
	}

}
