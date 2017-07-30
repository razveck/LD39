using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour {

	NavMeshAgent agent;
	AIActions actions;
	Tile target;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		actions = GetComponent<AIActions>();
		LookForPower();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.1f) {
			StartCoroutine(actions.ArrivedOnPowerNode(target));
		}
	}

	void FindNewDestination() {
		//agent.destination = new Vector3(transform.position.x + (Random.Range(-1f,2f)+10),0,0);
	}

	public void LookForPower() {
		List<Tile> list = Global.map.tilesByPower;
		float closestDistance=1000000;
		NavMeshPath path=new NavMeshPath();
		for(int i = 0;i < list.Count;i++) {
			if(list[i].isTargeted || list[i].power<=0)
				continue;

			NavMesh.CalculatePath(transform.position,list[i].transform.position,NavMesh.AllAreas,agent.path);
			if(agent.path.status == NavMeshPathStatus.PathComplete) {
				if(agent.remainingDistance < closestDistance) {
					target = list[i];
				} else {
					continue;
				}
			} else {
				continue;
			}
		}
		target.isTargeted = true;
		Vector3 pos = target.transform.position;
		pos.y = transform.position.y;
		agent.destination = pos;
	}	

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(agent.destination,1f);
	}
}
