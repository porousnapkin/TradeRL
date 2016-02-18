using UnityEngine;
using System.Collections.Generic;

public interface TravelingStoryAIRoutine {
	bool DoesAct();
	Vector2 GetMoveToPosition(Vector2 currentPosition);
}

public class TravelingStoryWander : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	public float idleChance = 0.75f;

	public bool DoesAct() {
		return Random.value > idleChance;
	}
	
	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		var offset = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
		var newPosition = currentPosition + offset;
		
		if(!Grid.IsValidPosition((int)newPosition.x, (int)newPosition.y) ||
		   mapGraph.GetTravelingStoryAtLocation(newPosition) != null)
			return GetMoveToPosition(currentPosition);
		return newPosition;
	}
}

public class TravelingStoryChase : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder {private get; set;}

	public bool DoesAct() {
		return true;
	}

	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		var path = pathfinder.SearchForPathOnMainMap(currentPosition, mapPlayerController.position);

		if(path.Count  < 2)
			return currentPosition;

		return path[1];
	}
}

public class TravelingStoryFlee : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	[Inject] public MapPlayerController mapPlayerController {private get; set;}

	public bool DoesAct() {
		return true;
	}

	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		var fleeFromPos = mapPlayerController.position;
		var validMoves = new List<Vector2>();

		for(int x = -1; x <= 1; x++) {
			for(int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0)
					continue;

				if(Grid.IsValidPosition((int)currentPosition.x + x, (int)currentPosition.y + y))
					validMoves.Add(new Vector2(currentPosition.x + x, currentPosition.y + y));
			}
		}

		validMoves.Sort((first, second) => (int)((Vector2.Distance(fleeFromPos, second) - Vector2.Distance(fleeFromPos, first)) * 100));
		return validMoves[0];
	}
}
