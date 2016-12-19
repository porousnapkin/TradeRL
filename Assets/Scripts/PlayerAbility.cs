using System.Collections.Generic;
using System.Linq;

public class PlayerAbility : PlayerActivatedPower {
	
	[Inject] public DooberFactory dooberFactory { private get; set; }
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
		return turnsOnCooldown <= 0 
            && targetPicker.HasValidTarget() 
            && restrictions.All(r => r.CanUse()) 
            && costs.All(c => c.CanAfford())
            && activeLabelRestrictions.DoLabelsPassRequirements(labels);
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

    public List<Cost> GetCosts()
    {
        return costs;
    }

    public List<Restriction> GetRestrictions()
    {
        return restrictions;
    }
    
    public List<AbilityLabel> GetLabels()
    {
        return labels;
    }
}