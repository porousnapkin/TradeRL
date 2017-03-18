using System;

public class PlayerCombatActor : CombatActor {
	[Inject] public PlayerAbilityButtons abilityButtons { private get; set; }
	[Inject] public PlayerCharacter playerCharacter { private get; set; }
	public ActivePlayerAbilityModifiers abilityModifiers { private get; set; }
    public CombatController controller { private get; set; }
    System.Action callback;
    PlayerAbility activeAbility;

    public void Setup()
    {
        playerCharacter.abilitiesChanged += AbilitiesChanged;

        abilityButtons.Setup(playerCharacter.GetCombatAbilities(controller), AbilityPicked, AbilityUnpicked);
        abilityButtons.HideButtons();
		abilityModifiers.Setup();
    }

    public void Cleanup()
    {
        playerCharacter.abilitiesChanged -= AbilitiesChanged;
    }

    private void AbilitiesChanged()
    {
        abilityButtons.Setup(playerCharacter.GetCombatAbilities(controller), AbilityPicked, AbilityUnpicked);
    }

    void AbilityPicked(PlayerAbility ability)
    {
        activeAbility = ability;
        ability.SelectTargets(TargettingFinished);
    }

    private void AbilityUnpicked(PlayerAbility ability)
    {
        if (ability != null)
            ability.CancelTargetSelection();
    }

    void TargettingFinished()
    {
        activeAbility.SetAbilityModifiers(abilityModifiers);
        activeAbility.Activate(AbilityFinished);

		abilityButtons.HideButtons();
		abilityModifiers.HideButtons();
    }

    void AbilityFinished() {
		callback();
	}

    public void Act(System.Action callback)
    {
        this.callback = callback;
        abilityButtons.FinishedUsingAbility();
		abilityModifiers.SetupForTurn();
    }
}
