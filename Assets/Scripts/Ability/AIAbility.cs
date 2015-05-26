using UnityEngine;
using System.Collections.Generic;

public class AIAbility {
	public int cooldown = 1;
	int turnsOnCooldown = 0;
	public AIController controller;
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	TurnManager turnManager;

	public AIAbility(TurnManager turnManager) {
		turnManager.TurnEndedEvent += AdvanceCooldown;
		this.turnManager = turnManager;
	}

	~AIAbility() { 
		turnManager.TurnEndedEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

	public void PerformAction() {
		turnsOnCooldown = cooldown;
		targetPicker.PickTargets(TargetsPicked);
	}	

	void TargetsPicked(List<Vector2> targets) {
		activator.Activate(targets, ActionFinished);
	}

	void ActionFinished() {
		controller.EndTurn();
	}

	public bool CanUse() {
		return turnsOnCooldown <= 0 && targetPicker.HasValidTarget();
	}
}