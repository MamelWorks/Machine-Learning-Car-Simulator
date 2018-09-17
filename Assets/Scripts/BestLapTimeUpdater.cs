using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestLapTimeUpdater : MonoBehaviour {

    public LearningManager manager;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = "Lap completed in: " + MamelsHelpMethods.FloatToTime(manager.BestLapTime, "0:00.00"); 

    }
}
