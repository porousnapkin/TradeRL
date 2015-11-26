using UnityEngine;
using System.Collections;

public class TravelingStoryFactory {
	public static GameObject travelingStoryPrefab;
	public static MapGraph mapGraph;
	public static TurnManager turnManager;

	public static void CreateTravelingStory(TravelingStoryData data, Vector2 position) {
		var go = GameObject.Instantiate(travelingStoryPrefab) as GameObject;
		TravelingStoryVisuals visuals = go.GetComponent<TravelingStoryVisuals>();
		visuals.data = data;
		visuals.mapGraph = mapGraph;
		visuals.turnManager = turnManager;

		visuals.WorldPosition = position;
		visuals.TeleportToWorldPosition();
	}
}
