using UnityEngine;
using System.Collections.Generic;

public class TownsAndCities {
	public List<Town> TownList { get { return new List<Town>(towns); }}
	List<Town> towns = new List<Town>();
	public List<Town> CityList { get { return new List<Town>(towns); }}
	List<Town> cities = new List<Town>();

	public void AddTown(Vector2 location, string name) {
		var t = new Town();
		t.worldPosition = location;
		t.name = name;
		towns.Add(t);
	}

	public void AddCity(Vector2 location, string name) {
		var t = new Town();
		t.worldPosition = location;
		t.name = name;
		cities.Add(t);
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
}