using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerAbility : PlayerActivatedPower, LabeledElement {
    [Inject] public ActiveLabelRequirements activeLabelRestrictions { private get; set; }
    [Inject] public PlayerAbilityModifierButtons playerAbilityModifierButtons { private get; set; }

	public int cooldown = 4;
	int turnsOnCooldown = 0;
	public int TurnsRemainingOnCooldown { get { return turnsOnCooldown; }}
	public AbilityTargetPicker targetPicker;
	public AbilityActivator activator;
	public TargetedAnimation animation;
	public string abilityName;
    public string abilityDescription;
	public Character character;
    public CombatController controller;
    public List<Restriction> restrictions { private get; set; }
    public List<Cost> costs { private get; set; }
	public event System.Action<List<Character>> targetsPickedEvent = delegate{};
    System.Action callback;
    public List<AbilityLabel> labels { private get; set; }
    ActivePlayerAbilityModifiers abilityModifiers;
    List<Character> targets;

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

    public void SetAbilityModifiers(ActivePlayerAbilityModifiers abilityModifiers)
    {
        this.abilityModifiers = abilityModifiers;
    }

    public void SelectTargets(System.Action callback)
    {
        this.callback = callback;
		targetPicker.PickTargets(TargetsPicked);
    }

    void TargetsPicked(List<Character> targets)
    {
        this.targets = targets;
        targetsPickedEvent(targets);
        callback();
    }

    public void CancelTargetSelection()
    {
        targetPicker.CancelPicking();
    }

    public void Activate(System.Action callback) {
        this.callback = callback;

        if(abilityModifiers != null)
            abilityModifiers.ActivateBeforeAbility(targets, FinishActivatingAbility);
    }

    private void FinishActivatingAbility()
    {
        turnsOnCooldown = cooldown;

        activator.Activate(targets, animation, SendOffAfterAbilityModifiers);
    }

    public bool CanUse() {
		return turnsOnCooldown <= 0 
            && targetPicker.HasValidTarget() 
            && restrictions.All(r => r.CanUse()) 
            && costs.All(c => c.CanAfford())
            && activeLabelRestrictions.DoLabelsPassRequirements(labels);
	}

    void SendOffAfterAbilityModifiers()
    {
        abilityModifiers.ActivateAfterAbility(callback);
    }

    public string GetName() 
	{
		return abilityName;
	}

    public string GetDescription()
    {
        return abilityDescription;
    }

    public void PayCosts()
    {
        activeLabelRestrictions.AddLabels(this);
        playerAbilityModifierButtons.Show();

        costs.ForEach(c => c.PayCost());
    }

    public void RefundCosts()
    {
        activeLabelRestrictions.RemoveLabels(this);
        playerAbilityModifierButtons.Show();

        costs.ForEach(c => c.Refund());
    }

    public List<AbilityLabel> GetLabels()
    {
        return labels;
    }

    public List<Visualizer> GetVisualizers()
    {
        var l = new List<Visualizer>();
        l.AddRange(GetVisualizersFromList(costs));
        l.AddRange(GetVisualizersFromList(restrictions));
        if (activator is Visualizer)
            l.Add(activator as Visualizer);
        return l;
    }

    List<Visualizer> GetVisualizersFromList<T>(List<T> list)
    {
        var newList = list.ConvertAll(x => x as Visualizer);
        newList.RemoveAll(x => x == null);
        return newList;
    }
}