using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NatureManager : MonoBehaviour {

	public float powerThreshhold;
	public float spawnCooldown;
	public float spawnTimer;

	public GameObject heroPrefab;

	void Awake() {
		Global.natureManager = this;
		spawnTimer = spawnCooldown;
	}

	void Update() {
		spawnTimer -= Time.deltaTime;
		if(spawnTimer <= 0) {
			SpawnHero();
			spawnTimer = spawnCooldown;
		}
	}

	void SpawnHero() {
		int size = Global.map.terrainSize;
		int x = Random.Range(0f,1f)==0?0:size-1;
		int z = Random.Range(0f,1f) == 0 ? 0 : size - 1;
		Vector3 pos = Global.map.grid[x,z].transform.position;
		GameObject obj=Instantiate(heroPrefab,pos,Quaternion.identity);
		obj.GetComponent<UnityEngine.AI.NavMeshAgent>().nextPosition = obj.transform.position;
	
	}
}
