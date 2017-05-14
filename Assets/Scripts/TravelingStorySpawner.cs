using UnityEngine;
using System.Collections.Generic;
using System;

public class TravelingStorySpawner {
	[Inject] public GameDate gameDate {private get;set;}	
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject] public MapData mapData {private get; set;}
    [Inject] public TravelingStoryFactory travelingStoryFactory { private get; set; }
    [Inject] public GameDate date { private get; set; }
	int spawnRange = 2;
	List<TravelingStory> activeStories = new List<TravelingStory>();
    TravelingStorySpawnStats stats;

    [PostConstruct]
	public void PostConstruct() {
        stats = TravelingStorySpawnStats.Instance;
        GlobalEvents.ExpeditionBegan += ExpeditionBegan;
        GlobalEvents.ExpeditionEnded += ExpeditionEnded;
	}

    void ExpeditionEnded()
    {
        date.DaysPassedEvent -= DayEnded;
    }

    void ExpeditionBegan()
    {
        date.DaysPassedEvent += DayEnded;
    }

    void DayEnded(int day)
    {
        for (int i = 0; i < day; i++)
            CheckForSpawn();

        CheckForInactiveStories();
    }

    void CheckForInactiveStories()
    {
        List<TravelingStory> storiesToRemove = new List<TravelingStory>();
        activeStories.ForEach(s =>
        {
            if ((s.WorldPosition - mapPlayerController.position).magnitude >= stats.maxAliveDistance)
                storiesToRemove.Add(s);
        });

        storiesToRemove.ForEach(s =>
        {
            s.Remove();
            activeStories.Remove(s);
        });
    }

    void CheckForSpawn()
    {
        if (UnityEngine.Random.value < CalculateChanceToSpawn())
            SpawnAtRandomPosition();
    }

    float CalculateChanceToSpawn()
    {
        var numStories = activeStories.Count;
        var percentOfMaxSpawned = numStories / stats.maxSpawned;
        return stats.baseChanceToSpawn * (1 - percentOfMaxSpawned);
    }

    void SpawnAtRandomPosition()
    {
        var position = GetPositionToSpawn();
        if(position != Vector2.zero)
            activeStories.Add(travelingStoryFactory.Create(position));
    }

	public void ClearSpawns() {
		foreach(var story in activeStories)
			story.Remove();

		activeStories.Clear();
	}

	Vector2 GetPositionToSpawn() {
        var spawnLocations = GetListOfViableSpawnPoints();
        Debug.Log("num spawn locs " + spawnLocations.Count);
        if (spawnLocations.Count > 0)
            return spawnLocations[UnityEngine.Random.Range(0, spawnLocations.Count)];
        return Vector2.zero;
	}

    List<Vector2> GetListOfViableSpawnPoints()
    {
        var basePosition = mapPlayerController.position;
        var maxRange = stats.maxSpawnRange;
        var minRange = stats.minSpawnRange;
        List<Vector2> retVal = new List<Vector2>();

        for (int x = minRange; x <= maxRange; x++)
            for (int y = minRange; y <= maxRange; y++)
                CheckAllVariationsForAdd(basePosition, x, y, retVal);

        return retVal;
    }

    void CheckAllVariationsForAdd(Vector2 basePosition, int x, int y, List<Vector2> retVal)
    {
        CheckForAdd(new Vector2(basePosition.x + x, basePosition.y + y), retVal);
        CheckForAdd(new Vector2(basePosition.x - x, basePosition.y + y), retVal);
        CheckForAdd(new Vector2(basePosition.x - x, basePosition.y - y), retVal);
        CheckForAdd(new Vector2(basePosition.x + x, basePosition.y - y), retVal);
    }

    void CheckForAdd(Vector2 v, List<Vector2> retVal)
    {
        if (v.x < mapData.Width && v.x > 0 &&
            v.y < mapData.Height && v.y > 0 &&
            !mapData.IsCity(v) && !mapData.IsTown(v) &&
            !mapData.IsHill(v) && !IsTooCloseToAnotherSpawningStory(v))
            retVal.Add(v);
    }

    bool IsTooCloseToAnotherSpawningStory(Vector2 v)
    {
        foreach (var s in activeStories)
            if (Vector2.Distance(s.WorldPosition, v) < spawnRange)
                return true;
        return false;
    }
}