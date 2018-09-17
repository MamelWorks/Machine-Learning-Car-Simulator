using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{

    public Color Color;
	void Start () {
	    GetComponent<Renderer>().material.SetColor("_Color", Color);
    }
	

}
