using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionActions : AIActions {

	protected override bool ShouldDrain() {
		return target.power >0;
	}

	protected override bool ShouldCheckTile(Tile tile) {
		if(tile.isTargeted || tile.power <= 0)
			return false;
		else return true;
	}

	protected override void Attack(GameObject target) {
		Destroy(target);
	}
}
