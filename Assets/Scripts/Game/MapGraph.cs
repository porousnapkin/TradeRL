using UnityEngine;

public class MapGraph {
	Character[,] charactersOnMap;

	static MapGraph instance = null;
	public static MapGraph Instance { get {  return instance; }}
	public MapGraph(int width, int height) {
		charactersOnMap = new Character[width, height];

		instance = this;
	}

	public void SetCharacterToPosition(Vector2 oldPosition, Vector2 newPosition, Character c) {
		charactersOnMap[(int)oldPosition.x, (int)oldPosition.y] = null;
		charactersOnMap[(int)newPosition.x, (int)newPosition.y] = c;
		c.WorldPosition = newPosition;
	}

	public void VacatePosition(Vector2 position) {
		charactersOnMap[(int)position.x, (int)position.y] = null;
	}

	public bool IsPositionOccupied(int x, int y) {
		return charactersOnMap[x, y] != null;
	}

	public Character GetPositionOccupant(int x, int y) {
		return charactersOnMap[x, y];
	}
}