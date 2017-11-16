using System;

public class TownGainsActionBenefit : TownBenefit
{
    [Inject] public TownEventLog eventLog {private get; set;}
    public Town town { private get; set; }
    public CityActionData action { private get; set; }

    public void Apply()
    {
        town.playerActions.AddAction(action);

        eventLog.AddTextEvent("Gained the " + action.actionDescription + " action", action.actionDescription);
    }

    public void Undo()
    {
        town.playerActions.RemoveAction(action);

        eventLog.AddTextEvent("Lost the " + action.actionDescription + " action", action.actionDescription);
    }
}

