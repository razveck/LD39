using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour {

	public float powerThreshhold;

	public GameObject heroPrefab;

	void Awake() {
		Global.natureManager = this;
	}

	public void Initialize() {
		List<Tile> list = Global.map.tilesByPower;
		GameObject root = new GameObject("Heroes");
		for(int i = 0;i < list.Count;i++) {
			if(list[i].power-1 > powerThreshhold) {
				GameObject obj = Instantiate(heroPrefab,list[i].transform.position + Vector3.up * 20,Quaternion.identity,root.transform);
				obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(list[i].power-1/ powerThreshhold-1,list[i].power-1/ powerThreshhold-1,list[i].power-1/ powerThreshhold-1));
			}
		}
	}
}
