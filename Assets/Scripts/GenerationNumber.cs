using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerationNumber : MonoBehaviour
{

    public LearningManager LearningManager;
	void Update ()
	{
	    GetComponent<Text>().text = "Generation " + LearningManager.GenerationNumber;
	}
}
