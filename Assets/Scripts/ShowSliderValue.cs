using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowSliderValue : MonoBehaviour {

    // Use this for initialization
    private Slider Slider;
    public string Unit;

    void Start()
    {
        Slider = transform.parent.gameObject.GetComponent<Slider>();
    }
	// Update is called once per frame
	void Update ()
	{
	    gameObject.GetComponent<Text>().text = Slider.value + " " + Unit;
	}
}
