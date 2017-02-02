using System.Collections.Generic;

public class PlayerAbilityModifier : PlayerActivatedPower, LabelRequiringElement, LabeledElement
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
    public List<AbilityLabel> labels { private get; set; }

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
        activeLabelRequirements.AddLabels(this);
        abilityButtons.ShowButtons();

        costs.ForEach(c => c.PayCost());
    }

    public void RefundCosts()
    {
        activeLabelRequirements.RemoveRequirements(this);
        activeLabelRequirements.RemoveLabels(this);
        abilityButtons.ShowButtons();

        costs.ForEach(c => c.Refund());
    }

    public List<AbilityLabel> GetLabelRestrictions()
    {
        if (!hasLabelRequirements)
            labelRequirements.Clear();

        return labelRequirements;
    }

    public List<Visualizer> GetVisualizers()
    {
        var l = new List<Visualizer>();
        l.AddRange(GetVisualizersFromList(costs));
        return l;
    }

    List<Visualizer> GetVisualizersFromList<T>(List<T> list)
    {
        var newList = list.ConvertAll(x => x as Visualizer);
        newList.RemoveAll(x => x == null);
        return newList;
    }

    public List<AbilityLabel> GetLabels()
    {
        return labels;
    }
}

