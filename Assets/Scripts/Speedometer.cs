using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedometer : MonoBehaviour
{

    const float MAX_SPEED = 240f;

    public Transform arrow;
    public GameObject PlayerCar;

    static float minAngle = 116f;
    static float maxAngle = -117f;

    void Update()
    {
        if (PlayerCar != null)
            ShowArrowAngle(PlayerCar.transform
                .InverseTransformDirection(PlayerCar.gameObject.GetComponent<Rigidbody>().velocity).z * 5);
    }

    public void ShowArrowAngle(float currentSpeed)
    {
        if (arrow != null)
        {
            float ang = Mathf.Lerp(minAngle, maxAngle, Mathf.InverseLerp(0, MAX_SPEED, currentSpeed));
            arrow.eulerAngles = new Vector3(0, 0, ang);
        }

    }

}
