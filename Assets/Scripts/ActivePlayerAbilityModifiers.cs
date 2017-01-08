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
    System.Action callback;
    int finishedActivatorCount;

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

	public void ActivateBeforeAbility(List<Character> targets, System.Action callback) {
        this.callback = callback;
		this.lastTargets = targets;
        finishedActivatorCount = 0;

        if (activeAbilityModifiers.Count == 0)
            callback();

		activeAbilityModifiers.ForEach(a => a.BeforeAbility(lastTargets, CountActivators));
	}

    private void CountActivators()
    {
        finishedActivatorCount++;
        if (finishedActivatorCount >= activeAbilityModifiers.Count)
            callback();
    }

    public void HideButtons() 
	{
		modifierButtons.Hide();
	}

	public void ActivateAfterAbility(System.Action callback) 
	{
        this.callback = callback;
        finishedActivatorCount = 0;

        if (activeAbilityModifiers.Count == 0)
            callback();

		activeAbilityModifiers.ForEach(a => a.AfterAbility(lastTargets, CountActivators));
	}
}

