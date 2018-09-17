using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderColorChanger : MonoBehaviour
{

    private Slider _slider;
    private Image _image;
	void Start ()
	{
	    _slider = gameObject.GetComponent<Slider>();
	    _image = transform.GetChild(0).GetComponent<Image>();
    }
	
	// Update is called once per frame
	void Update ()
	{

	    var x = (_slider.value / 10);
        Color myColor = new Color(2.0f * x, 2.0f * (1 - x), 0);
	    _image.color = myColor;
	}
}
