using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedOptionsToggleScript : MonoBehaviour
{
    public GameObject PanelToSwitchTo;
    public bool InitialStateOfToggle;

    private GameObject _panelToSwitchFrom;
    private Toggle _activePanelToggle;
    void Start()
    {
        _activePanelToggle = GetComponent<Toggle>();
        _panelToSwitchFrom = transform.parent.gameObject;
    }
	public void ValueChanged ()
	{
	    if (_activePanelToggle.isOn == InitialStateOfToggle)
	        return;
        PanelToSwitchTo.SetActive(true);
	    _activePanelToggle.isOn = InitialStateOfToggle;
        _panelToSwitchFrom.SetActive(false);
	}
}
