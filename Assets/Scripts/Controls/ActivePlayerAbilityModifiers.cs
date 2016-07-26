using System;
using System.Collections.Generic;

public class ActivePlayerAbilityModifiers
{
	[Inject]public PlayerAbilityModifierButtons modifierButtons { private get; set; }
	public Character owner;
	public List<PlayerAbilityModifier> abilityModifiers = new List<PlayerAbilityModifier>();

	List<Character> lastTargets;
	PlayerAbility lastAbility;

	public void Setup() 
	{
		modifierButtons.Setup(abilityModifiers);
		modifierButtons.buttonActivatedEvent += AddModifier;
		modifierButtons.buttonDeactivatedEvent += RemoveModifier;
	}

	void AddModifier(PlayerAbilityModifier modifier) 
	{
		abilityModifiers.Add(modifier);
	}

	void RemoveModifier(PlayerAbilityModifier modifier)
	{
		abilityModifiers.Remove(modifier);
	}

	public void SetupForTurn() 
	{
		abilityModifiers.Clear();
		modifierButtons.Show();
	}

	public void SetupForAbility(PlayerAbility ability) 
	{
		lastAbility = ability;
		lastAbility.targetsPickedEvent += TargetsPicked;
	}

	void TargetsPicked(List<Character> targets) {
		lastTargets = targets;
		abilityModifiers.ForEach(a => a.Activate(owner, lastTargets));
	}

	public void HideButtons() 
	{
		modifierButtons.Hide();
	}

	public void Cleanup() 
	{
		lastAbility.targetsPickedEvent -= TargetsPicked;
		abilityModifiers.ForEach(a => a.Finish(owner, lastTargets));
	}
}

