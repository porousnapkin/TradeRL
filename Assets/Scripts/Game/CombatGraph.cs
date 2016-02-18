using UnityEngine;

public class CombatGraph {
#warning "What's map data for combat? is it necessary?"
	[Inject] public MapData mapData { private get; set; }
	[Inject(DesertPathfinder.COMBAT)] public DesertPathfinder pathfinder { private get; set; }

	Character[,] charactersOnMap;
	System.Action<System.Action>[,] eventsForLocations;
	
	public void Setup() {
#warning "Where are combat values for this stuff?"
		int width = 10;
		int height = 10;
		
		charactersOnMap = new Character[width, height];
		eventsForLocations = new System.Action<System.Action>[width, height];
	}
	
	public void SetCharacterToPosition(Vector2 oldPosition, Vector2 newPosition, Character c) {
		charactersOnMap[(int)oldPosition.x, (int)oldPosition.y] = null;
		charactersOnMap[(int)newPosition.x, (int)newPosition.y] = c;
		c.Position = newPosition;
		
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
	
	public void SetEventForLocation(int x, int y, System.Action<System.Action> e) {
		eventsForLocations[x,y] = e;
	}
	
	public void RemoveEventAtLocation(int x, int y) {
		eventsForLocations[x,y] = null;
	}
	
	public bool DoesLocationHaveEvent(int x, int y) {
		return eventsForLocations[x,y] != null;
	}
	
	public void TriggerLocationEvent(int x, int y, System.Action finishedEventCallback) {
		if(DoesLocationHaveEvent(x, y))
			eventsForLocations[x,y](finishedEventCallback);
		else
			finishedEventCallback();
	}
	
	public int GetNumAdjacentEnemies(Character target) {
		int total = 0;
		var position = target.Position;
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
