using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ActionType {
	DrainHighest,
	DrainClosest,
	Patrol
}

public class AIActions : MonoBehaviour {

	public ActionType actionType;

	//delegates depending on the type
	delegate void GetDestination();
	GetDestination getDest;
	delegate void ArrivedOnDest();
	ArrivedOnDest arrived;

	public float drainPerSecond;
	protected NavMeshAgent agent;
	public Tile target;

	public bool canMove;
	public bool shouldSetTarget;

	// Use this for initialization
	void Start() {
		agent = GetComponent<NavMeshAgent>();
		if(actionType == ActionType.Patrol) {
			getDest =Patrol;
			arrived = PatrollerArrived;
			SphereCollider col=gameObject.AddComponent<SphereCollider>();
			col.radius = 5;
			col.isTrigger = true;
		} else {
			getDest=LookForPower;
			arrived = DrainerArrived;
			
		}
		getDest();
	}

	// Update is called once per frame
	protected void Update() {
		if(agent.pathPending || agent.path.status!=NavMeshPathStatus.PathComplete)
			return;

		if(target == null) {
			getDest();
		}

		if(agent.destination != target.transform.position)
			agent.SetDestination(target.transform.position);

		if(canMove == false)
			return;

		if(agent.remainingDistance < 0.01f) {
			arrived();
		}
	}

	protected void PatrollerArrived() {
		getDest();
	}

	protected void DrainerArrived() {
			canMove = false;
			StartCoroutine(ArrivedOnPowerNode());
	}
	
	public virtual IEnumerator ArrivedOnPowerNode() {
		yield return StartCoroutine(DrainPower());
		target.isTargeted = false;
		canMove = true;
		LookForPower();
	}

	protected virtual IEnumerator DrainPower() {
		Debug.Log("DRAINING");
		while(ShouldDrain()) {
			target.ChangePower(-Time.deltaTime * drainPerSecond);
			yield return null;
		}
	}

	protected virtual bool ShouldDrain() {
		throw new System.Exception();
	}

	public virtual void Patrol() {
		int size = Global.map.terrainSize;
		target = Global.map.grid[Random.Range(0,size - 1),Random.Range(0,size - 1)];
		agent.SetDestination(target.transform.position);
	}

	public virtual void LookForPower() {
		List<Tile> list = Global.map.tilesByPower;
		switch(actionType) {
			case ActionType.DrainClosest:
				list.Sort(SortByDistance);
				break;
			case ActionType.DrainHighest:
				list.Sort(SortByPower);
				break;
		}
		for(int i = 0;i < list.Count;i++) {
			if(ShouldCheckTile(list[i]))
				target = list[i];
			else
				continue;
		}
		target.isTargeted = shouldSetTarget;
		agent.SetDestination(target.transform.position);
	}

	protected virtual bool ShouldCheckTile(Tile tile) {
		throw new System.Exception();
	}

	protected int SortByDistance(Tile t1,Tile t2) { //@this is sorting by pure distance and not path distance!
		if(Vector3.Distance(t1.transform.position,transform.position) > Vector3.Distance(t2.transform.position,transform.position)) {
			return -1;
		} else {
			return 1;
		}
	}

	protected int SortByPower(Tile t1,Tile t2) {
		if(t1.power < t2.power)
			return -1;
		else
			return 1;
	}

	public float GetPathLength(NavMeshPath path) {
		float length = 0.0f;

		for(int i = 1;i < path.corners.Length;i++) {
			length += Vector3.Distance(path.corners[i - 1],path.corners[i]);
		}

		return length;
	}

	protected virtual void Attack(GameObject target) {
		throw new System.Exception();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(agent.destination,1f);
	}

	void OnTriggerEnter(Collider other) {
		//Debug.Log("Trigger called on "+name + " / "+actionType.ToString()+". Other is "+other.name+". Is it a trigger?...--->" + other.isTrigger);
		if(other.isTrigger == false) {
			Attack(other.gameObject);
		}
	}

	void OnDestroy() {
		target.isTargeted = false;
	}
}
