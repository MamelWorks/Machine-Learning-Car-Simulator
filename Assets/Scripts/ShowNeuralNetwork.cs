using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI.Extensions;
using Image = UnityEngine.UI.Image;

public class ShowNeuralNetwork : MonoBehaviour
{

    // Use this for initialization
    public GameObject PrefabPanel;
    public CarSelector CarSelector;
    public UILineRenderer LineRenderer;

    private GameObject _selectedCarGameObject;
    private GameObject[][] _neuronPanels;
    private List<UILineRenderer> _lineRenderers;
    private Dictionary<GameObject, int[]> _test;

    // Update is called once per frame
    void Update()
    {
        if (CarSelector.SelectedCarGameObject == null)
        {
            gameObject.SetActive(false);
            _test = null;
            return;
        }

        var selectedCar = CarSelector.SelectedCarGameObject.GetComponent<Car>();
        var selectedNeurons = selectedCar.Net.Neurons;

        if (_test != null)
        {
            foreach (var f in _test)
            {
                float weightValue = selectedNeurons[f.Value[0]][f.Value[1]];
                var tempColor = weightValue > 0 ?
                    new Color(1, (weightValue -1) * -1, (weightValue - 1) * -1, 1) :
                    new Color((weightValue * -1 - 1) * -1, (weightValue * -1 - 1) * -1, 1, 1);

                f.Key.GetComponent<Image>().color = tempColor;
                f.Key.GetComponent<WeightInfo>().Weight = weightValue;
            }
        }

        if (CarSelector.SelectedCarGameObject == _selectedCarGameObject)
            return;

        _selectedCarGameObject = CarSelector.SelectedCarGameObject;



        if (_neuronPanels != null)
            foreach (var neuronPanel in _neuronPanels)
                foreach (var o in neuronPanel)
                    Destroy(o);

        _neuronPanels = new GameObject[selectedNeurons.Length][];
        _test = new Dictionary<GameObject, int[]>();

        RectTransform thisPanel = GetComponent<RectTransform>();

        thisPanel.sizeDelta = new Vector2(selectedNeurons.Length  * 60 -20 , (selectedNeurons[1].Length + 1)  * 15 + 90);

        for (int i = 0; i < selectedNeurons.Length; i++)
        {
            _neuronPanels[i] = new GameObject[selectedNeurons[i].Length];
            for (int j = 0; j < selectedNeurons[i].Length; j++)
            {
                var instantiatedPanel = Instantiate(PrefabPanel);
                _neuronPanels[i][j] = instantiatedPanel;
                instantiatedPanel.transform.SetParent(transform);
                instantiatedPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(60 * i + 20, -15 * (j + 1) - 80, 0);
                _test.Add(instantiatedPanel, new int[] { i, j });
            }
        }

        if (_lineRenderers != null)
            foreach (var uiLineRenderer in _lineRenderers)
                Destroy(uiLineRenderer);
        _lineRenderers = new List<UILineRenderer>();
        for (int i = 0; i < _neuronPanels.Length - 1; i++)
        {
            for (int j = 0; j < _neuronPanels[i].Length; j++)
            {
                for (int l = 0; l < _neuronPanels[i + 1].Length; l++)
                {
                    var instantiatedLineRenderer = Instantiate(LineRenderer);
                    instantiatedLineRenderer.transform.SetParent(transform);
                    instantiatedLineRenderer.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

                    List<Vector2> pointlist = new List<Vector2>
                    {
                        _neuronPanels[i][j].GetComponent<RectTransform>().anchoredPosition,
                        _neuronPanels[i + 1][l].GetComponent<RectTransform>().anchoredPosition
                    };
                    float weightValue = selectedCar.Net.Weights[i][l][j];

                    instantiatedLineRenderer.Points = pointlist.ToArray();
                    instantiatedLineRenderer.lineThickness = weightValue * 4;
                    instantiatedLineRenderer.transform.SetAsFirstSibling();

                    if (weightValue < 0)
                        instantiatedLineRenderer.color = Color.black;

                    instantiatedLineRenderer.GetComponent<WeightInfo>().Weight = weightValue;

                    _lineRenderers.Add(instantiatedLineRenderer);

                }
            }
        }
    }
}

