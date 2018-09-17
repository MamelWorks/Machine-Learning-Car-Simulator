using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    // Use this for initialization
    public CarSelector Selector;
    public Camera MainCamera;
    public GameObject CameraPrefab;

    private GameObject _followCamera;



    // Update is called once per frame
    void Update()
    {
        if (_followCamera == null || !_followCamera.gameObject.activeInHierarchy)
        {
            MainCamera.gameObject.SetActive(true);
            _followCamera = null;
        }
        else
        {
            if (Selector.SelectedCarGameObject == null)
                return;
            if (_followCamera.transform.parent != Selector.SelectedCarGameObject.transform)
            {
                Follow();
            }
        }

    }

    public void Follow()
    {
        //0, 2.4, -1.35
        //18.92, 0, 0
        MainCamera.gameObject.SetActive(false);
        if(_followCamera != null)
            _followCamera.SetActive(false);
        _followCamera = ((GameObject)Instantiate(CameraPrefab, new Vector3(0, 0, 0), CameraPrefab.transform.localRotation));
        _followCamera.transform.parent = Selector.SelectedCarGameObject.transform;
        _followCamera.transform.localPosition = new Vector3(0, 2.4f, -1.35f);

        _followCamera.transform.localEulerAngles = new Vector3(_followCamera.transform.localEulerAngles.x, 0,0);



    }

    public void Follow(GameObject carToFollow)
    {
        Selector.SelectedCarGameObject = carToFollow;
        Follow();
    }

}
