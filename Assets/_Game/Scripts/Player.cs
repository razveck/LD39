using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float power;
	public float powerLossPerSecond;

	//prefabs
	
	public GameObject minionPrefab;

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
			Instantiate(minionPrefab);
		}
	}
}
