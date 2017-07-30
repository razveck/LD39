using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	void Awake() {
		Global.gameManager = this;
	}

	public void TerrainDoneCallback() {
		Global.natureManager.enabled = true;
	}
}
