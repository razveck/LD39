using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

	public int size;
	public int scale=1;
	public PerlinNoise2 noise;
	public GameObject navMeshObj;

	//prefabs
	public GameObject grassPrefab;

	void Awake() {
		Global.map = this;
	}

	// Use this for initialization
	void Start () {
		noise = new PerlinNoise2();
		float startPoint = (-size*scale / 2)-scale;
		for(int x = 0;x < size* scale;x+=scale) {
			for(int z = 0;z < size* scale;z += scale) {
				float height=noise.FractalNoise2D(x,z,2,10,2); //x, y, oct, freq, amp
				GameObject obj= Instantiate(grassPrefab,new Vector3(startPoint+x + scale,height-(scale*10/2)+0.1f,startPoint+z + scale),Quaternion.identity);
				obj.transform.localScale = new Vector3(scale,scale*10,scale);
			}
		}
		navMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
