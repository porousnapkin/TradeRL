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

	public Vector2 FindAdjacentPointMovingFromDirection(Vector2 startPos, Vector2 target) {
		List<Vector2> offsetAmounts = new List<Vector2>();
		if(startPos.x != target.x && startPos.y != target.y) {
			offsetAmounts.Add(new Vector2(startPos.x > target.x ? 1 : -1, startPos.y > target.y ? 1 : -1));
			offsetAmounts.Add(new Vector2(0, startPos.y > target.y ? 1 : -1));
			offsetAmounts.Add(new Vector2(startPos.x > target.x ? 1 : -1, 0));
		}
		else {
			if(startPos.x == target.x) {
				offsetAmounts.Add(new Vector2(0, startPos.y > target.y ? 1 : -1));
				offsetAmounts.Add(new Vector2(1, startPos.y > target.y ? 1 : -1));
				offsetAmounts.Add(new Vector2(-1, startPos.y > target.y ? 1 : -1));
			}
			else {
				offsetAmounts.Add(new Vector2(startPos.x > target.x ? 1 : -1, 0));
				offsetAmounts.Add(new Vector2(startPos.x > target.x ? 1 : -1, 1));
				offsetAmounts.Add(new Vector2(startPos.x > target.x ? 1 : -1, -1));
			}
		}

		foreach(var offset in offsetAmounts) {
			Vector2 check = target + offset;
			if(Grid.IsValidPosition((int)check.x, (int)check.y) && _mainMapWeights[(int)check.x, (int)check.y] > -1)
				return check;
		}

		return Vector2.zero;
	}
}