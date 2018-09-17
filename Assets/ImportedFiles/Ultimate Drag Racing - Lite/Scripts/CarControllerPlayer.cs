using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerPlayer : MonoBehaviour {

	[HideInInspector]
	public SoundController soundController;


	public GameController gameController;

	private CarInfo car;
	protected float currentSpeed = 0;

	[HideInInspector]
	public Tachometer tachometer;
	[HideInInspector]
	public Speedometer speedometer;
	[HideInInspector]
	public Nitrometer nitrometer;

	[HideInInspector]
	public CarPhysics carPhysics;

	[HideInInspector]
	public Gearbox gearbox;

	protected bool isNitro;
	protected bool isFinished;

	// Use this for initialization
	void Awake () {
		car = GetComponent<CarInfo> ();
		tachometer = GetComponent<Tachometer> ();
		speedometer = GetComponent<Speedometer> ();
		nitrometer = GetComponent<Nitrometer> ();
		carPhysics = GetComponent<CarPhysics> ();
		gearbox = GetComponent<Gearbox> ();
		soundController = GetComponentInChildren<SoundController> ();
		soundController.StartEnginePlay ();
	}
		
	// Update is called once per frame
	void Update () {
		soundController.ChangeEnginePitch (gearbox.currentTransfer);
		transform.position += Vector3.right * currentSpeed * Time.deltaTime * 0.25f;
		if (currentSpeed > 0) {
			if (currentSpeed > gearbox.maxTransferSpeed) {
				currentSpeed -= 0.035f * (6f * 3.5f - gearbox.currentTransfer * 3.5f);
			} else {
				currentSpeed -= 0.025f;
			}
		} else {
			currentSpeed = 0f;
		}
		tachometer.CalculateTacho(gearbox.maxTransferSpeed, currentSpeed);
		tachometer.ShowArrowAngle (gearbox.maxTransferSpeed, currentSpeed);
		speedometer.ShowArrowAngle (currentSpeed);

		if (nitrometer.CurrentNitro > 0f && isNitro) {
			nitrometer.CalculateNitro ();
		} else {
			isNitro = false;
			soundController.NitroStop ();
		}
		nitrometer.ShowArrowAngle ();

		//check finish
		if (isFinished) {
			soundController.SlippageStop ();
			if (currentSpeed > 0) {
				currentSpeed -= car.Braking / 100f;
				carPhysics.RotateBraking ();
			} else if (currentSpeed == 0) {
				carPhysics.Rotate (0f);
				soundController.BrakingStop ();
			}
			return;
		}
			
		if (Input.GetKey (KeyCode.LeftShift)) {
			isNitro = true;
			soundController.NitroPlay ();
		}

		//acceleration control
		if (isNitro && currentSpeed < gearbox.maxTransferSpeed) {
			if (currentSpeed < car.MaxSpeed) {
				currentSpeed += ( ( ( (car.Power / car.Mass * 10f) * car.NitroPower) + (car.Acceleration / 200f) )  / (gearbox.currentTransfer * car.GearSpeedRetention) );
			}
			carPhysics.RotateNitro();
		} else {
			if (currentSpeed < car.MaxSpeed) {
				currentSpeed += ( ( (car.Power / car.Mass * 10f) + (car.Acceleration / 200f) )  / (gearbox.currentTransfer * car.GearSpeedRetention) );
			}

			if (!isNitro && currentSpeed < gearbox.maxTransferSpeed - 5f) {
				carPhysics.RotateDriving ();
			} else if(!isNitro && currentSpeed >= gearbox.maxTransferSpeed - 5f){
				carPhysics.Rotate (0f);
			}
		}

		//gearbox control
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			soundController.GearPlay ();
			gearbox.NextTransfer ();
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			soundController.GearPlay ();
			gearbox.PreviousTransfer ();
		}
	}

	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Flag") {
			isFinished = true;
			soundController.StopEnginePlay ();
			soundController.BrakingPlay ();
			gameController.ShowGameOverPanel ();
			carPhysics.RotateBraking ();
		}
	}

	public void EnableNitro(){
		if (!isFinished) {
			isNitro = true;
			soundController.NitroPlay ();
			carPhysics.RotateNitro ();
		}
	}

	public void NextGear(){
		if (!isFinished) {
			soundController.GearPlay ();
			gearbox.NextTransfer ();
		}
	}

	public void PreviousGear(){
		if (!isFinished) {
			soundController.GearPlay ();
			gearbox.PreviousTransfer ();
		}
	}

	public float GetCurrentSpeed(){
		return currentSpeed;
	}
}
