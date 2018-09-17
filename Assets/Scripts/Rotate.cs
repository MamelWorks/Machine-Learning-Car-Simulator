using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float Speed;
	void Update () {
		transform.Rotate(new Vector3(0,1,0));
	}
}
