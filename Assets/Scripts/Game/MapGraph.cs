using UnityEngine;

public class MapGraph {
	[Inject] public MapData mapData { private get; set; }
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder {private get; set; }

	Vector2 playerPos;
	public Vector2 PlayerPosition { 
		get {
			return playerPos;
		}
		set {
			playerPos = value;
		}
	}
	TravelingStory[,] travelingStory;
	System.Action<System.Action>[,] eventsForLocations;

	public void Setup() {
		int width = mapData.Width;
		int height = mapData.Height;

		travelingStory = new TravelingStory[width, height];
		eventsForLocations = new System.Action<System.Action>[width, height];
	}

	public void SetEventForLocation(int x, int y, System.Action<System.Action> e) {
		eventsForLocations[x,y] = e;
	}

	public void RemoveEventAtLocation(int x, int y) {
		eventsForLocations[x,y] = null;
	}

	public bool DoesLocationHaveEvent(int x, int y)
	{
	    if (eventsForLocations == null)
	        return false;
		return eventsForLocations[x,y] != null;
	}

	public void TriggerLocationEvent(int x, int y, System.Action finishedEventCallback) {
		if(DoesLocationHaveTravelingStory(x, y)) 
			travelingStory[x,y].Activate(() => TriggerLocationEvent(x, y, finishedEventCallback));
		else if(DoesLocationHaveEvent(x, y)) 
			eventsForLocations[x,y](finishedEventCallback);
		else
			finishedEventCallback();
	}

	public bool DoesLocationHaveTravelingStory(int x, int y) {
		return travelingStory[x,y] != null;
	}

	public void SetTravelingStoryToPosition(Vector2 newPosition, TravelingStory tsv) {
		travelingStory[(int)newPosition.x, (int)newPosition.y] = tsv;
		pathfinder.LocationOccupied(newPosition);
	}

	public void TravelingStoryVacatesPosition(Vector2 newPosition) {
		travelingStory[(int)newPosition.x, (int)newPosition.y] = null;
		pathfinder.LocationVacated(newPosition);
	}

	public TravelingStory GetTravelingStoryAtLocation(Vector2 newPosition) {
		return travelingStory[(int)newPosition.x, (int)newPosition.y];
	}
}