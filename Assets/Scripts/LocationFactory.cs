using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.impl;

public class LocationFactory {
	[Inject] public MapData mapData { private get; set; }
	[Inject] public TownsAndCities towns { private get; set; }

	List<Vector2> positions = new List<Vector2>();
	List<Location> locations = new List<Location>();
	const int numLocations = 300;
	
	public void CreateLocations() {
		positions.Clear();
//		var locationDatas = Resources.LoadAll<LocationData> ("Locations");

//		for(int i = 0; i < numLocations; i++)
//			SetupLocation(locationDatas[Random.Range(0, locationDatas.Length)]);

	    for (int i = 0; i < numLocations; i++)
	        SetupLocation(Resources.Load<LocationData>("Locations/Oasis"));
	}

	void SetupLocation(LocationData loc) {
		var pos = GetAvailablePosition();

		CreateLocationAtPosition(loc, pos);
	}

    public void CreateLocationAtPosition(LocationData loc, Vector2 pos)
    {
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
		if(mapData.IsHill(randomPos))
			return GetAvailablePosition();
		if(positions.Contains(randomPos))
			return GetAvailablePosition();

		positions.Add (randomPos);
		//Add adjacent positions as well so we don't place locations next to each other.
		for(int x = -1; x <= 1; x++)
			for(int y = -1; y <= 1; y++)
				if(x != 0 || y != 0)
					positions.Add(randomPos + new Vector2(x, y));

		return randomPos;
	}
}
