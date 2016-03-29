using UnityEngine;
using System.Collections.Generic;

public interface TravelingStoryAIRoutine {
	bool DoesAct();
	Vector2 GetMoveToPosition(Vector2 currentPosition);
}

public enum TravelingStorySpeed
{
    Fast,
    Normal,
    Slow,
}

public class TravelingStoryWander : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	[Inject] public MapData mapData {private get; set;}
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinding {private get; set;}
    public TravelingStorySpeed speed;
	public int distanceToWander {private get; set;}
	Vector2 destination;
    bool slowWait = false;

	public bool DoesAct() {
        bool wait = false;
        if (slowWait)
            wait = true;
        slowWait = false;

		return !wait;
	}
	
	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		if(currentPosition == destination || destination == Vector2.zero)
			destination = GetWanderPoint(currentPosition);

        if (speed == TravelingStorySpeed.Slow)
            slowWait = true;

		var path = pathfinding.SearchForPathOnMainMap(currentPosition, destination);
        if (path.Count > 1 && mapGraph.GetTravelingStoryAtLocation(path[1]) == null)
        {
            if(speed == TravelingStorySpeed.Fast && path.Count > 2 && mapGraph.GetTravelingStoryAtLocation(path[2]) == null)
                return path[2];
            return path[1];
        }

		return currentPosition;
	}

	Vector2 GetWanderPoint(Vector2 currentPosition) {
		int xAdd = Random.value > 0.5f? distanceToWander : -distanceToWander;
		int yAdd = Random.value > 0.5f? distanceToWander : -distanceToWander;
		if(Random.value < 0.5f)
			xAdd = Random.Range(-distanceToWander, distanceToWander);
		else
			yAdd = Random.Range(-distanceToWander, distanceToWander);

		var point = currentPosition + new Vector2(xAdd, yAdd);

		if( !mapData.CheckPosition((int)point.x, (int)point.y) || mapData.IsHill(point) ||
			pathfinding.SearchForPathOnMainMap(currentPosition, destination).Count == 0 )
			return GetWanderPoint(currentPosition);

		return point;
	}
}

public class TravelingStoryChase : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder {private get; set;}
    public TravelingStorySpeed speed;
    bool slowWait = false;

	public bool DoesAct() {
        bool wait = false;
        if (slowWait)
            wait = true;
        slowWait = false;

        return !wait;
	}

	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
		var path = pathfinder.SearchForPathOnMainMap(currentPosition, mapPlayerController.position);

        if (speed == TravelingStorySpeed.Slow)
            slowWait = true;

		if(path.Count  < 2)
			return currentPosition;

        if(speed == TravelingStorySpeed.Fast && path.Count > 2)
    		return path[2];
        return path[1];
	}
}

public class TravelingStoryFlee : TravelingStoryAIRoutine {
	[Inject] public MapGraph mapGraph {private get; set;}
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
    public TravelingStorySpeed speed;
    bool slowWait = false;

	public bool DoesAct() {  
        bool wait = false;
        if (slowWait)
            wait = true;
        slowWait = false;

		return !wait;
	}

	public Vector2 GetMoveToPosition(Vector2 currentPosition) {
        if (speed == TravelingStorySpeed.Slow)
            slowWait = true;

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

        //TODO: How do I fast move?
		validMoves.Sort((first, second) => (int)((Vector2.Distance(fleeFromPos, second) - Vector2.Distance(fleeFromPos, first)) * 100));
		return validMoves[0];
	}
}
