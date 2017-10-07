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
    public GameObject[,] gridGOs;

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

        instance = this;
        gridGOs = new GameObject[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var go = new GameObject(x.ToString() + "," + y.ToString());
                gridGOs[x, y] = go;
                go.transform.parent = transform;
            }
        }
    }

    public FogView CreateFogSprite(int x, int y)
    {
        var worldPos = Grid.GetCharacterWorldPositionFromGridPositon(x, y);
        worldPos.z = -100.0f;
        var fog = GameObject.Instantiate(mapCreationData.fogSprite, gridGOs[x,y].transform);
        fog.transform.position = worldPos;
        return fog.GetComponent<FogView>();
    }

    public CreatedTileData CreateTileForPosition(int x, int y, Sprite baseSprite, Sprite garnishSprite)
    {
        var retval = new CreatedTileData();
		retval.baseSprite = CreateSpriteAtPosition(baseSprite, "ground", Grid.GetBaseWorldPositionFromGridPosition(x, y), x, y, false);
	    retval.garnishSprite = CreateSpriteAtPosition(garnishSprite, "garnish", Grid.GetGarnishWorldPositionFromGridPosition(x, y), x, y, true);
        retval.garnishSprite.enabled = garnishSprite != null;

		return retval;
    }

	SpriteRenderer CreateSpriteAtPosition(Sprite s, string name, Vector3 worldPosition, int gridX, int gridY, bool garnish) {
		var spriteRenderer = CreateSpriteAtPosition(s, name, worldPosition, gridX, gridY, "World", inputCollector, garnish);
        spriteRenderer.transform.parent = gridGOs[gridX, gridY].transform;
        return spriteRenderer;
	}
	
	public static SpriteRenderer CreateSpriteAtPosition(Sprite s, string name, Vector3 worldPosition, int gridX, int gridY, string layerName, GridInputCollectorView inputCollector, bool garnish) {
		var spriteGO = new GameObject(name);
		spriteGO.layer = LayerMask.NameToLayer(layerName);
		var sr = spriteGO.AddComponent<SpriteRenderer>();
		sr.sprite = s;
        if (!garnish)
        {
            var gridPos = spriteGO.AddComponent<GridInputPosition>();
            gridPos.position = new Vector2(gridX, gridY);
            gridPos.gridInputCollector = inputCollector;
		    spriteGO.AddComponent<PolygonCollider2D>();
        }
		spriteGO.transform.position = worldPosition;
		
		return sr;
	}

	public void HideBaseSprite(SpriteRenderer sr) 
	{
        sr.color = Color.white;
	}

	public void HideGarnishSprite(SpriteRenderer sr)
	{
		sr.color = new Color(0, 0, 0, 0);
	}

	public void ShowSprite(SpriteRenderer sr) {
		sr.color = Color.white;
	}

    public void DisableSprite(SpriteRenderer sr)
    {
        sr.transform.parent.gameObject.SetActive(false);
    }

    public void EnableSprite(SpriteRenderer sr)
    {
        sr.transform.parent.gameObject.SetActive(true);
    }

	public void SetupLocationSprite(Sprite s, SpriteRenderer baseSprite, SpriteRenderer garnishSprite) {
        if (garnishSprite != null)
        {
            garnishSprite.enabled = true;
            garnishSprite.sprite = s;
        }
	}

    public void RemoveLocationSprite(SpriteRenderer baseSprite, SpriteRenderer garnishSprite)
    {
        if (garnishSprite != null)
        {
            garnishSprite.enabled = false;
            garnishSprite.sprite = null;
        }
    }

	const float dimness = 0.7f;
	public void DimSprite(SpriteRenderer sr) {
	    sr.color = new Color(dimness, dimness, dimness, 1.0f);
	}

    public void UnDimSprite(SpriteRenderer sr)
    {
        sr.color = Color.white;
    }
}
