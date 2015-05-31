using UnityEngine;
using System.Collections.Generic;

public class PlayerAbility {
	public int cooldown = 4;
	int turnsOnCooldown = 0;
	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; }}
	public int effortCost = 1;
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	public LocationTargetedAnimation animation;
	public string abilityName;

	public Effort effort;
	public PlayerController controller;
	TurnManager turnManager;

	public PlayerAbility(TurnManager turnManager) {
		turnManager.TurnEndedEvent += AdvanceCooldown;
		this.turnManager = turnManager;
	} 

	~PlayerAbility() { 
		turnManager.TurnEndedEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

	public void Activate() {
		if(!CanUse())
			return;

		targetPicker.PickTargets(TargetsPicked);
	}	

	public bool CanUse() {
		//TODO: Do I care about the target picker having a valid space?
		//targetPicker.HasValidTarget()
		return turnsOnCooldown <= 0 && effort.Value >= effortCost;
	}

	void TargetsPicked(List<Vector2> targets) {
		turnsOnCooldown = cooldown;
		effort.Spend(effortCost);

		activator.Activate(targets, animation, ActionFinished);
	}

	void ActionFinished() {
		controller.EndTurn();
	}
}