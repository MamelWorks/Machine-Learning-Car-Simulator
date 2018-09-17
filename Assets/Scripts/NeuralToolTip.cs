using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;

public class NeuralToolTip : MonoBehaviour
{

    public GameObject NeuralNetworkVisualiserPanel;
    public Text ValueText;
    public WeightInfo WeightInfo;
    public void SetValue(string value)
    {
        ValueText.text = value;
    }

    void Update()
    {
        if(!NeuralNetworkVisualiserPanel.activeSelf)
            gameObject.SetActive(false);
        transform.position = Input.mousePosition;
        SetValue(WeightInfo.Weight.ToString());
    }
}
