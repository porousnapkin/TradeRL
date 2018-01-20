using System;
using System.Collections.Generic;

public class ChangeInitiativeAbility : AbilityActivator
{
    public enum TurnsAffected
    {
        ThisTurn,
        WholeCombat,
    }

    [Inject]public GlobalTextArea textArea { private get; set; }
    public int initiativeModifier { private get; set; }
    public string initiativeSource { private get; set; }
    public TurnsAffected turnsAffected { private get; set; }

    public void PrepareActivation(List<Character> targets, TargetedAnimation animation, Action preparedCallback)
    {
        preparedCallback();
    }

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        targets.ForEach(t =>
        {
            var initMod = new CombatController.InitiativeModifier();
            initMod.amount = initiativeModifier;
            initMod.description = initiativeSource;
            initMod.removeAtTurnEnd = turnsAffected == TurnsAffected.ThisTurn;
            t.controller.AddInitiativeModifier(initMod);

            textArea.AddLine(AbilityInitiativeModifier.GetInitiativeModifierString(initiativeModifier, t, initiativeSource));
        });

        finishedAbility();
    }
}