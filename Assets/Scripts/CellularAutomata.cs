using UnityEngine;
using System.Collections.Generic;

public class CellularAutomata 
{
	bool[,] _graph;
	delegate void ApplyToSquareDelegate(int x, int y);
	
	const int kTotalAdjacentSquares = 8;
	const int kTotalOrthogonallyAdjacentSquares = 4;
	int _numTrueAdjacentForTrue;
	int _numFalseAdjacentForFalse;

	public bool[,] Graph
	{
		get
		{
			return _graph;
		} 
		set
		{
			_graph = value;
		}
	}
	
	public CellularAutomata(int width, int height, int numTrueAdjacentForTrue = 4, int numFalseAdjacentForFalse = 5)
	{
		_graph = new bool[width, height];
		
		_numTrueAdjacentForTrue = numTrueAdjacentForTrue;
		_numFalseAdjacentForFalse =numFalseAdjacentForFalse;
	}
	
	public bool[,] BuildRandomCellularAutomataSet(int numCellularAutomataRuns, float seedChanceForTrue)
	{
		SeedRandomGraph(seedChanceForTrue);
				
		for(int i = 0; i < numCellularAutomataRuns; i++)
			CellularAutomataPass();
		
		return _graph;
	}
	
	void SeedRandomGraph(float chanceForTrue)
	{
		ForEachSquare((int x, int y) => _graph[x, y] = Random.value < chanceForTrue);
	}
	
	void ForEachSquare(ApplyToSquareDelegate toApply)
	{
		for(int x = 0; x < _graph.GetLength(0); x++)
		{
			for(int y = 0; y < _graph.GetLength(1); y++)
			{
				toApply(x, y);
			}
		}
	}
	
	public void CellularAutomataPass()
	{
		ForEachSquare(ApplyCellularAutomata);
	}
	
	void ApplyCellularAutomata(int x, int y)
	{
		int numAdjacent = GetNumTrueAdjacent(x, y);
		if(!_graph[x, y] && numAdjacent > _numTrueAdjacentForTrue)
			_graph[x, y] = true;
		if(_graph[x, y] && kTotalAdjacentSquares - numAdjacent > _numFalseAdjacentForFalse)
			_graph[x, y] = false;
	}
	
	int GetNumTrueAdjacent(int x, int y)
	{
		int count = 0;
		for(int xAdd = -1; xAdd <= 1; xAdd++)
		{
			for(int yAdd = -1; yAdd <= 1; yAdd++)
			{
				if(xAdd == 0 && yAdd == 0)
					continue;
				if(SafeCheckLocation(x + xAdd, y + yAdd))
					count++;
			}
		}
		
		return count;
	}
	
	bool SafeCheckLocation(int x, int y)
	{
		if( x < 0 || x >= _graph.GetLength(0) ||
			y < 0 || y >= _graph.GetLength(1) )
			return false;
		
		return _graph[x, y];
	}
	
	public bool[,] DestroyEnds(bool[,] graph)
	{
		_graph = graph;
		ForEachSquare(DestroyEnd);
		
		return _graph;
	}
	
	void DestroyEnd(int x, int y)
	{
		int numAdjacent = GetNumTrueOrthogonallyAdjacent(x, y);
		if(kTotalOrthogonallyAdjacentSquares - numAdjacent >= 3)
			_graph[x, y] = false;
		
		if(!SafeCheckLocation(x+1, y) && !SafeCheckLocation(x-1, y))
			_graph[x, y] = false;
		
		if(!SafeCheckLocation(x, y+1) && !SafeCheckLocation(x, y-1))
			_graph[x, y] = false;
		
		if(!SafeCheckLocation(x+1, y+1) && !SafeCheckLocation(x-1, y-1))
			_graph[x, y] = false;
		
		if(!SafeCheckLocation(x-1, y+1) && !SafeCheckLocation(x+1, y-1))
			_graph[x, y] = false;
	}
	
	int GetNumTrueOrthogonallyAdjacent(int x, int y)
	{
		int count = 0;
		if(SafeCheckLocation(x + 1, y))
			count++;
		if(SafeCheckLocation(x - 1, y))
			count++;
		if(SafeCheckLocation(x, y + 1))
			count++;
		if(SafeCheckLocation(x, y - 1))
			count++;
		return count;
	}

	public bool[,] RemoveIslands(bool[,] graph) {
		_graph = graph;	
		List<HashSet<Vector2> > islands = GetIslands();	
		if(islands.Count <= 1)
			return _graph;

		RemoveBiggestIsland(islands);
		ClearRemainingIslands(islands);

		return _graph;
	}

	List<HashSet<Vector2> > GetIslands() {
		List<HashSet<Vector2> > islands = new List<HashSet<Vector2> >();
		ForEachSquare( delegate(int x, int y) { 
				if(_graph[x, y]) {
					bool isInIsland = false;
					foreach(HashSet<Vector2> island in islands) {
						if(island.Contains(new Vector2(x, y))) {
							isInIsland = true;
						}
					}

					if(!isInIsland)
						islands.Add(CreateIslandFromRoot(new Vector2(x, y)));
				}
			});

		return islands;
	}

	HashSet<Vector2> CreateIslandFromRoot(Vector2 root) {
		HashSet<Vector2> island = new HashSet<Vector2>(new VectorComparer());
		RecursivelyAddPositionsToIsland(root, island);

		return island;
	}

	void RecursivelyAddPositionsToIsland(Vector2 position, HashSet<Vector2> island) {
		if(SafeCheckLocation((int)position.x, (int)position.y) && !island.Contains(position)) {
			island.Add(position);

			RecursivelyAddPositionsToIsland(new Vector2(position.x + 1, position.y), island);
			RecursivelyAddPositionsToIsland(new Vector2(position.x, position.y + 1), island);
			RecursivelyAddPositionsToIsland(new Vector2(position.x - 1, position.y), island);
			RecursivelyAddPositionsToIsland(new Vector2(position.x, position.y - 1), island);
		}
	}

	class VectorComparer : IEqualityComparer<Vector2>
	{
		public bool Equals(Vector2 one, Vector2 two)
		{
		    return one.x == two.x && one.y == two.y;
		}

		public int GetHashCode(Vector2 item)
		{
		    return item.GetHashCode();
		}
	}

	void RemoveBiggestIsland(List<HashSet<Vector2> > islands) {
		int biggestIslandSize = 0;
		int biggestIslandIndex = 0;
		for(int i = 0; i < islands.Count; i++) {
			if(islands[i].Count > biggestIslandSize) {
				biggestIslandIndex = i;
				biggestIslandSize = islands[i].Count;
			}
		}

		islands.RemoveAt(biggestIslandIndex);
	}


	void ClearRemainingIslands(List<HashSet<Vector2> > islands) {
		foreach(HashSet<Vector2> island in islands) {
			foreach(Vector2 position in island) {
				_graph[(int)position.x, (int)position.y] = false;
			}
		}	
	}

	public int GetLandmassSize() {
		int landmassSize = 0;
		ForEachSquare(delegate(int x, int y) { 
			landmassSize += _graph[x, y]? 1 : 0; });
		return landmassSize;
	}
}
