using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TravelingStorySpawner {
	[Inject] public GameDate gameDate {private get;set;}	
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject] public MapData mapData {private get; set;}
    [Inject] public TravelingStoryFactory travelingStoryFactory { private get; set; }
	int spawnRange = 2;
	List<TravelingStoryData> travelingStories;
	List<TravelingStory> activeStories = new List<TravelingStory>();
	List<Vector2> baseSetOfSpawnLocations = new List<Vector2>();
	int numToSpawn = 130;

	[PostConstruct]
	public void PostConstruct() {
		travelingStories = Resources.LoadAll<TravelingStoryData>("TravelingStory").ToList();
        travelingStories.RemoveAll(t => !t.use);
	}

	public void Setup() {
		for(int x = 0; x < mapData.Width; x++)
			for(int y = 0; y < mapData.Height; y++)
				if(!mapData.IsHill(new Vector2(x, y)) && !mapData.IsCity(new Vector2(x, y)))
					baseSetOfSpawnLocations.Add(new Vector2(x, y));
	}

	public void SpawnTravelingStories() {
		List<Vector2> spawnLocations = new List<Vector2>(baseSetOfSpawnLocations);

		for(int i = 0; i < numToSpawn; i++) {
			var data = GetDataToSpawn();
			var position = GetPositionToSpawn(spawnLocations);

			activeStories.Add(travelingStoryFactory.Create(position, data));
		}
	}

	public void ClearSpawns() {
		foreach(var story in activeStories)
			story.Remove();

		activeStories.Clear();
	}

	TravelingStoryData GetDataToSpawn() {
		var possibleStory = travelingStories[Random.Range(0, travelingStories.Count)];	
        if(Random.value < possibleStory.rarityDiscardChance)
            return possibleStory;
        else
            return GetDataToSpawn();
	}

	Vector2 GetPositionToSpawn(List<Vector2> spawnLocations) {
		var index = Random.Range(0, spawnLocations.Count);
		var pos = spawnLocations[index];
		spawnLocations.RemoveAt(index);

		bool isViable = true;
		foreach(var s in activeStories)
			if(Vector2.Distance(s.WorldPosition, pos) < spawnRange)
				isViable = false;

		if(isViable)
			return pos;
		else
			return GetPositionToSpawn(spawnLocations);
	}
}