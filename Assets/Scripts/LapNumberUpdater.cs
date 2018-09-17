using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapNumberUpdater : MonoBehaviour
{
    public LearningManager manager;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    gameObject.GetComponent<Text>().text = "Lap number: " + manager.MostLapCount;
	}
}
