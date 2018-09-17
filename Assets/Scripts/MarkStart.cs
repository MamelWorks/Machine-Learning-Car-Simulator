using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkStart : MonoBehaviour
{

    public Material Material;
	void Start ()
	{
	    GetComponent<MeshRenderer>().enabled = true;
	    GetComponent<MeshRenderer>().material = Material;
        transform.Rotate(180,0,0);
	}
}
