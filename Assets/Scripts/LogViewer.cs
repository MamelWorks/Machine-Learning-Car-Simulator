using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogViewer : MonoBehaviour
{

    // Use this for initialization
    public LearningManager LearningManager;
    public Text TextPrefab;
    public Toggle AutoScrollToggle;

    void Update()
    {
        CheckForChanges();
    }

    private void CheckForChanges()
    {
        if (LearningManager.Logs.Count == transform.childCount)
            return;
        if (LearningManager.Logs.Count > transform.childCount)
            UpdateLogs();
        else if (LearningManager.Logs.Count < transform.childCount)
            ResetLogs();
        if (AutoScrollToggle.isOn && transform.parent.parent.GetComponent<ScrollRect>().verticalScrollbar.IsActive())
            transform.parent.parent.GetComponent<ScrollRect>().velocity = new Vector2(0f, 100f);
    }

    private void ResetLogs()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void UpdateLogs()
    {
        for (int i = transform.childCount; i < LearningManager.Logs.Count; i++)
        {
            var text = Instantiate(TextPrefab);
            text.transform.SetParent(transform);
            text.text = LearningManager.Logs[i];
            text.supportRichText = true;
        }
    }
}
