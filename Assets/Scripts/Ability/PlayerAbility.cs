using UnityEngine;
using System.Collections.Generic;

#warning "Feels like there should be two of these maybe... one for combat and one for non-combat?"
public class PlayerAbility {
	[Inject] public Effort effort { private get; set; }
	[Inject] public TurnManager turnManager { private get; set; }
	[Inject] public DooberFactory dooberFactory { private get; set; }

	public int cooldown = 4;
	int turnsOnCooldown = 0;
	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; }}
	public int effortCost = 1;
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	public LocationTargetedAnimation animation;
	public string abilityName;

	public MapPlayerView controller;
	public Character character;
	
	public void Setup(PlayerAbilityData data, Character owner) {
		turnManager.TurnEndedEvent += AdvanceCooldown;

		character = owner; 
		cooldown = data.cooldown;
		effortCost = data.effortCost;
		abilityName = data.abilityName;
		targetPicker = data.targetPicker.Create(owner);
		activator = data.activator.Create(owner);
		animation = data.animation.Create(owner);
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

		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)character.Position.x, (int)character.Position.y);
		dooberFactory.CreateAbilityMessageDoober(messageAnchor, abilityName);
		activator.Activate(targets, animation, ActionFinished);
	}

	//This is the map controller...
	void ActionFinished() {
		//controller.EndTurn();
	}
}