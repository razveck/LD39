using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Actions {
	DrainPower,
	Patrol
}

public class AIActions : MonoBehaviour {

	AINavigation nav;
	public Actions actions;

	public float drainPerSecond;

	// Use this for initialization
	void Start () {
		nav = GetComponent<AINavigation>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator ArrivedOnPowerNode(Tile tile) {
		yield return StartCoroutine(DrainPower(tile));
		nav.LookForPower();
	}

	IEnumerator DrainPower(Tile tile) {
		if(tile.power > 0) {
			tile.power -= Time.deltaTime * drainPerSecond;
			yield return null;
			StartCoroutine(DrainPower(tile));
		}
	}
}
