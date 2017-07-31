using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActions:AIActions {

	public float maxPowerPerTile;

	protected override void Start() {
		base.Start();
		SphereCollider col = gameObject.AddComponent<SphereCollider>();
		col.radius = 5;
		col.isTrigger = true;
	}

	protected override bool ShouldDrain() {
		return target.power < maxPowerPerTile;
	}	

	protected override bool ShouldCheckTile(Tile tile) {
		return tile.power<maxPowerPerTile;
	}

	protected override void Attack(GameObject target) {
		if(attackTimer <= 0) {
			target.GetComponent<AIActions>().canAttack = false;
			Destroy(target);
			attackTimer = attackCooldown;
		}
	}
}