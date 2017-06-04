using System.Collections.Generic;
using UnityEngine;

public class TownPlayerBuildings
{
	public List<Building> unbuiltBuilding = new List<Building>();
	public List<Building> builtBuilding = new List<Building>();
    Town town;

	public event System.Action<Town, Building> possibleBuildingGainedEvent = delegate{};

    public void Setup(Town town)
    {
        this.town = town;
    }

    public void AddPossibleBuliding(Building b) {
		unbuiltBuilding.Add(b);
		possibleBuildingGainedEvent(town, b);
		CheckForBuildingScene();
	}

	void CheckForBuildingScene() {
        //TODO: this is so hacky. Is it necessary?
		if(town.playerActions.cityActions.Find(a => a.name == "Building") == null &&
		   (unbuiltBuilding.Count > 0 || 
		 	builtBuilding.Count > 0))
			town.playerActions.AddAction(Resources.Load ("CityActions/Building") as CityActionData);
	}
	
	public void BuildBuilding(Building b) {
		unbuiltBuilding.Remove (b);
		builtBuilding.Add (b);
		CheckForBuildingScene();
	}
}
