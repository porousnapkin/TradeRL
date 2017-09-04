using UnityEngine;

public class CellularAutomataDefault : CellularAutomata
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
	
	public CellularAutomataDefault(int width, int height, int numTrueAdjacentForTrue = 4, int numFalseAdjacentForFalse = 5)
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
	
	void CellularAutomataPass()
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
}
