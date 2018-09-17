using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Crosstales.FB;
using Crosstales.FB.Wrapper;
using UnityEngine;
using UnityEngine.EventSystems;

public class SaveToFile : MonoBehaviour, IPointerDownHandler
{
    public LearningManager LearningManager;
    public void OnPointerDown(PointerEventData eventData)
    {
        string path = FileBrowser.SaveFile("", "", "","log");
        if (path != "")
        {
            using (FileStream fs = File.Create(path))
            {
                string dataasstring = "";
                foreach (var log in LearningManager.Logs)
                {
                    dataasstring += log + "\r\n";
                }
                byte[] info = new UTF8Encoding(true).GetBytes(dataasstring);
                fs.Write(info, 0, info.Length);

                // writing data in bytes already
                byte[] data = new byte[] { 0x0 };
                fs.Write(data, 0, data.Length);
                
            }
        }
    }
}
