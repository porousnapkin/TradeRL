using UnityEngine;

public class MapGraph {
	Character[,] charactersOnMap;
	System.Action<System.Action>[,] combatEventsForLocations;
	System.Action<System.Action>[,] worldEventsForLocations;

	static MapGraph instance = null;
	public static MapGraph Instance { get {  return instance; }}
	public DesertPathfinder pathfinder;
	public bool isInCombat = false;

	public MapGraph(int width, int height) {
		charactersOnMap = new Character[width, height];
		combatEventsForLocations = new System.Action<System.Action>[width, height];
		worldEventsForLocations = new System.Action<System.Action>[width, height];

		instance = this;
	}

	public void SetCharacterToPosition(Vector2 oldPosition, Vector2 newPosition, Character c) {
		charactersOnMap[(int)oldPosition.x, (int)oldPosition.y] = null;
		charactersOnMap[(int)newPosition.x, (int)newPosition.y] = c;
		c.WorldPosition = newPosition;

		pathfinder.LocationVacated(oldPosition);
		pathfinder.LocationOccupied(newPosition);
	}

	public void VacatePosition(Vector2 position) {
		pathfinder.LocationVacated(position);

		charactersOnMap[(int)position.x, (int)position.y] = null;
	}

	public bool IsPositionOccupied(int x, int y) {
		return charactersOnMap[x, y] != null;
	}

	public Character GetPositionOccupant(int x, int y) {
		return charactersOnMap[x, y];
	}

	public void SetEventForLocation(int x, int y, System.Action<System.Action> e, bool combatEvent) {
		if(combatEvent)
			combatEventsForLocations[x,y] = e;	
		else
			worldEventsForLocations[x,y] = e;
	}

	public void RemoveEventAtLocation(int x, int y, bool combatEvent) {
		if(combatEvent)
			combatEventsForLocations[x,y] = null;	
		else
			worldEventsForLocations[x,y] = null;
	}

	public void ClearCombatEvents() {
		for(int x = 0; x < combatEventsForLocations.GetLength(0); x++)
			for(int y = 0; y < combatEventsForLocations.GetLength(1); y++)
				combatEventsForLocations[x,y] = null;
	}

	public bool DoesLocationHaveEvent(int x, int y) {
		if(isInCombat)
			return combatEventsForLocations[x,y] != null;
		else
			return worldEventsForLocations[x,y] != null;
	}

	public void TriggerLocationEvent(int x, int y, System.Action finishedEventCallback) {
		if(DoesLocationHaveEvent(x, y)) {
			if(isInCombat)
				combatEventsForLocations[x,y](finishedEventCallback);
			else
				worldEventsForLocations[x,y](finishedEventCallback);
		}
		else {
			finishedEventCallback();
		}
	}

	public int GetNumAdjacentEnemies(Character target) {
		int total = 0;
		var position = target.WorldPosition;
		for(int x = -1; x <= 1; x++) {
			for(int y = -1; y <= 1; y++) {
				if(x == 0 && y == 0)
					continue;

				if(Grid.IsValidPosition((int)position.x + x, (int)position.y + y)) {	
					var c = charactersOnMap[(int)position.x + x, (int)position.y + y];
					if(c != null && c.myFaction != target.myFaction)
						total++;
				}
			}
		}

		return total;
	}
}