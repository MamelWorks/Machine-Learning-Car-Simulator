using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Crosstales.FB;
using UnityEngine;

public class SaveTrackToFile : MonoBehaviour
{
    public ControlPointsRandomizer ControlPointsRandomizer;
    public void SavePointsToFile()
    {
        string stringToSave = "";
        foreach (var point in ControlPointsRandomizer.ControlPointsList)
        {
            stringToSave += JsonUtility.ToJson(point.transform.position) + "\r\n";
        }
        string path = FileBrowser.SaveFile("", "", "", "track");
        if (path != "")
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(stringToSave);
                fs.Write(info, 0, info.Length);

                // writing data in bytes already
                byte[] data = new byte[] { 0x0 };
                fs.Write(data, 0, data.Length);

            }
        }
    }
}
