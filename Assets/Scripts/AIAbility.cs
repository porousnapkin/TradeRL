using System.Linq;
using System.Collections.Generic;

public class AIAbility {
    public string abilityName;
    public string abilityDescription;
	public int cooldown = 1;
	int turnsOnCooldown = 0;
	public CombatController controller { private get; set; }
	public AbilityTargetPicker targetPicker { private get; set; }
	public AbilityActivator activator { private get; set; }
	public TargetedAnimation animation { private get; set; }
	public string displayMessage { private get; set; }
    public List<Restriction> restrictions { private get; set; }
    public List<AbilityLabel> labels { private get; set; }
    System.Action performCallback;
    System.Action preparedCallback;
    CombatController.InitiativeModifier initMod;

    public void SetInitiativeModifiation(int mod)
    {
        initMod = new CombatController.InitiativeModifier();
        initMod.amount = mod;
        initMod.description = displayMessage;
        initMod.removeAtTurnEnd = true;
    }

    public void Setup() {
		controller.ActEvent += AdvanceCooldown;
	}
	
	~AIAbility() {
        controller.ActEvent -= AdvanceCooldown;
	}

	void AdvanceCooldown() {
		if(turnsOnCooldown > 0)
			turnsOnCooldown--;
	}

    public void Prepare(System.Action preparedCallback)
    {
        controller.AddInitiativeModifier(initMod);

        this.preparedCallback = preparedCallback;
        targetPicker.PrePickTargets(TargetsPrePicked);
    }

    void TargetsPrePicked(List<Character> targets)
    {
        controller.character.attackModule.activeLabels = labels;
        activator.PrepareActivation(targets, animation, preparedCallback);
    }

	public void PerformAction(System.Action callback) {
        this.performCallback = callback;
		turnsOnCooldown = cooldown;

		targetPicker.PickTargets(TargetsPicked);
	}

    void TargetsPicked(List<Character> targets) {
        controller.character.attackModule.activeLabels = labels;

        activator.Activate(targets, animation, ActionFinished);
	}

	void ActionFinished() {
        performCallback();
	}

	public bool CanUse() {
		return turnsOnCooldown <= 0 && targetPicker.HasValidTarget() && restrictions.All(r => r.CanUse());
	}
}