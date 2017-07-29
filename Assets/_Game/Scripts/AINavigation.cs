using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AINavigation : MonoBehaviour {

	NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if(agent.remainingDistance < 0.1f) {
			FindNewDestination();
		}
	}

	void FindNewDestination() {
		agent.destination = new Vector3(transform.position.x + (Random.Range(-1f,2f)+10),0,0);
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(agent.destination,0.25f);
	}
}
