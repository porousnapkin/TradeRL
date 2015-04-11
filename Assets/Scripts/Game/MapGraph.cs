using UnityEngine;

public class MapGraph {
	Character[,] charactersOnMap;

	public MapGraph(int width, int height) {
		charactersOnMap = new Character[width, height];
	}

	public void SetCharacterToPosition(Vector2 oldPosition, Vector2 newPosition, Character c) {
		charactersOnMap[(int)oldPosition.x, (int)oldPosition.y] = null;
		charactersOnMap[(int)newPosition.x, (int)newPosition.y] = c;
		c.WorldPosition = newPosition;
	}

	public bool IsPositionOccupied(int x, int y) {
		return charactersOnMap[x, y] != null;
	}

	public Character GetPositionOccupant(int x, int y) {
		return charactersOnMap[x, y];
	}
}