using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCombatActor : CombatActor {
    [Inject] public PlayerAbilityButtons abilityButtons { private get; set; }
    public List<PlayerAbility> playerAbilities;
    public PlayerAbility debugPlayerAbility;
    System.Action callback;

    public void Setup()
    {
        abilityButtons.Setup(playerAbilities, AbilityPicked);
        abilityButtons.HideButtons();
    }

    void AbilityPicked(PlayerAbility ability)
    {
        ability.Activate(callback);
        abilityButtons.HideButtons();
    }

    public void Act(System.Action callback)
    {
        this.callback = callback;
        PickAbility();
    }

    void PickAbility()
    {
        abilityButtons.ShowButtons();
    }
}
