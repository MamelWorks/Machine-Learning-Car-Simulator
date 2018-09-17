using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioSource gearSound;
	public AudioSource engineSound;
	public AudioSource nitroSound;
	public AudioSource slippageSound;
	public AudioSource brakingSound;

	public void GearPlay(){
		gearSound.Play ();
	}

	public void StartEnginePlay(){
		engineSound.Play ();
	}

	public void StopEnginePlay(){
		engineSound.Stop ();
	}

	public void ChangeEnginePitch(int gear){
		switch (gear) {
		case 1:
			engineSound.pitch = 0.3f;
			break;
		case 2:
			engineSound.pitch = 0.4f;
			break;
		case 3:
			engineSound.pitch = 0.5f;
			break;
		case 4:
			engineSound.pitch = 0.6f;
			break;
		case 5:
			engineSound.pitch = 0.8f;
			break;
		case 6:
			engineSound.pitch = 1f;
			break;
		}
	}

	public void NitroPlay(){
		nitroSound.Play ();
	}

	public void NitroStop(){
		nitroSound.Stop ();
	}

	public void SlippagePlay(){
		slippageSound.Play ();
	}
	public void SlippageStop(){
		slippageSound.Stop ();
	}

	public void FlipStop(){
		slippageSound.Stop ();
	}

	public void BrakingPlay(){
		brakingSound.Play ();
	}

	public void BrakingStop(){
		brakingSound.Stop ();
	}
}
