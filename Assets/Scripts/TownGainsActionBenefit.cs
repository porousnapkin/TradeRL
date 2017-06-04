public class TownGainsActionBenefit : TownBenefit
{
    public Town town { private get; set; }
    public CityActionData action { private get; set; }

    public void Apply()
    {
        town.playerActions.AddAction(action);
    }

    public void Undo()
    {
        town.playerActions.RemoveAction(action);
    }
}