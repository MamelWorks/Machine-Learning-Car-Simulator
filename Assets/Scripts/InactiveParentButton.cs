using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InactiveParentButton : MonoBehaviour, IPointerDownHandler
{

    public GameObject toSetInactive;
    public bool anotherFormOfInactivity = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (toSetInactive != null)
        {
            if (anotherFormOfInactivity)
                toSetInactive.transform.position = new Vector3(toSetInactive.transform.position.x, -1000, 0);
            else
                toSetInactive.SetActive(false);
        }
        else
        {
            if (anotherFormOfInactivity)
                transform.parent.position = new Vector3(transform.parent.position.x, -1000, 0);
            else
                transform.parent.gameObject.SetActive(false);

        }
    }
}
