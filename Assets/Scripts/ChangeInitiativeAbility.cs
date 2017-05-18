using System;
using System.Collections.Generic;

public class ChangeInitiativeAbility : AbilityActivator
{
    public enum TurnsAffected
    {
        ThisTurn,
        NextTurn,
        ThisAndNextTurn,
    }

    [Inject]public GlobalTextArea textArea { private get; set; }
    public int initiativeModifier { private get; set; }
    public string initiativeSource { private get; set; }
    public TurnsAffected turnsAffected { private get; set; }
    public bool persisteNewInitiative { private get; set; }

    public void Activate(List<Character> targets, TargetedAnimation animation, Action finishedAbility)
    {
        targets.ForEach(t =>
        {
            if (turnsAffected == TurnsAffected.ThisTurn || turnsAffected == TurnsAffected.ThisAndNextTurn)
            {
                var init = t.controller.GetInitiative(0);
                t.controller.SetInitiative(0, init + initiativeModifier, persisteNewInitiative);
            }
            if (turnsAffected == TurnsAffected.NextTurn || turnsAffected == TurnsAffected.ThisAndNextTurn)
            {
                var init = t.controller.GetInitiative(1);
                t.controller.SetInitiative(1, init + initiativeModifier, persisteNewInitiative);
            }

            textArea.AddLine(AbilityInitiativeModifier.GetInitiativeModifierString(initiativeModifier, t, initiativeSource));
        });

        finishedAbility();
    }
}