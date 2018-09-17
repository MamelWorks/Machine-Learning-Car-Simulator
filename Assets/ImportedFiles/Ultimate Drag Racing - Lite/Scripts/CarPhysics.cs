using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour {

	public Transform RotatingPart;

	public float DrivingRotation;
	public float NitroRotation;
	public float BrakingRotation;
	public float Damping = 1f;

	public Quaternion desiredRotation;
	private float elapsed = 0f;
	private float currentAngle;

	public void Rotate(float angle){
		if (currentAngle != angle) {
			elapsed = 0f;
		}
		desiredRotation = Quaternion.Euler(new Vector3 (RotatingPart.rotation.x, RotatingPart.rotation.y, RotatingPart.rotation.z + angle));
		currentAngle = angle;
	}

	public void RotateDriving(){
		float angle = DrivingRotation;
		if (currentAngle != angle) {
			elapsed = 0f;
		}
		desiredRotation = Quaternion.Euler(new Vector3 (RotatingPart.rotation.x, RotatingPart.rotation.y, RotatingPart.rotation.z + angle));
		currentAngle = angle;
	}

	public void RotateNitro(){
		float angle = NitroRotation;
		if (currentAngle != angle) {
			elapsed = 0f;
		}
		desiredRotation = Quaternion.Euler(new Vector3 (RotatingPart.rotation.x, RotatingPart.rotation.y, RotatingPart.rotation.z + angle));
		currentAngle = angle;
	}

	public void RotateBraking(){
		float angle = BrakingRotation;
		if (currentAngle != angle) {
			elapsed = 0f;
		}
		desiredRotation = Quaternion.Euler(new Vector3 (RotatingPart.rotation.x, RotatingPart.rotation.y, RotatingPart.rotation.z + angle));
		currentAngle = angle;
	}

	void Update(){
		if (desiredRotation.z != 0) {
			elapsed += Time.deltaTime * Damping;
			RotatingPart.rotation = Quaternion.Lerp(RotatingPart.rotation, desiredRotation, elapsed);
		}
	}
}
