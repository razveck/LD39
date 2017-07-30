using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActions:AIActions {

	public float maxPowerPerTile;

	public override IEnumerator ArrivedOnPowerNode() {
		yield return StartCoroutine(DrainPower());
		canMove = true;
		LookForPower();
	}

	protected override bool ShouldDrain() {
		return target.power < maxPowerPerTile;
	}	

	protected override bool ShouldCheckTile(Tile tile) {
		return tile.power<maxPowerPerTile;
	}

	protected override void Attack(GameObject target) {
		Destroy(target);
	}
}