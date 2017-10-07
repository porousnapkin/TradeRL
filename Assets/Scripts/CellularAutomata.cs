public interface CellularAutomata
{
    bool[,] Graph { get; set;  }
    bool[,] BuildRandomCellularAutomataSet(int numCellularAutomataRuns, float seedChanceForTrue);
}