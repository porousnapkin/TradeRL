using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class PlayerAbility : PlayerActivatedPower {
	
	[Inject] public DooberFactory dooberFactory { private get; set; }

	public int cooldown = 4;
	int turnsOnCooldown = 0;
	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; }}
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	public TargetedAnimation animation;
	public string abilityName;
	public Character character;
    public CombatController controller;
    public List<AbilityRestriction> restrictions { private get; set; }
    public List<AbilityCost> costs { private get; set; }
	public event System.Action<List<Character>> targetsPickedEvent = delegate{};
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
        this.callback = callback;
		targetPicker.PickTargets(TargetsPicked);
	}	

	public bool CanUse() {
		return turnsOnCooldown <= 0 && targetPicker.HasValidTarget() && restrictions.All(r => r.CanUse()) && costs.All(c => c.CanAfford());
	}

	void TargetsPicked(List<Character> targets) {
		targetsPickedEvent(targets);

		turnsOnCooldown = cooldown;

        //TODO: Is this correct?
		var messageAnchor = Grid.GetCharacterWorldPositionFromGridPositon((int)character.Position.x, (int)character.Position.y);
		dooberFactory.CreateAbilityMessageDoober(messageAnchor, abilityName);

		activator.Activate(targets, animation, callback);
	}

	public string GetName() 
	{
		return abilityName;
	}

    public void PayCosts()
    {
        costs.ForEach(c => c.PayCost());
    }

    public void RefundCosts()
    {
        costs.ForEach(c => c.Refund());
    }

    public List<AbilityCost> GetCosts()
    {
        return costs;
    }

    public List<AbilityRestriction> GetRestrictions()
    {
        return restrictions;
    }
}