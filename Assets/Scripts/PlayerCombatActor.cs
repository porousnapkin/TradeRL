using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatActor : CombatActor {
	[Inject] public PlayerAbilityButtons abilityButtons { private get; set; }
	public ActivePlayerAbilityModifiers abilityModifiers { private get; set; }
    public List<PlayerAbility> playerAbilities;
    System.Action callback;
	Character owner;

    public void Setup()
    {
        abilityButtons.Setup(playerAbilities, AbilityPicked);
        abilityButtons.HideButtons();
		abilityModifiers.Setup();
    }

    void AbilityPicked(PlayerAbility ability)
    {
		abilityModifiers.SetupForAbility(ability);
		ability.Activate(AbilityFinished);

		abilityButtons.HideButtons();
		abilityModifiers.HideButtons();
    }

	void AbilityFinished() {
		abilityModifiers.Cleanup();
		callback();
	}

    public void Act(System.Action callback)
    {
        this.callback = callback;
        abilityButtons.ShowButtons();
		abilityModifiers.SetupForTurn();
    }
}
