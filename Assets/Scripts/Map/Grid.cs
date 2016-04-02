using UnityEngine;
using System.Collections.Generic;

public static class Grid {
	static float tileWidth = 1.0f;
	static float tileHeight = 0.5f;
	static int width = 100;
	static int height = 100;
    static float diagonalHeight = 0.3f;
    static float garnishHeight = 0.1f;
    static float characterHeight = 0.2f;

	public static bool IsValidPosition(int x, int y) {
		return (x >= 0 && x < width && y > 0 && y < height);
	}

	public static Vector3 GetBaseWorldPositionFromGridPosition(int x, int y) {
		return new Vector3(x * (tileWidth / 2) + y * (tileWidth / 2), -x * (tileHeight / 2) + y * (tileHeight / 2), GetZ(x, y));
	}

    static float GetZ(int x, int y)
    {
        return (y - x) * diagonalHeight;
    }

	public static Vector3 GetGarnishWorldPositionFromGridPosition(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -garnishHeight);
	}

	public static Vector3 GetCharacterWorldPositionFromGridPositon(int x, int y) {
		return GetBaseWorldPositionFromGridPosition(x, y) + new Vector3(0, 0, -characterHeight);
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

    public static List<Vector2> GetAdjacentValidPositions(int x, int y)
    {
        List<Vector2> retVal = new List<Vector2>();
        for (int xAdd = -1; xAdd <= 1; xAdd++) {
            for (int yAdd = -1; yAdd <= 1; yAdd++) {
                if (xAdd == 0 && yAdd == 0)
                    continue;

                var newPos = new Vector2(x+ xAdd, y+ yAdd);

                if (IsValidPosition((int)newPos.x, (int)newPos.y))
                    retVal.Add(newPos);
            }
        }

        return retVal;
    }
}