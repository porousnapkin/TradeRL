using System;
using System.Collections.Generic;

public class ActivePlayerAbilityModifiers
{
	[Inject]public PlayerAbilityModifierButtons modifierButtons { private get; set; }
    [Inject]public PlayerCharacter playerCharacter { private get; set; }
	public CombatController owner;
	List<PlayerAbilityModifier> activeAbilityModifiers = new List<PlayerAbilityModifier>();

	List<Character> lastTargets;
	PlayerAbility lastAbility;

	public void Setup() 
	{
        playerCharacter.abilityModifiersChanged += AbilityModifiersChanged;

		modifierButtons.Setup(playerCharacter.GetCombatAbilityModifiers(owner));
		modifierButtons.buttonActivatedEvent += AddModifier;
		modifierButtons.buttonDeactivatedEvent += RemoveModifier;
	}

    public void Cleanup()
    {
        playerCharacter.abilityModifiersChanged -= AbilityModifiersChanged;
    }

    private void AbilityModifiersChanged()
    {
		modifierButtons.Setup(playerCharacter.GetCombatAbilityModifiers(owner));
    }

    void AddModifier(PlayerAbilityModifier modifier) 
	{
		activeAbilityModifiers.Add(modifier);
	}

	void RemoveModifier(PlayerAbilityModifier modifier)
	{
		activeAbilityModifiers.Remove(modifier);
	}

	public void SetupForTurn() 
	{
		activeAbilityModifiers.Clear();
		modifierButtons.Show();
	}

	public void SetupForAbility(PlayerAbility ability) 
	{
		lastAbility = ability;
		lastAbility.targetsPickedEvent += TargetsPicked;
	}

	void TargetsPicked(List<Character> targets) {
		lastTargets = targets;
		activeAbilityModifiers.ForEach(a => a.Activate(owner, lastTargets));
	}

	public void HideButtons() 
	{
		modifierButtons.Hide();
	}

	public void Finish() 
	{
		lastAbility.targetsPickedEvent -= TargetsPicked;
		activeAbilityModifiers.ForEach(a => a.Finish(owner, lastTargets));
	}
}

