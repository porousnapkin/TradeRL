using System.Collections.Generic;
using UnityEngine;

public class DesertPathfinder {
	int[,] _mainMapWeights;

	public void SetMainMapWeights(int[,] mainMapWeights) {
		_mainMapWeights = mainMapWeights;
	}

	public List<Vector2> SearchForPathOnMainMap(Vector2 startPos, Vector2 endPos) {
		return SearchForPath(startPos, endPos, _mainMapWeights);
	}

	List<Vector2> SearchForPath(Vector2 startPos, Vector2 endPos, int[,] mapWeights)
	{		
		SearchPoint start = new SearchPoint((int)startPos.x, (int)startPos.y);
		SearchPoint end = new SearchPoint((int)endPos.x, (int)endPos.y);
		List<SearchPoint> path = Pathfinding.CreateConnectingPath(start, end, mapWeights, true);
		
		List<Vector2> retVal = new List<Vector2>();
		foreach(SearchPoint sp in path)
			retVal.Add(sp.position);
		
		return retVal;
	}
}