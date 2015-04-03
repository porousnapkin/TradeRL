using UnityEngine;
using System.Collections.Generic;

public class MapCreator : MonoBehaviour {
	public int width = 100;
	public int height = 100;
	public MapCreationData mapData;
	DesertPathfinder pathfinder;
	int[,] mapWeights;
	SpriteRenderer[,] baseSprites;
	SpriteRenderer[,] garnishSprites;

	CellularAutomata ca;

	public DesertPathfinder Pathfinder { get { return pathfinder; }}

	void Awake () {
		ca = new CellularAutomata(width, height);
		ca.BuildRandomCellularAutomataSet(5, 0.3f);

		pathfinder = new DesertPathfinder();
		mapWeights = new int[width, height];
		baseSprites = new SpriteRenderer[width, height];
		garnishSprites = new SpriteRenderer[width, height];

		for(int x = 0; x < width; x++) 
			for(int y = 0; y < height; y++) 
				CreateTileForPosition(mapData, x, y);

		pathfinder.SetMainMapWeights(mapWeights);
	}

	void CreateTileForPosition(MapCreationData mapCreationData, int x, int y) {
		MapCreationData.TileData  tileData;
		MapCreationData.SetTileData setTileData;

		if(ca.Graph[x, y]) {
			tileData = mapCreationData.tiles[0].baseTiles[0];
			setTileData = mapCreationData.tiles[0];
		}
		else {
			tileData = GetRandomTileData(mapCreationData.defaultTile.baseTiles);
			setTileData = mapCreationData.defaultTile;
		}


		baseSprites[x, y] = CreateSpriteAtPosition(tileData.sprite, Grid.GetBaseWorldPositionFromGridPosition(x, y), x, y);
		mapWeights[x,y] = tileData.pathfindingWeight;

		if(Random.value < setTileData.garnishChance) {
			garnishSprites[x, y] = CreateSpriteAtPosition(GetRandomTileData(setTileData.garnishTiles).sprite, 
				Grid.GetGarnishWorldPositionFromGridPosition(x, y), x, y);
		}
	}

	MapCreationData.TileData GetRandomTileData(List<MapCreationData.TileData> tileDatas) {
		var tileData = tileDatas[Random.Range(0, tileDatas.Count)];
		if(Random.value < tileData.weight)
			return tileData;
		else
			return GetRandomTileData(tileDatas);
	}

	SpriteRenderer CreateSpriteAtPosition(Sprite s, Vector3 worldPosition, int gridX, int gridY) {
		var spriteGO = new GameObject(s.name);
		spriteGO.transform.parent = transform;
		var gridPos = spriteGO.AddComponent<GridPosition>();
		gridPos.position = new Vector2(gridX, gridY);
		var sr = spriteGO.AddComponent<SpriteRenderer>();
		sr.sprite = s;
		var collider = spriteGO.AddComponent<BoxCollider2D>();
		collider.size = new Vector2(1, 0.5f);
		spriteGO.transform.position = worldPosition;

		return sr;
	}

	public void HideSprite(int x, int y) {
		if(baseSprites[x,y] != null)
			baseSprites[x, y].color = Color.black;
		if(garnishSprites[x,y] != null)
			garnishSprites[x, y].color = new Color(0, 0, 0, 0);
	}

	public void ShowSprite(int x, int y) {
		if(baseSprites[x,y] != null)
			baseSprites[x, y].color = Color.white;
		if(garnishSprites[x,y] != null)
			garnishSprites[x, y].color = Color.white;
	}
}
