using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class MapCreatorView : DesertView {
	public int width = 100;
	public int height = 100;
	public MapCreationData mapCreationData;
	public GridInputCollectorView inputCollector;

	public int minDistanceFromCities = 20;
	public int numCities = 7;
	public int minDistanceFromTowns = 10;
	public int numTowns = 12;

	public enum TileType {
		City,
		Town,
		Dune,
		Ground,
	}

	public class CreatedTileData 
	{
		public SpriteRenderer baseSprite;
		public SpriteRenderer garnishSprite;
	}

	public CreatedTileData CreateTileForPosition(int x, int y, TileType tileType) {
		MapCreationData.TileData  tileData;
		MapCreationData.SetTileData setTileData;

		switch(tileType) {
		case TileType.City:
			tileData = mapCreationData.tiles[0].baseTiles[1];
			setTileData = mapCreationData.tiles[0];
			break;
		case TileType.Town:
			tileData = mapCreationData.tiles[0].baseTiles[2];
			setTileData = mapCreationData.tiles[0];
			break;
		case TileType.Dune:
			tileData = mapCreationData.tiles[0].baseTiles[0];
			setTileData = mapCreationData.tiles[0];
			break;
		case TileType.Ground:
			tileData = GetRandomTileData(mapCreationData.defaultTile.baseTiles);
			setTileData = mapCreationData.defaultTile;
			break;
		default: 
			tileData = GetRandomTileData(mapCreationData.defaultTile.baseTiles);
			setTileData = mapCreationData.defaultTile;
			break;
		}

		var retval = new CreatedTileData();
		retval.baseSprite = CreateSpriteAtPosition(tileData.sprite, Grid.GetBaseWorldPositionFromGridPosition(x, y), x, y);

		if(Random.value < setTileData.garnishChance) {
			retval.garnishSprite = CreateSpriteAtPosition(GetRandomTileData(setTileData.garnishTiles).sprite, 
				Grid.GetGarnishWorldPositionFromGridPosition(x, y), x, y);
		}

		return retval;
	}

	MapCreationData.TileData GetRandomTileData(List<MapCreationData.TileData> tileDatas) {
		var tileData = tileDatas[Random.Range(0, tileDatas.Count)];
		if(Random.value < tileData.weight)
			return tileData;
		else
			return GetRandomTileData(tileDatas);
	}

	SpriteRenderer CreateSpriteAtPosition(Sprite s, Vector3 worldPosition, int gridX, int gridY) {
		var spriteRenderer = CreateSpriteAtPosition(s, worldPosition, gridX, gridY, "World", inputCollector);
		spriteRenderer.transform.parent = transform;
		return spriteRenderer;
	}
	
	public static SpriteRenderer CreateSpriteAtPosition(Sprite s, Vector3 worldPosition, int gridX, int gridY, string layerName, GridInputCollectorView inputCollector) {
		var spriteGO = new GameObject(s.name);
		spriteGO.layer = LayerMask.NameToLayer(layerName);
		var gridPos = spriteGO.AddComponent<GridInputPosition>();
		gridPos.position = new Vector2(gridX, gridY);
		gridPos.gridInputCollector = inputCollector;
		var sr = spriteGO.AddComponent<SpriteRenderer>();
		sr.sprite = s;
		spriteGO.AddComponent<PolygonCollider2D>();
		spriteGO.transform.position = worldPosition;
		
		return sr;
	}

	public void HideBaseSprite(SpriteRenderer sr) 
	{
		sr.color = Color.black;
	}

	public void HideGarnishSprite(SpriteRenderer sr)
	{
		sr.color = new Color(0, 0, 0, 0);
	}

	public void ShowSprite(SpriteRenderer sr) {
		sr.color = Color.white;
	}

	public void SetupLocationSprite(Sprite s, SpriteRenderer baseSprite, SpriteRenderer garnishSprite) {
        if (garnishSprite != null)
        {
            garnishSprite.enabled = true;
            garnishSprite.sprite = s;
        }
	}

	const float dimness = 0.7f;
	public void DimSprite(SpriteRenderer sr) {
		//Only dim sprites that aren't hidden.
		if(sr.color.r > 0)
			sr.color = new Color(dimness, dimness, dimness, 1.0f);
	}
}
