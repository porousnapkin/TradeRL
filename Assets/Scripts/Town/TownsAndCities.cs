using UnityEngine;
using System.Collections.Generic;

public class TownsAndCities {
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
		var t = new Town();
		t.worldPosition = location;
		t.name = name;
		SetupBasics(t);
		towns.Add(t);
	}

	public void AddCity(Vector2 location, string name) {
		var t = new Town();
		t.worldPosition = location;
		t.name = name;
		SetupBasics(t);
		cities.Add(t);
	}

	public void Setup (GameDate gameDate) {
		foreach(var t in towns)
			SetupTown(t, gameDate, false);
		foreach(var c in cities)
			SetupTown(c, gameDate, true);
	}

	void SetupTown(Town t, GameDate date, bool isCity) {
		t.Setup(date, isCity);
		t.rumoredLocations =  GetRumoredTowns(t);

		GlobalEvents.TownLeveldUpEvent += TownLeveled;
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

	void SetupBasics(Town t) {
		t.AddCityAction(CityAction.Center);
		t.AddCityAction(CityAction.Pub);
		t.AddCityAction(CityAction.Market);
		t.AddCityAction(CityAction.Travel);
	}

	void TownLeveled(Town t) {
		if(t.EconomicLevel == 2) {
			t.AddPossibleBuliding(BuildingFactory.CreateTradePost(t));
			GlobalEvents.TownLeveldUpEvent -= TownLeveled;
		}
	}

	public Town GetTown(string name) {
		return towns.Find(t => t.name == name);	
	}

	public Town GetTown(Vector2 location) {
		return towns.Find(t => t.worldPosition == location);	
	}

	public Town GetCity(string name) {
		return cities.Find(t => t.name == name);	
	}

	public Town GetCity(Vector2 location) {
		return cities.Find(t => t.worldPosition == location);	
	}

	public Town GetRandomTown() {
		return towns[Random.Range(0, towns.Count)];
	}

	public Town GetRandomCity() {
		return cities[Random.Range(0, cities.Count)];
	}

	public List<Town> GetTownsAndCitiesSortedByDistanceFromPoint(Vector2 point) {
		List<Town> sortedTownsAndCities = new List<Town>();
		sortedTownsAndCities.AddRange(towns);
		sortedTownsAndCities.AddRange(cities);
		sortedTownsAndCities.Sort((first, second) => Mathf.RoundToInt((Vector2.Distance(point, first.worldPosition) - 
			Vector2.Distance(point, second.worldPosition)) * 100));
		return sortedTownsAndCities;

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

	public void SetupCityAndTownEvents(MapGraph mapGraph, Transform canvasParent) {
		foreach(var town in towns) {
			var temp = town;
			mapGraph.SetEventForLocation((int)town.worldPosition.x, (int)town.worldPosition.y, (finished) => StartTown(temp, finished, canvasParent), false);
		}
		foreach(var town in cities) {
			var temp = town;
			mapGraph.SetEventForLocation((int)town.worldPosition.x, (int)town.worldPosition.y, (finished) => StartTown(temp, finished, canvasParent), false);
		}
	}

	public void StartTown(Town t, System.Action finished, Transform canvasParent) {
		var cityDisplayGO = CityActionFactory.CreateDisplayForCity(t);
		cityDisplayGO.transform.SetParent(canvasParent, false);
		ExpeditionFactory.FinishExpedition();
		finished();
	}

	public void DiscoverLocation(Town t) {
		if(!knownLocations.Contains(t))
			knownLocations.Add (t);
	}
}