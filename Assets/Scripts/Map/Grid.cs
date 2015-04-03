using UnityEngine;

public static class Grid {
	static float tileWidth = 1.0f;
	static float tileHeight = 0.5f;

	public static Vector3 GetBaseWorldPositionFromGridPosition(int x, int y) {
		return new Vector3(x * (tileWidth / 2) + y * (tileWidth / 2), -x * (tileHeight / 2) + y * (tileHeight / 2), 0);
	}

	public static Vector3 GetGarnishWorldPositionFromGridPosition(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -1);
	}

	public static Vector3 GetCharacterWorldPositionFromGridPositon(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -2);
	}
}