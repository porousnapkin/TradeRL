using System;
using System.Collections.Generic;

public class IncreaseFriendsInitiativeAmbush : AmbushActivator
{
    public int initiativeGain { private get; set; }
    public enum TurnsToIncrease
    {
        ThisTurn,
        NextTurn,
        Both
    }
    public TurnsToIncrease toIncrease { private get; set; }

    public void Activate(List<Character> friends, List<Character> foes, Action finished)
    {
        friends.ForEach(f =>
        {
            if (toIncrease == TurnsToIncrease.Both || toIncrease == TurnsToIncrease.ThisTurn)
            {
                var curInitiative = f.controller.GetInitiative(0);
                f.controller.SetInitiative(0, curInitiative + initiativeGain);
            }
            if (toIncrease == TurnsToIncrease.Both || toIncrease == TurnsToIncrease.NextTurn)
            {
                var curInitiative = f.controller.GetInitiative(1);
                f.controller.SetInitiative(1, curInitiative + initiativeGain);
            }
        });

        finished();
    }
}