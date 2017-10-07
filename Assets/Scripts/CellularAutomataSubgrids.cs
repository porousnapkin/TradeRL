using UnityEngine;

public class CellularAutomataSubGrids : CellularAutomata {
    int subGridSize;
    CellularAutomata[,] subGrids;
    int numSubGridsWide;
    int numSubGridsHigh;
	bool[,] finalGraph;
    public bool[,] Graph
    {
        get
        {
            return finalGraph;
        }
        set
        {
            finalGraph = value;
        }
    }

    public CellularAutomataSubGrids(int width, int height, int subGridSize, int numTrueAdjacentForTrue = 4, int numFalseAdjacentForFalse = 5)
    {
        finalGraph = new bool[width, height];
        this.subGridSize = subGridSize;
        numSubGridsWide = Mathf.CeilToInt(width / (float)subGridSize);
        numSubGridsHigh = Mathf.CeilToInt(height / (float)subGridSize);

        subGrids = new CellularAutomata[numSubGridsWide, numSubGridsHigh];
        for (int subX = 0; subX < numSubGridsWide; subX++)
        {
            for (int subY = 0; subY < numSubGridsHigh; subY++)
            {
                var gridWidth = subGridSize;
                var gridHeight = subGridSize;
                if (subX == numSubGridsWide - 1 && width % subGridSize != 0)
                    gridWidth = width % subGridSize;
                if (subY == numSubGridsHigh - 1 && height % subGridSize != 0)
                    gridHeight = height % subGridSize;
                subGrids[subX, subY] = new CellularAutomataDefault(gridWidth, gridHeight, numTrueAdjacentForTrue, numFalseAdjacentForFalse);
            }
        }
    }

    public bool[,] BuildRandomCellularAutomataSet(int numCellularAutomataRuns, float seedChanceForTrue)
    {
        for (int subX = 0; subX < numSubGridsWide; subX++)
        {
            for (int subY = 0; subY < numSubGridsHigh; subY++)
            {
                var subGraph = subGrids[subX, subY].BuildRandomCellularAutomataSet(numCellularAutomataRuns, seedChanceForTrue);
                for(int innerX = 0; innerX < subGraph.GetLength(0); innerX++)
                {
                    for(int innerY = 0; innerY < subGraph.GetLength(1); innerY++)
                    {
                        int x = subX * subGridSize + innerX;
                        int y = subY * subGridSize + innerY;
                        finalGraph[x,y] = subGraph[innerX, innerY];
                    }
                }
            }
        }

        return finalGraph;
    }
}
