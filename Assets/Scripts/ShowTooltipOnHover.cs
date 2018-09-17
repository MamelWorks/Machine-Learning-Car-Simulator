using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowTooltipOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public NeuralToolTip TooltipPanel;
    void Awake()
    {
        var fooGroup = Resources.FindObjectsOfTypeAll<NeuralToolTip>();
        if (fooGroup.Length > 0)
        {
            TooltipPanel = fooGroup[0];
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipPanel.transform.position = eventData.position;
        TooltipPanel.WeightInfo = gameObject.GetComponent<WeightInfo>();
        TooltipPanel.gameObject.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipPanel.gameObject.SetActive(false);
    }

   
}
