﻿using System.Collections.Generic;

public class PlayerAbilityModifier : PlayerActivatedPower
{
    [Inject] public ActiveLabelRequirements activeLabelRequirements { private get; set; }
    [Inject] public PlayerAbilityButtons abilityButtons { private get; set; }

	public AbilityModifier abilityModifier;
	public string name;
	public int cooldown = 4;
	int turnsOnCooldown = 0;
    public string description;

    public List<Cost> costs { private get; set; }
    public bool hasLabelRequirements { private get; set; }
    public List<AbilityLabel> labelRequirements { private get; set; }

	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; } }
    System.Action callback;

    public bool CanUse()
	{
        return TurnsRemainingOnCooldown <= 0
            && costs.TrueForAll(c => c.CanAfford())
            && (!hasLabelRequirements || activeLabelRequirements.DoRequirementsMeetActiveLabels(labelRequirements));
	}

	public string GetName()
	{
		return name;
	}
    
    public string GetDescription()
    {
        return description;
    }

	public AbilityModifier GetAbilityModifier()
	{
		return abilityModifier;
	}

	public void TurnEnd()
	{
		turnsOnCooldown--;
	}

    public void Activate(System.Action callback)
    {
    }

	public void BeforeAbility(List<Character> targets, System.Action callback)
	{
		turnsOnCooldown = cooldown;
		abilityModifier.BeforeActivation(targets, callback);
	}

	public void AfterAbility(List<Character> targets, System.Action callback)
	{
		abilityModifier.ActivationEnded(targets, callback);
	}

    public void PayCosts()
    {
        activeLabelRequirements.AddRequirements(this);
        abilityButtons.ShowButtons();

        costs.ForEach(c => c.PayCost());
    }

    public void RefundCosts()
    {
        activeLabelRequirements.RemoveRequirements(this);
        abilityButtons.ShowButtons();

        costs.ForEach(c => c.Refund());
    }

    public List<Cost> GetCosts()
    {
        return costs;
    }

    public List<Restriction> GetRestrictions()
    {
        return new List<Restriction>();
    }

    public List<AbilityLabel> GetLabelRestrictions()
    {
        if (!hasLabelRequirements)
            labelRequirements.Clear();

        return labelRequirements;
    }
}

