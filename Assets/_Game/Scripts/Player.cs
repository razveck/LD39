using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float power;
	public float powerLossPerSecond;


	//prefabs	
	public GameObject minionDrainClosestPrefab;
	public GameObject minionDrainHighestPrefab;
	public GameObject minionPatrolPrefab;

	void Awake() {
		Global.player = this;
	}

	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		power -= powerLossPerSecond * Time.deltaTime;

		//input
		if(Input.GetKeyDown("1")) {
			Instantiate(minionDrainClosestPrefab,Global.map.mapCenter, Quaternion.identity);
		}
		if(Input.GetKeyDown("2")) {
			Instantiate(minionDrainHighestPrefab,Global.map.mapCenter,Quaternion.identity);
		}
		if(Input.GetKeyDown("3")) {
			Instantiate(minionPatrolPrefab,Global.map.mapCenter,Quaternion.identity);
		}
	}
}
