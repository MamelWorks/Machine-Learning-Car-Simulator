using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNetworkButton : MonoBehaviour
{

    public GameObject NeuralNetworkPanel;

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }


    void OnClick()
    {
        NeuralNetworkPanel.SetActive(!NeuralNetworkPanel.activeSelf);
    }

    void Update()
    {
        transform.GetChild(0).GetComponent<Text>().text = NeuralNetworkPanel.activeSelf ? "Hide Neural Network" : "Show Neural Network";
    }
}
