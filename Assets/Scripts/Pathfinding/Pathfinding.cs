using UnityEngine;
using System.Collections.Generic;

public static class Pathfinding
{	
	public static List<SearchPoint> CreateConnectingPath(SearchPoint start, SearchPoint end, int[,] weights, bool canDiagonal = true, int maxSearchDepth = 10000)
	{
		//Use A* to look for a path real quick
		List<SearchPoint> openList = new List<SearchPoint>();
		
		Dictionary<Vector2, SearchPoint> closedList = new Dictionary<Vector2, SearchPoint>();
	
		SearchPoint curTile = new SearchPoint((int)start.position.x, (int)start.position.y);
		curTile.heuristic = (float)ManhattanDistanceHeuristic(start, end);
		curTile.depth = 0;
		curTile.previousPosition = start.position;
		curTile.weight = weights[(int)start.position.x, (int)start.position.y];
		openList.Add(curTile);
		
		int depthSearched = -1;
	
		//We will return out of this loop when we hit the right place
		while(openList.Count != 0)
		{
			//Get the next node from the open list for the next search
			curTile = openList[0];
			openList.RemoveAt(0);
			closedList.Add(curTile.position, curTile);
			
			if(curTile.position == end.position)
			{
				//Success!
				List<SearchPoint> output = new List<SearchPoint>();
				CreatePath(curTile, ref closedList, ref output);
				return output;
			}
			
			depthSearched++;
			if(maxSearchDepth > 0 && depthSearched > maxSearchDepth)
			{
				Debug.LogWarning("Max search depth reached");
				return new List<SearchPoint>();
			}
	
			//Add all adjacent squares to the open list
			for(int i = -1; i <= 1; i++)
			{
				for(int j = -1; j <= 1; j++)
				{	
					//ignore the space we're searching from
					if(i == 0 && j == 0)
						continue;
					
					//if we're looking at a diagonal skip it if we're ignoring them...
					if(!canDiagonal && i != 0 && j != 0)
						continue;
					
					SearchPoint newPoint = new SearchPoint((int)curTile.position.x + i, (int)curTile.position.y + j);
					newPoint.previousPosition = curTile.position;
	
					if( newPoint.position.x >= 0 && newPoint.position.x < weights.GetLength(0) &&
						newPoint.position.y >= 0 && newPoint.position.y < weights.GetLength(1) &&
						curTile.depth < maxSearchDepth )
					{						
						//Create this search node
						newPoint.heuristic = (float)ManhattanDistanceHeuristic(newPoint, end);
						newPoint.weight = weights[(int)newPoint.position.x, (int)newPoint.position.y];
						if(newPoint.weight == SearchPoint.kImpassableWeight)
							continue;
						
						newPoint.depth = curTile.depth + newPoint.weight;
						
						//if we're looking at a diagonal make it cost a bit more to move there.
						if(i != 0 && j != 0)
							newPoint.depth += 0.2f;
						
						//Check if this tile is in the closed list
						if(closedList.ContainsKey(newPoint.position))
						{							
							//Check to see if this is a faster route to the closed list point.
							SearchPoint prevClosedListPoint = closedList[newPoint.position];
							if(prevClosedListPoint.depth > newPoint.depth)
								closedList[newPoint.position] = newPoint;
							
							continue;
						}
	
						//Add it to the open list
						//Check if it is in the open list first
						bool addToList = true;
						foreach(SearchPoint sp in openList)
						{
							if(newPoint.position.x == sp.position.x && newPoint.position.y == sp.position.y)
							{
								//It's in the list... but if this still has a better heuristic, we need to remove the old one and add this
								if(newPoint.depth < sp.depth)
									openList.Remove(sp);
								else
									addToList = false;
								break;
							}
						}
						if(addToList)
							openList.Add(newPoint);
					}
				}
			}
			
			openList.Sort(CompareSearchPointsByPathShortness);
		}
		
		return new List<SearchPoint>();
	}
	
	static float ManhattanDistanceHeuristic(SearchPoint start, SearchPoint end)
	{
		return (Mathf.Abs(start.position.x - end.position.x) + Mathf.Abs(start.position.y - end.position.y));
	}
	
	static void CreatePath( SearchPoint pathEnd, ref Dictionary<Vector2, SearchPoint> fastestToPoints, ref List<SearchPoint> pathOut )
	{
		if(pathEnd.previousPosition != pathEnd.position)
			CreatePath(fastestToPoints[pathEnd.previousPosition], ref fastestToPoints, ref pathOut);
	
		pathOut.Add(pathEnd);
	}
	
	static int CompareSearchPointsByPathShortness(SearchPoint a, SearchPoint b)
    {
		float aTotal = a.heuristic + a.depth;
		float bTotal = b.heuristic + b.depth;
		
		return (int)(aTotal - bTotal);
	}
}
