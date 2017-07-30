using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public float power;
	public bool isTargeted;
	
	public double timer;
	public GameObject treePrefab;
	public List<GameObject> trees;
	public float treeGrowthSpeed=0;


	void Start() {
		trees = new List<GameObject>();
		trees.Add(Instantiate(treePrefab,transform.position + Vector3.up * (transform.localScale.y / 2),Quaternion.identity, transform));
	}

	void Update() {
		timer += Time.deltaTime;

		for(int i = 0;i < trees.Count;i++) {
			Vector3 scale = trees[i].transform.localScale;
			scale *= 1 + Time.deltaTime * treeGrowthSpeed;
			trees[i].transform.localScale = new Vector3((float)timer,(float)timer,(float)timer) * treeGrowthSpeed; ;
		}
	}
}
