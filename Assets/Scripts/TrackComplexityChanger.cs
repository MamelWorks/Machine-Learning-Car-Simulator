using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrackComplexityChanger : MonoBehaviour
{

    public Slider WaypointResolutionSlider;
    public Slider SizeSlider;
    public Slider NumberOfKnotsSlider;
    public Slider RangeKnotPositionSlider;

    private Slider _complexitySlider;

    void Start()
    {
        _complexitySlider = gameObject.GetComponent<Slider>();
        UpdateSliders();
    }
    public void UpdateSliders()
    {
        var x = _complexitySlider.value/10;
        SizeSlider.value = Mathf.Lerp(40f, 100f, x);
        NumberOfKnotsSlider.value = Mathf.Lerp(3f, 15f, x);
        RangeKnotPositionSlider.value = Mathf.Lerp(20f, 50f, x);
        WaypointResolutionSlider.value = Mathf.Lerp(5f, 10f, x);
    }
}
