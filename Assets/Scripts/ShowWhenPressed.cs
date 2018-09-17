using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowWhenPressed : MonoBehaviour, IPointerDownHandler
{
    public GameObject PanelToShow;
    public bool HideButNotInactive = false;
    private bool startStateOfActive;

    void Start()
    {
        startStateOfActive = PanelToShow.activeSelf;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (HideButNotInactive && PanelToShow.transform.localPosition.y >-500)
        {
            PanelToShow.transform.localPosition = new Vector3(0, -1000, 0);
            startStateOfActive = !startStateOfActive;
            return;
        }
        if (HideButNotInactive && PanelToShow.transform.localPosition.y < -500)
        {
            PanelToShow.transform.localPosition = new Vector3(0, 0, 0);
            startStateOfActive = !startStateOfActive;
            return;
        }
        PanelToShow.SetActive(!PanelToShow.activeSelf);

    }

}
