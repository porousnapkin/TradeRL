using UnityEngine;
using System.Collections.Generic;

public class Towns {
	[Inject] public ExpeditionFactory expeditionFactory {private get; set; }
	[Inject] public CityActionFactory cityActionFactory {private get; set;}
	[Inject] public MapGraph mapGraph {private get; set; }
    [Inject] public Inventory inventory { private get; set;  }

	public List<Town> TownList { get { return new List<Town>(towns); }}
	List<Town> towns = new List<Town>();
	public List<Town> CityList { get { return new List<Town>(towns); }}
	List<Town> cities = new List<Town>();
	public List<Town> Everything { 
		get {
			List<Town> retVal = new List<Town>();
			retVal.AddRange(towns);
			retVal.AddRange(cities);
			return retVal;
		}
	}
	List<Town> knownLocations = new List<Town>();
	public List<Town> KnownLocations { get { return new List<Town>(knownLocations); }}
	const int rumoredTownsPerCity = 3;

	public void AddTown(Vector2 location, string name) {
        var townData = Resources.Load("Towns/Tahsis") as TownData;
        var t = townData.Create(location);
		towns.Add(t);
	}

	public void AddCity(Vector2 location, string name) {
        var townData = Resources.Load("Towns/Tahsis") as TownData;
        var t = townData.Create(location);
        cities.Add(t);
	}

	List<Town> GetRumoredTowns(Town baseTown) {
		var locations = Everything;
		locations.Remove(baseTown);

		List<Town> retVal = new List<Town>();
		for(int i = 0; i < rumoredTownsPerCity; i++) {
			var randomIndex = Random.Range(0, locations.Count);
			retVal.Add(locations[randomIndex]);
			locations.RemoveAt(randomIndex);
		}

		return retVal;
	}

	public Town GetTown(string name) {
		return Everything.Find(t => t.name == name);	
	}

	public Town GetTown(Vector2 location) {
		return Everything.Find(t => t.worldPosition == location);	
	}

	public List<Town> GetTownsSortedByDistanceFromPoint(Vector2 point) {
		List<Town> sortedTownsAndCities = new List<Town>();
		sortedTownsAndCities.AddRange(towns);
		sortedTownsAndCities.AddRange(cities);
		sortedTownsAndCities.Sort((first, second) => Mathf.RoundToInt((Vector2.Distance(point, first.worldPosition) - 
			Vector2.Distance(point, second.worldPosition)) * 100));
		return sortedTownsAndCities;
	}

    public Town GetTownClosestToCenter()
    {
        var sorted = GetTownsSortedByDistanceFromPoint(new Vector2(mapGraph.Width/2, mapGraph.Height/2));
        return sorted[0];
    }

	public Town GetTownFurthestFromCities() {
		var sortedTowns = new List<Town>(towns);	
		sortedTowns.Sort(SortTownsBasedOnDistanceFromCities);
		return sortedTowns[0];
	}

	int SortTownsBasedOnDistanceFromCities(Town first, Town second) {
		return GetDistanceToClosestCity(second) - GetDistanceToClosestCity(first);
	}

	int GetDistanceToClosestCity(Town t) {
		float closestDistance = 1000;
		for(int i = 0; i < cities.Count; i++) {
			var closeness = Vector2.Distance(cities[i].worldPosition, t.worldPosition);
			if(closeness < closestDistance)
				closestDistance = closeness;
		}

		return Mathf.RoundToInt(closestDistance * 100);
	}

	public void SetupTownEvents() {
		foreach(var town in towns) {
			var temp = town;
			mapGraph.SetEventForLocation((int)town.worldPosition.x, (int)town.worldPosition.y, (finished) => StartTown(temp, finished));
		}
		foreach(var town in cities) {
			var temp = town;
			mapGraph.SetEventForLocation((int)town.worldPosition.x, (int)town.worldPosition.y, (finished) => StartTown(temp, finished));
		}
	}

	public void StartTown(Town t, System.Action finished) {
		var cityDisplayGO = cityActionFactory.CreateDisplayForCity(t);
		expeditionFactory.FinishExpedition();
		finished();
	}

	public void DiscoverLocation(Town t) {
		if(!knownLocations.Contains(t)) {
			knownLocations.Add (t);
			GlobalEvents.TownDiscovered(t);
		}
	}

	public bool CheckIfPositionHasTown(Vector2 position) {
		foreach(var t in towns)
			if(t.worldPosition == position)
				return true;
		foreach(var t in cities)
			if(t.worldPosition == position)
				return true;
		return false;
	}
}