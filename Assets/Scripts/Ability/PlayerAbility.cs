using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
    public List<AbilityRestriction> restrictions { private get; set; }
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
		return turnsOnCooldown <= 0 && effort.Value >= effortCost && targetPicker.HasValidTarget() && restrictions.All(r => r.CanUse());
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