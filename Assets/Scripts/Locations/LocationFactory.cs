using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocationFactory {
	public static TownsAndCities towns;
	public static MapGraph mapGraph;
	public static MapCreator mapCreator;
	public static TurnManager turnManager;
	static List<Vector2> positions = new List<Vector2>();
	static List<Location> locations = new List<Location>();
	const int numLocations = 60;
	
	public static void CreateLocations() {
		positions.Clear();
		var locationDatas = Resources.LoadAll<LocationData> ("Locations");

		for(int i = 0; i < numLocations; i++)
			SetupLocation(locationDatas[Random.Range(0, locationDatas.Length)]);
	}

	static void SetupLocation(LocationData loc) {
		var pos = GetAvailablePosition();

		var l = new Location();
		l.x = (int)pos.x;
		l.y = (int)pos.y;
		l.turnManager = turnManager;
		l.mapGraph = mapGraph;
		l.mapCreator = mapCreator;
		l.data = loc;
		l.Setup();
		locations.Add(l);
	}

	static Vector2 GetAvailablePosition() {
		var randomPos = new Vector2(Random.Range(0, mapCreator.width), Random.Range(0, mapCreator.height));
		if(towns.CheckIfPositionHasTown(randomPos))
			return GetAvailablePosition();
		if(positions.Contains(randomPos))
			return GetAvailablePosition();

		positions.Add (randomPos);
		return randomPos;
	}
}
