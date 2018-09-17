using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour {

	public GameObject roadPrefab;

	public Transform lastRoadPart;

	public Transform playerCar;

	public bool isNeedFinishFlag;

	public GameObject finishFlag;

	public float FinishFlagDistance = 400f;

	private float distanceToSpawn = 17f;

	[HideInInspector]
	public float ObjectHandicap = 30f;

	private bool isFlagSpawned;

	const int START_ROAD_PARTS = 2;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < START_ROAD_PARTS; i++) {
			SpawnRoad ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (playerCar.position.x >= distanceToSpawn) {
			SpawnRoad ();
			distanceToSpawn += 30f;
		}
		if ((playerCar.position.x >= FinishFlagDistance ) && !isFlagSpawned && isNeedFinishFlag) {
			isFlagSpawned = true;
			SpawnFlag ();
		}
	}

	private void SpawnRoad(){
		GameObject newRoad = Instantiate (roadPrefab);
		newRoad.transform.position = new Vector3 (lastRoadPart.position.x+30f, lastRoadPart.position.y, lastRoadPart.position.z);
		lastRoadPart = newRoad.transform;
	}

	private void SpawnFlag(){
		GameObject flag = Instantiate (finishFlag);
		flag.transform.position = new Vector3 (lastRoadPart.position.x, -0.95f, 2.35f);
		flag.transform.rotation = finishFlag.transform.rotation;
	}
}
