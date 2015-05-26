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
		//TODO: Wha? Is... this acceptable? I think we should have an instance of the controller here...
		PlayerController.Instance.EndTurn();
	}
}