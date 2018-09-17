using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarSelector : MonoBehaviour
{
    public GameObject SelectedCarGameObject;
    public LearningManager LearningManager;
    public GameObject InfoPanel;
    public Text CarInfoText;

    void Update()
    {
        if (SelectedCarGameObject != null)
            if (!SelectedCarGameObject.activeSelf)
                SelectedCarGameObject = null;

        if (SelectedCarGameObject != null)
        {
            var SelectedCar = SelectedCarGameObject.GetComponent<Car>();
            InfoPanel.SetActive(true);
            string infoText =
                "<b>Name of Neural Network:</b> " + "\n" + "   " + SelectedCar.GetNetworkName() + "\n"
                + "<b>Place:</b> " + (LearningManager.SortedCars.IndexOf(SelectedCarGameObject.GetComponent<Car>()) + 1) + "\n"
                + "<b>Fitness:</b> " + SelectedCar.Fitness + "\n"
                + "<b>Last Fitness Update:</b> " +
                MamelsHelpMethods.FloatToTime(SelectedCar.TimeSinceLastFitnessUpdate, "0:00.00") + "\n"
                + "<b>Best Lap Time:</b> " + MamelsHelpMethods.FloatToTime(SelectedCar.BestLapTime, "0:00.00") + "\n"
                + "<b>Lap Count:</b> " + SelectedCar.LapCount + "\n"
                + "<b>Speed:</b> " + SelectedCarGameObject.transform
                    .InverseTransformDirection(SelectedCar.gameObject.GetComponent<Rigidbody>().velocity).z + "\n";

            CarInfoText.text = infoText;
        }
        else
        {
            InfoPanel.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo = new RaycastHit();

            bool hit = Physics.Raycast((Camera.main ?? FindObjectsOfType<Camera>().FirstOrDefault(x=>x.gameObject.activeSelf)).ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                if (hitInfo.transform.gameObject.tag == "Car")
                {
                    if (SelectedCarGameObject != null)
                    {
                        SelectedCarGameObject.transform.Find("IsSelectedLight").gameObject.SetActive(false);
                    }
                    SelectedCarGameObject = hitInfo.transform.gameObject;
                    SelectedCarGameObject.transform.Find("IsSelectedLight").gameObject.SetActive(true);
                }
            }
            else
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                if (SelectedCarGameObject != null)
                    SelectedCarGameObject.transform.Find("IsSelectedLight").gameObject.SetActive(false);
                SelectedCarGameObject = null;
            }
        }
    }
}
