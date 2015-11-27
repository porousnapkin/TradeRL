using UnityEngine;

public static class Grid {
	static float tileWidth = 1.0f;
	static float tileHeight = 0.5f;
	static int width = 100;
	static int height = 100;

	public static bool IsValidPosition(int x, int y) {
		return (x >= 0 && x < width && y > 0 && y < height);
	}

	public static Vector3 GetBaseWorldPositionFromGridPosition(int x, int y) {
		return new Vector3(x * (tileWidth / 2) + y * (tileWidth / 2), -x * (tileHeight / 2) + y * (tileHeight / 2), 0);
	}

	public static Vector3 GetGarnishWorldPositionFromGridPosition(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -1);
	}

	public static Vector3 GetCharacterWorldPositionFromGridPositon(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -2);
	}

	public static Vector2 GetGridPosition(Vector3 worldPosition) {
		return new Vector2(worldPosition.x / (tileWidth / 2), worldPosition.y / (tileHeight / 2));
	}

	public static Vector3 GetBaseCombatPosition(int x, int y) {
		var worldPos = GetBaseWorldPositionFromGridPosition(x, y);
		return new Vector3(worldPos.x, worldPos.y, -100);
	}

	public static Vector3 GetGarnishCombatPosition(int x, int y) {
		return GetBaseCombatPosition(x, y) + new Vector3(0, 0, -1);
	}

	public static Vector3 GetCharacterCombatPosition(int x, int y) {
		return GetBaseCombatPosition(x, y) + new Vector3(0, 0, -1);
	}
}