using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenerateButton : MonoBehaviour
{

    public Toggle GenerateNewNetwork;

    public LearningManager Manager;
	// Use this for initialization
	void Start () {
		GetComponent<Button>().onClick.AddListener(OnClick);
	}
	

    void OnClick()
    {
        if (GenerateNewNetwork.isOn)
            Manager.InitCars();
        Manager.NextGen();
        

    }

}
