using System.Collections.Generic;
using UnityEngine;
using System;

public class DesertPathfinder {
	int[,] mainMapWeights;
	List<Vector2> occupiedLocations = new List<Vector2>();
	const int occupiedWeight = 50;

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

	List<Vector2> SearchForPath(Vector2 startPos, Vector2 endPos, int[,] mapWeights) {		
		int[,] newWeights = (int[,])mapWeights.Clone();
		foreach(var loc in occupiedLocations)
			newWeights[(int)loc.x, (int)loc.y] = occupiedWeight;

		SearchPoint start = new SearchPoint((int)startPos.x, (int)startPos.y);
		SearchPoint end = new SearchPoint((int)endPos.x, (int)endPos.y);
		List<SearchPoint> path = Pathfinding.CreateConnectingPath(start, end, newWeights, true);
		
		List<Vector2> retVal = new List<Vector2>();
		foreach(SearchPoint sp in path)
			retVal.Add(sp.position);
		
		return retVal;
	}

#warning "this whole thing should be in combat pathfinder."
	/*public Vector2 FindAdjacentPointMovingFromDirection(Vector2 startPos, Vector2 target, CombatGraph combatGraph) {
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
			if(Grid.IsValidPosition((int)check.x, (int)check.y) && _mainMapWeights[(int)check.x, (int)check.y] > -1 &&
				!combatGraph.IsPositionOccupied((int)check.x , (int)check.y))
				return check;
		}

		return Vector2.zero;
	}*/
}