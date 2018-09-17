using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ControlPointsRandomizer : MonoBehaviour
{

    // Use this for initialization
    public float radius = 10f;
    public float randX = 10f;
    public float randY = 10f;
    public int numberOfControlPoints = 8;
    public List<GameObject> ControlPointsList;
    public GameObject PrefabTransform;
    public Slider NumberOfKnotsSlider;
    public Slider RangeKnotPositionSlider;
    public Slider SizeSlider;

    public void GenerateControlPoints()
    {
        numberOfControlPoints = Int32.Parse(NumberOfKnotsSlider.value.ToString());
        radius = SizeSlider.value;
        randX = RangeKnotPositionSlider.value;
        randY = RangeKnotPositionSlider.value;
        DestroyControlPoints();
        ControlPointsList = new List<GameObject>();
        for (int i = 0; i < numberOfControlPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / numberOfControlPoints;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * (radius + (i % 2 == 0 ? -1 : 1) * Random.Range(0, randX)), 5, Mathf.Sin(angle) * (radius + (i % 2 == 0 ? -1 : 1) * Random.Range(0, randY)));
            GameObject go = Instantiate(PrefabTransform, newPos, Quaternion.identity);
            ControlPointsList.Add(go);
        }
    }

    public void GenerateControlPoints(List<Vector3> vector3List)
    {
        DestroyControlPoints();
        ControlPointsList = new List<GameObject>();
        foreach (var vector3 in vector3List)
        {
            GameObject go = Instantiate(PrefabTransform, vector3, Quaternion.identity);
            ControlPointsList.Add(go);
        }
    }
    public void DestroyControlPoints()
    {
        foreach (var o in ControlPointsList)
        {
            Destroy(o);
        }
    }

}
