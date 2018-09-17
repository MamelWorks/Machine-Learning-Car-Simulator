using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{

    public float speed;

    void Update()
    {
        Time.timeScale = speed;

    }

    public void ValueChanged(float value)
    {
        speed = value;
    }
}
