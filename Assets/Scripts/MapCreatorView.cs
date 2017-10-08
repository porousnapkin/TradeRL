using System;
using System.Collections.Generic;
using UnityEngine;

public class MapCreatorView : DesertView {
    public static MapCreatorView Instance { get { return instance; } }
    private static MapCreatorView instance;

    public int width = 100;
	public int height = 100;
	public MapCreationData mapCreationData;
	public GridInputCollectorView inputCollector;

	public int minDistanceFromCities = 20;
	public int numCities = 7;
	public int minDistanceFromTowns = 10;
	public int numTowns = 12;

    class PooledTile
    {
        public GameObject parentGO;
        public SpriteRenderer baseSprite;
        public SpriteRenderer garnishSprite;
        public FogView fog;
        public GridInputPosition gridInputPosition;
    }
    List<PooledTile> availablePooledTiles = new List<PooledTile>();

    PooledTile[,] pooledTiles;


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

    protected override void Start()
    {
        base.Start();

        pooledTiles = new PooledTile[width, height];

        instance = this;
    }

    public void DestroyTileAtPosition(int x, int y)
    {
        if (pooledTiles[x, y] == null)
            return;

        var pooledTile = pooledTiles[x, y];
        pooledTile.parentGO.SetActive(false);
        pooledTiles[x, y] = null;
        availablePooledTiles.Add(pooledTile);
    }

    public void CreateTileForPosition(int x, int y, Sprite baseSprite, Sprite garnishSprite)
    {
        var pooledTile = SetupPooledTileForPosition(x, y);
        pooledTile.baseSprite.sprite = baseSprite;
        pooledTile.garnishSprite.sprite = garnishSprite;
    }

    private PooledTile SetupPooledTileForPosition(int x, int y)
    {
        PooledTile pooledTile;
        if(availablePooledTiles.Count > 0)
            pooledTile = SetupUnusedPooledTile(x, y);
        else
            pooledTile = CreateNewPooledTile(x, y);

        pooledTiles[x, y] = pooledTile;
        pooledTile.gridInputPosition.position = new Vector2(x, y);
        pooledTile.baseSprite.transform.position = Grid.GetBaseWorldPositionFromGridPosition(x, y);
        pooledTile.garnishSprite.transform.position = Grid.GetGarnishWorldPositionFromGridPosition(x, y);
        pooledTile.fog.transform.position = Grid.GetCharacterWorldPositionFromGridPositon(x, y) + new Vector3(0, 0, -100.0f);

        return pooledTile;
    }

    private PooledTile SetupUnusedPooledTile(int x, int y)
    {
        var pooledTile = availablePooledTiles[0];
        availablePooledTiles.RemoveAt(0);
        pooledTile.parentGO.SetActive(true);
        pooledTile.fog.Reset();
        return pooledTile;
    }

    private PooledTile CreateNewPooledTile(int x, int y)
    {
        var pooledTile = new PooledTile();

        var go = new GameObject("tile");
        go.transform.parent = transform;
        pooledTile.parentGO = go;

        pooledTile.baseSprite = CreateSprite(null, go.transform);
        pooledTile.garnishSprite = CreateSprite(null, go.transform);
        pooledTile.fog = CreateFogSprite(x, y, go.transform);

        var gridPos = pooledTile.baseSprite.gameObject.AddComponent<GridInputPosition>();
        gridPos.position = new Vector2(x, y);
        gridPos.gridInputCollector = inputCollector;
        pooledTile.baseSprite.gameObject.AddComponent<PolygonCollider2D>();
        pooledTile.gridInputPosition = gridPos;

        return pooledTile;
    }

    FogView CreateFogSprite(int x, int y, Transform parent)
    {
        var fog = GameObject.Instantiate(mapCreationData.fogSprite, parent);
        return fog.GetComponent<FogView>();
    }

	SpriteRenderer CreateSprite(Sprite s, Transform parent) {
        var spriteGO = new GameObject("tileSprite");
        spriteGO.layer = LayerMask.NameToLayer("World");
        var sr = spriteGO.AddComponent<SpriteRenderer>();
        sr.sprite = s;
        sr.transform.parent = parent;

        return sr;
	}

	public void HideBaseSprite(int x, int y) 
	{
        if(pooledTiles[x,y] != null)
            pooledTiles[x,y].baseSprite.color = Color.white;
	}

	public void HideGarnishSprite(int x, int y)
	{
        if(pooledTiles[x,y] != null)
    		pooledTiles[x,y].garnishSprite.color = new Color(0, 0, 0, 0);
	}

	public void ShowSprite(int x, int y) {
        if (pooledTiles[x, y] == null)
            return;
		pooledTiles[x,y].baseSprite.color = Color.white;
		pooledTiles[x,y].garnishSprite.color = Color.white;
        pooledTiles[x,y].fog.Undim();
	}

    public void SetGarnishSprite(Sprite s, int x, int y)
    {
        if(pooledTiles[x,y] != null)
            pooledTiles[x, y].garnishSprite.sprite = s;
    }

    //const float dimness = 0.7f;
	public void DimSprite(int x, int y) {
        if (pooledTiles[x,y] != null)
            pooledTiles[x, y].fog.Dim();
	}

    public void UnDimSprite(int x, int y)
    {
        if (pooledTiles[x, y] == null)
            return;
        pooledTiles[x, y].baseSprite.color = Color.white;
        pooledTiles[x, y].garnishSprite.color = Color.white;
        pooledTiles[x, y].fog.Undim();
    }

    public void CreateCombatMapSprite(Transform transform, int x, int y, Sprite baseSprite, Sprite garnishSprite)
    {
        var b = CreateSprite(baseSprite, transform);
        var g = CreateSprite(garnishSprite, transform);

        b.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Combat"));
        g.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Combat"));
        b.transform.position = Grid.GetBaseWorldPositionFromGridPosition(x, y);
        g.transform.position = Grid.GetGarnishWorldPositionFromGridPosition(x, y);
    }
}
