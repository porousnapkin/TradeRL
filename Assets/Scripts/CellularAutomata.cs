public interface CellularAutomata
{
    bool[,] Graph { get; }
    bool[,] BuildRandomCellularAutomataSet(int numCellularAutomataRuns, float seedChanceForTrue);
}