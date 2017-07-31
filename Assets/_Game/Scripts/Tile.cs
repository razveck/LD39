using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public float power;
	public bool isTargeted;
	
	public GameObject treePrefab;
	public List<GameObject> trees;
	public float treeGrowthSpeed=0;
	public float maxTreeSize = 5;

	public float treeTimer;

	Color originalColor;

	void Start() {
		originalColor = GetComponent<Renderer>().material.color;
		trees = new List<GameObject>();
	}

	void Update() {

		if(power >= 5) {
			treeTimer -= Time.deltaTime;
			if(treeTimer <= 0) {
				trees.Add(Instantiate(treePrefab,new Vector3(transform.position.x + Random.Range(-2,3),transform.position.y,transform.position.z + Random.Range(-2,3)),Quaternion.identity,transform.parent));
				treeTimer = 60;
			}
		}
		
		for(int i = 0;i < trees.Count;i++) {
			Vector3 scale = trees[i].transform.localScale;

			scale.x = Mathf.Clamp(scale.x,0,maxTreeSize);
			scale.y = Mathf.Clamp(scale.x,0,maxTreeSize);
			scale.z = Mathf.Clamp(scale.x,0,maxTreeSize);

			scale *= 1 + Time.deltaTime * treeGrowthSpeed * (power > 0 ? 1 : -1);

			scale.x = Mathf.Clamp(scale.x,0,maxTreeSize);
			scale.y = Mathf.Clamp(scale.x,0,maxTreeSize);
			scale.z = Mathf.Clamp(scale.x,0,maxTreeSize);
			trees[i].transform.localScale = scale;
		}
	}

	public void ChangePower(float change) {
		if(power <= 0)
			return;
		power += change;
		Color c = GetComponent<Renderer>().material.color;
		c.r = Mathf.Clamp(c.r+change,originalColor.r-0.5f,originalColor.r);
		c.g = Mathf.Clamp(c.g + change,originalColor.g - 0.5f,originalColor.g);
		c.b = Mathf.Clamp(c.b + change,originalColor.b - 0.5f,originalColor.b);

		GetComponent<Renderer>().material.color = c;
	}
}
