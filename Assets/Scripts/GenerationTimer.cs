using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerationTimer : MonoBehaviour
{

    public LearningManager Manager;

	void Update ()
	{
	    GetComponent<Text>().text = "Time: " + MamelsHelpMethods.FloatToTime(Manager.GenerationTime, "0:00.00");
	}
}
