using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraController : MonoBehaviour {

	public Transform player;

	// Update is called once per frame
	void Update () {
		if (player.position.x >= 0f) {
			transform.parent = player;
			Destroy (this);
		}
	}
}
