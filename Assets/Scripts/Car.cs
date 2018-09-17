using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{


    public int LastWaypoint;
    public GameObject Waypoints;
    public bool CarCrashed;
    public int Fitness;
    public float TimeSinceLastFitnessUpdate;
    public Toggle ShowRaycastsToggle;
    public int LapCount;
    public float BestLapTime = -1;
    public LearningManager LearningManager;
    public NeuralNetwork Net;
    public bool IsPlayer;
    
    private bool _initilized;
    private Rigidbody _rigidBody;
    private float LastFitnessUpdate { get; set; }


    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        Waypoints = GameObject.Find("Waypoints");
        TimeSinceLastFitnessUpdate = 0;
        LastFitnessUpdate = Time.time;
        LearningManager = GameObject.Find("Manager").GetComponent<LearningManager>();
        lapStartTime = Time.timeSinceLevelLoad;
        if(LearningManager.IsRacing)
            return;

        ShowRaycastsToggle = GameObject.Find("ShowRaycastsToggle").GetComponent<Toggle>();

        if (Net.MutationOfWhichPlace == 1)
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 0.84f, 0));
        else if (Net.MutationOfWhichPlace == 2)
            GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 1f, 1f));
        else if (Net.MutationOfWhichPlace == 3)
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.64f ,0.164f, 0.164f));
        else if (Net.MutationOfWhichPlace == -1)
            GetComponent<Renderer>().material.SetColor("_Color", new Color(0.240566f, 0.937f, 1f));
    }

    public float lapStartTime { get; set; }

    void FixedUpdate()
    {

        if (IsPlayer)
        {
            _rigidBody.AddRelativeForce(new Vector3(0, 0, Input.GetAxis("Vertical") * 60));
            transform.Rotate(new Vector3(0, Input.GetAxis("Horizontal") * 2f, 0));
            return;
        }
            

        if (CarCrashed)
            gameObject.SetActive(false);
        
        UpdateLastFitnessUpdate();

        if (_initilized)
        {
            RaycastHit hit;
            float distance = 200f;
            float[] inputs = new float[transform.Cast<Transform>().Count(x=>x.gameObject.activeSelf && x.gameObject.name.Contains("RayCast"))];

            int i = 0;
            foreach (var child in transform.Cast<Transform>().Where(x=>x.gameObject.activeSelf && x.gameObject.name.Contains("RayCast")))
            {
                Physics.Raycast(transform.position,
                    child.TransformDirection(Vector3.forward), out hit, distance);
                inputs[i] = Mathf.Clamp(hit.distance,0,10);
                i++;
                if(LearningManager.IsRacing)
                    continue;
                if (ShowRaycastsToggle.isOn)
                {
                    var lineRenderer = child.GetComponent<LineRenderer>();
                    lineRenderer.enabled = true;
                    lineRenderer.positionCount = 2;
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, hit.point);
                    lineRenderer.widthCurve = AnimationCurve.Constant(0f,0f,1);
                    
                    

                }
                else
                {
                    var lineRenderer = child.GetComponent<LineRenderer>();
                    lineRenderer.enabled = false;
                }
            }

            float[] output = Net.FeedForward(inputs);


            _rigidBody.AddRelativeForce(new Vector3(0, 0, output[0] * 60));
            transform.Rotate(new Vector3(0, output[1] * 2f, 0));

            Net.UpdateFitness(Fitness);
        }
    }

  

    void OnTriggerEnter(Collider other)
    {
        if (other.name.Split(' ')[0] == "Waypoint")
        {

            var triggeredWaypoint = Int32.Parse(other.name.Split(' ')[1]);
            if (triggeredWaypoint - LastWaypoint != 1 &&
                (LastWaypoint != Waypoints.transform.childCount - 1 
                || triggeredWaypoint != 0)) return;
            Fitness += 1;
            LastWaypoint = triggeredWaypoint;
            ResetTimeFitnessUpdate();
            if (triggeredWaypoint == 0)
            {
                LapCount++;
                var thisLapTime = Time.timeSinceLevelLoad - lapStartTime;
                if (thisLapTime < BestLapTime || BestLapTime == -1f)
                    BestLapTime = thisLapTime;
                lapStartTime = Time.timeSinceLevelLoad;
            }

            UpdateLastFitnessUpdate();
        }
    }
    public void Init(NeuralNetwork net)
    {
        this.Net = net;
        _initilized = true;
    }

    public String GetNetworkName()
    {
        return Net.Name;
        
    }

    private void UpdateLastFitnessUpdate()
    {
        TimeSinceLastFitnessUpdate = Time.time - LastFitnessUpdate;
    }

    private void ResetTimeFitnessUpdate()
    {
        LastFitnessUpdate = Time.time;
    }

}

