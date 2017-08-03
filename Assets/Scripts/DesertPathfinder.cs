using System.Collections.Generic;
using UnityEngine;
using System;

public class DesertPathfinder {
	public const string COMBAT = "Combat";
	public const string MAP = "Map";

	int[,] mainMapWeights;
	List<Vector2> occupiedLocations = new List<Vector2>();
	List<Vector2> eventedLocations = new List<Vector2>();
	const int occupiedWeight = 50;
    const int eventedLocationWeight = 3;

	public void SetMainMapWeights(int[,] mainMapWeights) {
		this.mainMapWeights = mainMapWeights;
	}

	public List<Vector2> SearchForPathOnMainMap(Vector2 startPos, Vector2 endPos) {
		return SearchForPath(startPos, endPos, mainMapWeights);
	}

	public void LocationOccupied(Vector2 location) {
		occupiedLocations.Add(location);
	}

	public void LocationVacated(Vector2 location) {
		occupiedLocations.Remove(location);
	}

    public void LocationGainedEvent(Vector2 location)
    {
        eventedLocations.Add(location);
    }

    public void LocationLostEvent(Vector2 location)
    {
        eventedLocations.Remove(location);
    }

	List<Vector2> SearchForPath(Vector2 startPos, Vector2 endPos, int[,] mapWeights) {			
		int[,] newWeights = (int[,])mapWeights.Clone();
        foreach(var loc in eventedLocations)
			newWeights[(int)loc.x, (int)loc.y] = eventedLocationWeight;

		foreach(var loc in occupiedLocations)
			newWeights[(int)loc.x, (int)loc.y] = occupiedWeight;

		SearchPoint start = new SearchPoint((int)startPos.x, (int)startPos.y);
		SearchPoint end = new SearchPoint((int)endPos.x, (int)endPos.y);
		List<SearchPoint> path = Pathfinding.CreateConnectingPath(start, end, newWeights, true, 10000);
		
		List<Vector2> retVal = new List<Vector2>();
		foreach(SearchPoint sp in path)
			retVal.Add(sp.position);
		
		return retVal;
	}
}