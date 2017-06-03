using System.Collections.Generic;

public class TownPlayerActions
{
	public List<CityActionData> cityActions = new List<CityActionData>();
    Town town;

	public event System.Action<Town, CityActionData> cityActionAddedEvent = delegate{};
    
    public void Setup(Town town)
    {
        this.town = town;
    }

	public void AddCityAction(CityActionData ca) {
		cityActions.Add (ca);
		cityActionAddedEvent(town, ca);
	}
}
