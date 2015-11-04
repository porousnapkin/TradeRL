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
	public GridInputCollector inputCollector;

	public int minDistanceFromCities = 20;
	public int numCities = 7;
	List<Vector2> cityLocations = new List<Vector2>();
	public int minDistanceFromTowns = 10;
	public int numTowns = 12;
	List<Vector2> townLocations = new List<Vector2>();
	TownsAndCities townsAndCities;

	CellularAutomata ca;
	public bool IsHill(Vector2 pos) { return ca.Graph[(int)pos.x, (int)pos.y]; }

	public class NoValidLcoationFoundException : System.Exception {}

	public DesertPathfinder Pathfinder { get { return pathfinder; }}

	void Awake () {
		ca = new CellularAutomata(width, height);
		ca.BuildRandomCellularAutomataSet(5, 0.3f);

		pathfinder = new DesertPathfinder();
		mapWeights = new int[width, height];
		baseSprites = new SpriteRenderer[width, height];
		garnishSprites = new SpriteRenderer[width, height];

		CreateCityAndTownLocations();		

		for(int x = 0; x < width; x++) 
			for(int y = 0; y < height; y++) 
				CreateTileForPosition(mapData, x, y);

		pathfinder.SetMainMapWeights(mapWeights);
	}

	void CreateCityAndTownLocations() {
		try {
			CreateCityLocations();
			CreateTownLocations();
		} catch(NoValidLcoationFoundException) {
			cityLocations.Clear();
			townLocations.Clear();

			Debug.LogWarning("Tried to make cities and locations and failed. Trying again. If this happens often, check town and city parameters.");
			CreateCityLocations();
		}

		townsAndCities = new TownsAndCities();
		var nameGenerator = new RandomNameGenerator();
		foreach(var location in cityLocations) 
			townsAndCities.AddCity(location, nameGenerator.GetCityName());
		foreach(var location in townLocations) 
			townsAndCities.AddTown(location, nameGenerator.GetTownName());
	}

	void CreateCityLocations() {
		for(int i = 0; i < numCities; i++) 
			cityLocations.Add(FindRandomCityLocation());
	}

	Vector2 FindRandomCityLocation() {
		Vector2 newLoc;
		bool validLoc = true;
		int attempts = 0;
		do {
			newLoc = new Vector2(Random.Range(1, width -1), Random.Range(1, height-1));
			validLoc = true;
			for(int i = 0; i < cityLocations.Count; i++) 
				if(Vector2.Distance(cityLocations[i], newLoc) < minDistanceFromCities)
					validLoc = false;

			attempts++;
			if(attempts > 30)
				throw new NoValidLcoationFoundException();
		} while(!validLoc);

		return newLoc;
	}

	void CreateTownLocations() {
		for(int i = 0; i < numCities; i++) 
			townLocations.Add(FindRandomTownLocation());
	}

	Vector2 FindRandomTownLocation() {
		Vector2 newLoc;
		bool validLoc = true;
		int attempts = 0;
		do {
			newLoc = new Vector2(Random.Range(1, width -1), Random.Range(1, height-1));
			validLoc = true;
			for(int i = 0; i < cityLocations.Count; i++) 
				if(Vector2.Distance(cityLocations[i], newLoc) < minDistanceFromTowns)
					validLoc = false;
			for(int i = 0; i < townLocations.Count; i++)
				if(Vector2.Distance(townLocations[i], newLoc) < minDistanceFromTowns)
					validLoc = false;

			attempts++;
			if(attempts > 30)
				throw new NoValidLcoationFoundException();
		} while(!validLoc);

		return newLoc;
	}

	void CreateTileForPosition(MapCreationData mapCreationData, int x, int y) {
		MapCreationData.TileData  tileData;
		MapCreationData.SetTileData setTileData;

		if(cityLocations.Contains(new Vector2(x, y))) {
			tileData = mapCreationData.tiles[0].baseTiles[1];
			setTileData = mapCreationData.tiles[0];
		}
		else if(townLocations.Contains(new Vector2(x, y))) {
			tileData = mapCreationData.tiles[0].baseTiles[2];
			setTileData = mapCreationData.tiles[0];
		}
		else if(ca.Graph[x, y]) {
			tileData = mapCreationData.tiles[0].baseTiles[0];
			setTileData = mapCreationData.tiles[0];
		}
		else {
			tileData = GetRandomTileData(mapCreationData.defaultTile.baseTiles);
			setTileData = mapCreationData.defaultTile;
		}

		baseSprites[x, y] = CreateSpriteAtPosition(tileData.sprite, Grid.GetBaseWorldPositionFromGridPosition(x, y), x, y);

		if(ca.Graph[x, y])
			mapWeights[x,y] = tileData.pathfindingHillWeight;
		else
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
		spriteGO.layer = LayerMask.NameToLayer("World");
		spriteGO.transform.parent = transform;
		var gridPos = spriteGO.AddComponent<GridInputPosition>();
		gridPos.position = new Vector2(gridX, gridY);
		gridPos.gridInputCollector = inputCollector;
		var sr = spriteGO.AddComponent<SpriteRenderer>();
		sr.sprite = s;
		spriteGO.AddComponent<PolygonCollider2D>();
		spriteGO.transform.position = worldPosition;

		return sr;
	}

	public bool CheckPosition(int x, int y) {
		return !(x >= width || x < 0 || y >= height || y < 0);
	}

	public void HideSprite(int x, int y) {
		if(!CheckPosition(x, y))
			return;
		if(baseSprites[x,y] != null)
			baseSprites[x, y].color = Color.black;
		if(garnishSprites[x,y] != null)
			garnishSprites[x, y].color = new Color(0, 0, 0, 0);
	}

	public void ShowSprite(int x, int y) {
		if(!CheckPosition(x, y))
			return;
		if(baseSprites[x,y] != null)
			baseSprites[x, y].color = Color.white;
		if(garnishSprites[x,y] != null)
			garnishSprites[x, y].color = Color.white;
	}

	public TownsAndCities GetTownsAndCities() {
		return townsAndCities;
	}
}
