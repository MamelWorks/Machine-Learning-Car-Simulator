using UnityEngine;
using System.Collections;
using System.IO;
using Crosstales.FB;
using Crosstales.UI.Util;
using UnityEngine.EventSystems;

public class ScreenshotNow : MonoBehaviour, IPointerDownHandler
{
    public RectTransform rectT; // Assign the UI element which you wanna capture
    int width; // width of the object to capture
    int height; // height of the object to capture

    // Use this for initialization
    void Start()
    {
        width = System.Convert.ToInt32(rectT.rect.width);
        height = System.Convert.ToInt32(rectT.rect.height);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(takeScreenShot()); // screenshot of a particular UI Element.

    }

    public IEnumerator takeScreenShot()
    {
        yield return new WaitForEndOfFrame(); // it must be a coroutine 

        Vector2 temp = rectT.transform.position;

        var startX = temp.x - width / 2;
        var startY = temp.y - height / 2;

        var tex = new Texture2D(width, height - 50, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(startX, startY, width, height), 0, 0);
        tex.Apply();

        // Encode texture into PNG
        var bytes = tex.EncodeToPNG();
        Destroy(tex);
        string path = FileBrowser.SaveFile("", "", "", "png");
        if (path != "")
            File.WriteAllBytes(path, bytes);

    }
}