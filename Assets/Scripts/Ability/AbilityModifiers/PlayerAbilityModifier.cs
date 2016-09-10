using System;
using System.Collections.Generic;

public class PlayerAbilityModifier : PlayerActivatedPower
{
	public AbilityModifier abilityModifier;
	public string name;
	public int cooldown = 4;
	int turnsOnCooldown = 0;
    public List<AbilityCost> costs { private get; set; }

	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; } }

	public bool CanUse()
	{
		return TurnsRemainingOnCooldown <= 0;
	}

	public string GetName()
	{
		return name;
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
        //This isn't used. Need to figure out a coherent interface for these guys...
    }

	public void Activate(CombatController owner, List<Character> targets)
	{
		turnsOnCooldown = cooldown;
		abilityModifier.BeforeActivation(owner, targets);
	}

	public void Finish(CombatController owner, List<Character> targets)
	{
		abilityModifier.ActivationEnded(owner, targets);
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

    public List<Restriction> GetRestrictions()
    {
        return new List<Restriction>();
    }
}

