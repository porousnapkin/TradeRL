using System.Collections.Generic;

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

