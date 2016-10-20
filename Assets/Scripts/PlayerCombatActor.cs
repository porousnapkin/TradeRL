public class PlayerCombatActor : CombatActor {
	[Inject] public PlayerAbilityButtons abilityButtons { private get; set; }
	[Inject] public PlayerCharacter playerCharacter { private get; set; }
	public ActivePlayerAbilityModifiers abilityModifiers { private get; set; }
    public CombatController controller { private get; set; }
    System.Action callback;

    public void Setup()
    {
        playerCharacter.abilitiesChanged += AbilitiesChanged;

        abilityButtons.Setup(playerCharacter.GetCombatAbilities(controller), AbilityPicked);
        abilityButtons.HideButtons();
		abilityModifiers.Setup();
    }

    public void Cleanup()
    {
        playerCharacter.abilitiesChanged -= AbilitiesChanged;
    }

    private void AbilitiesChanged()
    {
        abilityButtons.Setup(playerCharacter.GetCombatAbilities(controller), AbilityPicked);
    }

    void AbilityPicked(PlayerAbility ability)
    {
		abilityModifiers.SetupForAbility(ability);
		ability.Activate(AbilityFinished);

		abilityButtons.HideButtons();
		abilityModifiers.HideButtons();
    }

	void AbilityFinished() {
		abilityModifiers.Finish();
		callback();
	}

    public void Act(System.Action callback)
    {
        this.callback = callback;
        abilityButtons.ShowButtons();
		abilityModifiers.SetupForTurn();
    }
}
