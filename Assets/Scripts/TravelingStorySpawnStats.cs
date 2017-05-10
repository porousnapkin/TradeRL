using UnityEngine;

public class TravelingStorySpawnStats : ScriptableObject
{
    public static TravelingStorySpawnStats Instance
    {
        get
        {
            return Resources.Load("TravelingStorySpawnStats") as TravelingStorySpawnStats;
        }
    }

    public int maxSpawnRange = 10;
    public int minSpawnRange = 5;
    public int maxAliveDistance = 20;
    public int maxSpawned = 7;
    public float baseChanceToSpawn = 0.2f;
}
