using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : AIActions {

	protected override void Start() {
		base.Start();
		SphereCollider col = gameObject.AddComponent<SphereCollider>();
		col.radius = 5;
		col.isTrigger = true;
	}

	protected override bool ShouldDrain() {
		return target.power >0;
	}

	protected override bool ShouldCheckTile(Tile tile) {
		if(tile.isTargeted || tile.power <= 0)
			return false;
		else return true;
	}

	protected override void Attack(GameObject target) {
		if(attackTimer <= 0) {
			target.GetComponent<AIActions>().canAttack = false;
			Destroy(target);
			attackTimer = attackCooldown;
		}
	}
}
