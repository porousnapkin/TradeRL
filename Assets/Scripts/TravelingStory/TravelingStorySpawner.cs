using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TravelingStorySpawner {
	[Inject] public GameDate gameDate {private get;set;}	
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	bool isSpawning = false;
	float spawnChance = 0.10f;
	int minDistanceToSpawn = 3;
	int spawnRange = 2;
	int spawnCooldown = 5;
	int cooldownTimer = 0;
	List<TravelingStoryData> travelingStories;
	List<TravelingStory> activeStories = new List<TravelingStory>();

	[PostConstruct]
	public void PostConstruct() {
		gameDate.DaysPassedEvent += CheckForSpawn;

		travelingStories = Resources.LoadAll<TravelingStoryData>("TravelingStory").ToList();
	}

	public void BeginSpawning() {
		isSpawning = true;
	}

	public void StopSpawning() {
		isSpawning = false;
	}

	public void ClearSpawns() {
		foreach(var story in activeStories)
			story.Remove();

		activeStories.Clear();
	}

	void CheckForSpawn(int daysPassed) {
		if( !isSpawning ||
			gameDate.HasADailyEventOccuredToday)
			return;

		cooldownTimer--;
		if(cooldownTimer > 0)
			return;

		bool didSpawn = false;
		for(int i = 0; i < daysPassed; i++)
			if(Random.value < spawnChance)
				didSpawn = true;

		if(didSpawn)
			SpawnNewTravelingStory();
	}

	void SpawnNewTravelingStory() {
		gameDate.DailyEventOccured();
		cooldownTimer = spawnCooldown;
		var data = GetDataToSpawn();
		var position = GetPositionToSpawn();

		activeStories.Add(data.Create(position));
		textArea.AddLine(data.spawnMessage);
		mapPlayerController.StopMovement();
	}

	TravelingStoryData GetDataToSpawn() {
		return travelingStories[Random.Range(0, travelingStories.Count)];	
	}

	Vector2 GetPositionToSpawn() {
		var center = mapPlayerController.position;
		var distance = minDistanceToSpawn + Random.Range(0, spawnRange + 1);
		var offSideDistance = Random.Range(-distance, distance);

		var randomSideValue = Random.value;
		if(randomSideValue < 0.25f)
			return center + new Vector2(offSideDistance, distance);
		if(randomSideValue < 0.5f)
			return center + new Vector2(offSideDistance, -distance);
		if(randomSideValue < 0.75f)
			return center + new Vector2(distance, offSideDistance);

		return center + new Vector2(-distance, offSideDistance);
	}
}