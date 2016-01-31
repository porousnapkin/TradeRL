using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;

public class LocationFactory {
	[Inject] public MapData mapData { private get; set; }
	[Inject] public TownsAndCities towns { private get; set; }

	List<Vector2> positions = new List<Vector2>();
	List<Location> locations = new List<Location>();
	const int numLocations = 60;
	
	public void CreateLocations() {
		positions.Clear();
		var locationDatas = Resources.LoadAll<LocationData> ("Locations");

		for(int i = 0; i < numLocations; i++)
			SetupLocation(locationDatas[Random.Range(0, locationDatas.Length)]);
	}

	void SetupLocation(LocationData loc) {
		var pos = GetAvailablePosition();

		var l = DesertContext.StrangeNew<Location>();
		l.x = (int)pos.x;
		l.y = (int)pos.y;
		l.data = loc;
		l.Setup();
		locations.Add(l);
	}

	Vector2 GetAvailablePosition() {
		var randomPos = new Vector2(Random.Range(0, mapData.Width), Random.Range(0, mapData.Height));

		if(towns.CheckIfPositionHasTown(randomPos))
			return GetAvailablePosition();
		if(positions.Contains(randomPos))
			return GetAvailablePosition();

		positions.Add (randomPos);
		return randomPos;
	}
}
