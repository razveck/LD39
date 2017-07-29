using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour {

	public float power;
	public float powerLossPerSecond;

	void Awake() {
		Global.powerManager = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		power -= powerLossPerSecond * Time.deltaTime;
	}
}
