using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float power;
	public float powerLossPerSecond;

	public int minion1Cost;
	public int minion2Cost;
	public int minion3Cost;

	public UnityEngine.UI.Slider powerBar;

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
		power = Mathf.Clamp(power-powerLossPerSecond * Time.smoothDeltaTime,0,100);

		
		if(powerLossPerSecond<30)
			powerLossPerSecond = Time.timeSinceLevelLoad/30;
		//input
		if(Input.GetKeyDown("1")) {
			SpawnMinion1();
		}
		if(Input.GetKeyDown("2")) {
			SpawnMinion2();
		}
		if(Input.GetKeyDown("3")) {
			SpawnMinion3();
		}
	}

	public void LateUpdate() {
		powerBar.value = power;
	}

	public void SpawnMinion1() {
		if(power - minion1Cost < 0)
			return;

		Instantiate(minionDrainClosestPrefab,Global.map.mapCenter,Quaternion.identity);
		power -= minion1Cost;
	}
	public void SpawnMinion2() {
		if(power - minion2Cost < 0)
			return;
		Instantiate(minionDrainHighestPrefab,Global.map.mapCenter,Quaternion.identity);
		power -= minion2Cost;
	}
	public void SpawnMinion3() {
		if(power - minion3Cost < 0)
			return;
		Instantiate(minionPatrolPrefab,Global.map.mapCenter,Quaternion.identity);
		power -= minion3Cost;
	}
}
