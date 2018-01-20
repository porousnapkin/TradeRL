using System.Collections.Generic;

public class PlayerAbilityModifier : PlayerActivatedPower, LabelRequiringElement, LabeledElement
{
    [Inject] public ActiveLabelRequirements activeLabelRequirements { private get; set; }
    [Inject] public PlayerAbilityButtons abilityButtons { private get; set; }

	public AbilityModifier abilityModifier;
    public CombatController owner; 
	public string name;
	public int cooldown = 4;
	int turnsOnCooldown = 0;
    public string description;
    public bool usesAbilitysTargets = true;

    public List<Cost> costs { private get; set; }
    public bool hasLabelRequirements { private get; set; }
    public List<AbilityLabel> labelRequirements { private get; set; }
    public List<AbilityLabel> labels { private get; set; }
    public AbilityTargetPicker targetPicker { private get; set; }

    public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; } }
    System.Action callback;
    CombatController.InitiativeModifier initMod;

    public void SetInitiativeModifiation(int mod)
    {
        initMod = new CombatController.InitiativeModifier();
        initMod.amount = mod;
        initMod.description = name;
        initMod.removeAtTurnEnd = true;
    }

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

    public void PrepareActivation(List<Character> targets, System.Action callback)
    {
        GetAppropriateTargets(targets, (newTargets) =>
        {
            abilityModifier.PrepareActivation(newTargets, callback);
        });
    }

    void GetAppropriateTargets(List<Character> abilitysTargets, System.Action<List<Character>> callback)
    {
        if (usesAbilitysTargets)
            callback(abilitysTargets);
        else
            targetPicker.PickTargets(callback);
            
    }

    public void Activate(System.Action callback)
    {
    }

	public void BeforeAbility(List<Character> targets, System.Action callback)
	{
        turnsOnCooldown = cooldown;
        GetAppropriateTargets(targets, (newTargets) =>
        {
		    abilityModifier.BeforeActivation(newTargets, callback);
        });
	}

	public void AfterAbility(List<Character> targets, System.Action callback)
	{
        GetAppropriateTargets(targets, (newTargets) =>
        {
            abilityModifier.ActivationEnded(newTargets, callback);
        });
	}

    public void PrePurchase()
    {
        owner.AddInitiativeModifier(initMod);

        activeLabelRequirements.AddRequirements(this);
        activeLabelRequirements.AddLabels(this);
        abilityButtons.ShowButtons();

        costs.ForEach(c => c.PayCost());
    }

    public void RefundUse()
    {
        owner.RemoveInitiativeModifier(initMod);

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

