using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PleaseDontMove : MonoBehaviour
{
    private Vector2 _savedPositionVector2;
	void Awake ()
	{
	    _savedPositionVector2 = GetComponent<RectTransform>().anchoredPosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    GetComponent<RectTransform>().anchoredPosition = _savedPositionVector2;

	}
}
