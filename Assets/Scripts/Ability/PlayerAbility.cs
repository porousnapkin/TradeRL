using UnityEngine;
using System.Collections.Generic;

public class PlayerAbility {
	[Inject] public Effort effort { private get; set; }
	[Inject] public DooberFactory dooberFactory { private get; set; }

	public int cooldown = 4;
	int turnsOnCooldown = 0;
	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; }}
	public int effortCost = 1;
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	public TargetedAnimation animation;
	public string abilityName;
	public Character character;
    public CombatController controller;
    System.Action callback;

    public void Setup() {
		controller.ActEvent += AdvanceCooldown;
	}

	~PlayerAbility() {
        controller.ActEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

	public void Activate(System.Action callback) {
		if(!CanUse())
			return;

        this.callback = callback;
		targetPicker.PickTargets(TargetsPicked);
	}	

	public bool CanUse() {
		//TODO: Do I care about the target picker having a valid space?
		//targetPicker.HasValidTarget()
		return turnsOnCooldown <= 0 && effort.Value >= effortCost;
	}

	void TargetsPicked(List<Character> targets) {
		turnsOnCooldown = cooldown;

        //TODO: This should be more nuanced. I think we'll have 3 types of effort?
		effort.Spend(effortCost);

        //TODO: Is this correct?
		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)character.Position.x, (int)character.Position.y);
		dooberFactory.CreateAbilityMessageDoober(messageAnchor, abilityName);

		activator.Activate(targets, animation, callback);
	}
}