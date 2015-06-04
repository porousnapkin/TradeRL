using UnityEngine;

public class MapGraph {
	Character[,] charactersOnMap;

	static MapGraph instance = null;
	public static MapGraph Instance { get {  return instance; }}

	public DesertPathfinder pathfinder;

	public MapGraph(int width, int height) {
		charactersOnMap = new Character[width, height];

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