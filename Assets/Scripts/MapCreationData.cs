using UnityEngine;
using System.Collections.Generic;

public class MapCreationData : ScriptableObject {
	[System.Serializable]
	public class TileData {
		public Sprite sprite;
		public float weight = 1.0f;
	}

	[System.Serializable]
	public class SetTileData {
        public string tileDataName = "base";
		public List<TileData> baseTiles;
		public List<TileData> garnishTiles;
		public float garnishChance = 0.1f;
	}

	public SetTileData defaultTile;
	public List<SetTileData> tiles;
    public GameObject fogSprite;

    private Dictionary<string, SetTileData> tileDataNameToData;

    public void Setup()
    {
        tileDataNameToData = new Dictionary<string, SetTileData>();
        foreach (var t in tiles)
            tileDataNameToData.Add(t.tileDataName, t);
    }

    public Sprite GetBaseTileSprite(string tileDataName, int index)
    {
        return GetTileData(tileDataName).baseTiles[index].sprite;
    }

    public Sprite GetGarnishTileSprite(string tileDataName, int index)
    {
        var tileData = GetTileData(tileDataName);
        if(tileData.garnishTiles.Count > 0 && index > 0)
            return tileData.garnishTiles[index].sprite;
        return null;
    }

    public SetTileData GetTileData(string tileDataName)
    {
        return tileDataNameToData[tileDataName];
    }
}
