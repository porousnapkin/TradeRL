using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TravelingStorySpawner {
	[Inject] public GameDate gameDate {private get;set;}	
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject] public MapData mapData {private get; set;}
	bool isSpawning = false;
	float spawnChance = 0.10f;
	int minDistanceToSpawn = 3;
	int spawnRange = 2;
	int spawnCooldown = 5;
	int cooldownTimer = 0;
	List<TravelingStoryData> travelingStories;
	List<TravelingStory> activeStories = new List<TravelingStory>();
	List<Vector2> baseSetOfSpawnLocations = new List<Vector2>();
	int numToSpawn = 200;

	[PostConstruct]
	public void PostConstruct() {
		travelingStories = Resources.LoadAll<TravelingStoryData>("TravelingStory").ToList();
	}

	public void Setup() {
		for(int x = 0; x < mapData.Width; x++)
			for(int y = 0; y < mapData.Height; y++)
				if(!mapData.IsHill(new Vector2(x, y)) || !mapData.IsCity(new Vector2(x, y)))
					baseSetOfSpawnLocations.Add(new Vector2(x, y));
	}

	public void SpawnTravelingStories() {
		Debug.Log("Setup");
		List<Vector2> spawnLocations = new List<Vector2>(baseSetOfSpawnLocations);

		for(int i = 0; i < numToSpawn; i++) {
			var data = GetDataToSpawn();
			var position = GetPositionToSpawn(spawnLocations);

			activeStories.Add(data.Create(position));
			textArea.AddLine(data.spawnMessage);
		}
	}

	public void ClearSpawns() {
		foreach(var story in activeStories)
			story.Remove();

		activeStories.Clear();
	}

	TravelingStoryData GetDataToSpawn() {
		return travelingStories[Random.Range(0, travelingStories.Count)];	
	}

	Vector2 GetPositionToSpawn(List<Vector2> spawnLocations) {
		var pos = spawnLocations[Random.Range(0, spawnLocations.Count)];

		for(int x = -spawnRange; x < spawnRange; x++)
			for(int y = -spawnRange; y < spawnRange; y++)
				spawnLocations.Remove(pos + new Vector2(x, y));

		return pos;
	}
}