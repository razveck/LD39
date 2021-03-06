﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

	public Tile[,] grid;
	public Vector3 mapCenter;

	[Header("Terrain")]
	PerlinNoise2 noise;
	public int terrainSize;
	public int tileScale=1;
	public float terrainDensity;
	public float terrainElevation;
	public float terrainAmplitude;
	public Gradient terrainColorGradient;
	public GameObject navMeshObj;

	[Header("Power")]
	public float powerDensity;
	public float powerConcentration;
	public List<Tile> tilesByPower;

	[Header("Prefabs")]
	//prefabs
	public GameObject grassPrefab;
	public GameObject castlePrefab;

	void Awake() {
		Global.map = this;
	}

	// Use this for initialization
	void Start () {
		noise = new PerlinNoise2();
		tilesByPower = new List<Tile>();
		GenerateTerrain();
		Instantiate(castlePrefab, mapCenter, Quaternion.identity);
		navMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
		GeneratePowerNodes();
		for(int x = 0;x < 5;x++) {
			for(int z = 0;z < 5;z++) {
				grid[terrainSize/2-2+x,terrainSize / 2 - 2+z].ChangePower(-10);
			}
		}

		Global.gameManager.TerrainDoneCallback();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void GenerateTerrain() {
		grid = new Tile[terrainSize,terrainSize];

		float startPoint = (-terrainSize*tileScale / 2)-tileScale;

		for(int x = 0;x < terrainSize;x++) {
			for(int z = 0;z < terrainSize;z ++) {

				float height=noise.FractalNoise2D(x,z,3,terrainDensity,terrainAmplitude) +terrainElevation; //x, y, oct, freq, amp

				GameObject obj= Instantiate(grassPrefab,new Vector3(startPoint+x*tileScale + tileScale, height, startPoint+z*tileScale + tileScale),Quaternion.identity, transform);
				obj.transform.localScale = new Vector3(tileScale,tileScale* terrainAmplitude,tileScale);
				//obj.GetComponent<Renderer>().material.SetColor("_Color",height<0? Color.white : terrainColorGradient.Evaluate(height/2));
				obj.GetComponent<Renderer>().material.color=terrainColorGradient.Evaluate(height / 2);
				grid[x,z] = obj.GetComponent<Tile>();
			}
		}
		mapCenter = grid[terrainSize / 2,terrainSize / 2].transform.position;
	}

	void GeneratePowerNodes() {
		for(int x = 0;x < terrainSize;x++) {
			for(int z = 0;z < terrainSize;z++) {

				float powerValue = noise.FractalNoise2D(x, z, 8, powerDensity, powerConcentration)/5; //x, y, oct, freq, amp

				if(powerValue < 1) { //every tile must have power
					powerValue = 1;
				}

				grid[x,z].Initialize(powerValue);
				tilesByPower.Add(grid[x,z]);
			}
		}
	}
}
