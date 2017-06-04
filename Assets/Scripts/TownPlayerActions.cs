using System.Collections.Generic;

public class TownPlayerActions
{
	public List<CityActionData> cityActions = new List<CityActionData>();
    Town town;

	public event System.Action<Town> cityActionAddedEvent = delegate{};
	public event System.Action<Town> cityActionRemovedEvent = delegate{};
    
    public void Setup(Town town)
    {
        this.town = town;
    }

	public void AddAction(CityActionData ca) {
		cityActions.Add (ca);
		cityActionAddedEvent(town);
	}

    public void RemoveAction(CityActionData ca)
    {
        cityActions.Remove(ca);
        cityActionRemovedEvent(town);
    }
}
