using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour {

	public GameObject heroPrefab;

	void Awake() {
		Global.natureManager = this;
	}

	public void Initialize() {
		List<Tile> list = Global.map.tilesByPower;
		GameObject root = new GameObject("Heroes");
		for(int i = 0;i < list.Count;i++) {
			GameObject obj = Instantiate(heroPrefab,list[i].transform.position+Vector3.up*10,Quaternion.identity,root.transform);
			obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(list[i].power,list[i].power,list[i].power));
		}
	}
}
