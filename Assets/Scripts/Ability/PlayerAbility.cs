using UnityEngine;
using System.Collections.Generic;

public class PlayerAbility {
	public AbilityCost cost;
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;

	public void Activate() {
		if(!cost.CanAfford())
			return;

		targetPicker.PickTargets(TargetsPicked);
	}	

	void TargetsPicked(List<Vector2> targets) {
		cost.PayCost();
		activator.Activate(targets, ActionFinished);
	}

	void ActionFinished() {
		PlayerController.Instance.EndTurn();
	}
}