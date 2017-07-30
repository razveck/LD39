using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Map : MonoBehaviour {

	public Tile[,] grid;

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
		RaycastHit hit;
		if(Physics.Raycast(new Vector3(0,100,0),Vector3.down,out hit)) {
			GameObject obj = Instantiate(castlePrefab,hit.point,Quaternion.identity);
		}
		navMeshObj.GetComponent<NavMeshSurface>().BuildNavMesh();
		GeneratePowerNodes();

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

				GameObject obj= Instantiate(grassPrefab,new Vector3(startPoint+x*tileScale + tileScale,height-(tileScale*terrainAmplitude/2),startPoint+z*tileScale + tileScale),Quaternion.identity, transform);
				obj.transform.localScale = new Vector3(tileScale,tileScale* terrainAmplitude,tileScale);
				//obj.GetComponent<Renderer>().material.SetColor("_Color",height<0? Color.white : terrainColorGradient.Evaluate(height/2));
				obj.GetComponent<Renderer>().material.color=height < 0 ? Color.white : terrainColorGradient.Evaluate(height / 2);
				grid[x,z] = obj.GetComponent<Tile>();
			}
		}
	}

	void GeneratePowerNodes() {
		for(int x = 0;x < terrainSize;x++) {
			for(int z = 0;z < terrainSize;z++) {

				float powerValue = noise.FractalNoise2D(x, z, 3, powerDensity, powerConcentration)+1; //x, y, oct, freq, amp

				if(powerValue < 0) { //negative power doesn't make sense
					powerValue = 0;
				}

				grid[x,z].power = powerValue;
				tilesByPower.Add(grid[x,z]);
			}
		}
		//@Do I need to sort?? Probably do...I guess
		tilesByPower.Sort((t1,t2) =>
		{
			if(t1.power > t2.power)
				return -1;
			else
				return 1;
		}
		);
	}
}
