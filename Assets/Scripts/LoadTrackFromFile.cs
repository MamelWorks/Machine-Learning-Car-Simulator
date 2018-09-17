using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Crosstales.FB;
using UnityEngine;

public class LoadTrackFromFile : MonoBehaviour
{

    public CurveImplementation CurveImplementation;
    public LearningManager LearningManager;
    public void LoadTrack()
    {
        string path = FileBrowser.OpenSingleFile("","","track");
        if (string.IsNullOrEmpty(path))
            return;
        var trackFile = File.ReadAllLines(path);
        var vectorStringList = new List<string>(trackFile);
        List<Vector3> listofVector3S = new List<Vector3>();
        foreach (var vectorString in vectorStringList)
        {
            Debug.Log(vectorString);
            try
            {
                listofVector3S.Add(JsonUtility.FromJson<Vector3>(vectorString));
            }
            catch (Exception e)
            {
                Debug.Log("Error in " + vectorString + " | " + e.Message);
            }
        }

        CurveImplementation.CreateTrackFromFile(listofVector3S);
        CurveImplementation.GenerateMesh();
        LearningManager.InitCars();
        LearningManager.NextGen();
    }
}
