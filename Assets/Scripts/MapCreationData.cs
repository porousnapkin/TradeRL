using UnityEngine;
using System.Collections.Generic;

public class MapCreationData : ScriptableObject {
	[System.Serializable]
	public class TileData {
		public Sprite sprite;
		public float weight = 1.0f;
		public int pathfindingWeight = 30;
		public int pathfindingHillWeight = 60;
	}

	[System.Serializable]
	public class SetTileData {
		public List<TileData> baseTiles;
		public List<TileData> garnishTiles;
		public float garnishChance = 0.1f;
	}

	// [System.Serializable]
	// public class AdditiveTileData {
	// 	public IntegrationMethod integrationMethod;
	// 	public List<TileData> baseTiles;
	// 	public List<TileData> garnishTiles;
	// }

	public SetTileData defaultTile;
	public List<SetTileData> tiles;
    public GameObject fogSprite;
}
