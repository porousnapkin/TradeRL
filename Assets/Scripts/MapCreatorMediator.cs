using strange.extensions.mediation.impl;
using System;

public class MapCreatorMediator : Mediator {
	[Inject] public MapCreatorView view { private get; set; }
	[Inject] public MapData mapData { private get; set; }
	[Inject] public MapViewData mapViewData { private get; set; }
	[Inject] public MapCreator mapCreator { private get; set; }
    [Inject] public LocationMapData locationMapData { private get; set; }

	public override void OnRegister() {
		mapData.Setup(new MapData.ViewData {
			width = view.width,
			height = view.height,
			minDistanceFromCities = view.minDistanceFromCities,
			numCities = view.numCities,
			minDistanceFromTowns = view.minDistanceFromTowns,
			numTowns = view.numTowns
		});

		mapCreator.hideLocationSpriteEvent += HideSprite;
		mapCreator.showLocationSpriteEvent += ShowSprite;
		mapCreator.dimLocationSpriteEvent += DimSprite;
		mapCreator.undimLocationSpriteEvent += UnDimSprite;
        mapCreator.disableLocationSpriteEvent += DisableLocationSprite;
        mapCreator.enableLocationSpriteEvent += EnableLocationSprite;
        mapCreator.createCombatMapSpriteEvent += CreateCombatMapSprite;

        locationMapData.locationAdded += LocationAdded;
        locationMapData.locationRemoved += LocationRemoved;
	}

    private void CreateCombatMapSprite(UnityEngine.Transform transform, int x, int y)
    {
        var data = mapViewData.CreateRandomDesertTile();
        view.CreateCombatMapSprite(transform, x, y, mapViewData.GetBaseSprite(data), mapViewData.GetGarnishSprite(data));
    }

    void LocationRemoved(int x, int y)
    {
        view.SetGarnishSprite(mapViewData.GetGarnishSprite(x, y), x, y);
    }

    void LocationAdded(int x, int y)
    {
        var locData = locationMapData.GetLocationData(x, y);
        view.SetGarnishSprite(locData.art, x, y);
    }

    public override void OnRemove() {
		mapCreator.hideLocationSpriteEvent -= HideSprite;
		mapCreator.showLocationSpriteEvent -= ShowSprite;
		mapCreator.dimLocationSpriteEvent -= DimSprite;
		mapCreator.undimLocationSpriteEvent -= UnDimSprite;
	}

    public void HideSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;

        view.HideBaseSprite(x, y);
        view.HideGarnishSprite(x, y);
	}
	
	public void ShowSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;

        view.ShowSprite(x, y);
	}
	
	public void DimSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;

        view.DimSprite(x,y);
	}

    public void UnDimSprite(int x, int y)
    {
        if (!mapData.CheckPosition(x, y))
            return;

        view.UnDimSprite(x,y);
    }

    public void DisableLocationSprite(int x, int y)
    {
        view.DestroyTileAtPosition(x,y);
    }

    private void EnableLocationSprite(int x, int y)
    {
        CreateTileForPosition(x, y);
    }

    void CreateTileForPosition(int x, int y)
    {
        var location = locationMapData.GetLocationData(x, y);
        if (location != null)
            view.CreateTileForPosition(x, y, mapViewData.GetBaseSprite(x, y), location.art);
        else
            view.CreateTileForPosition(x, y, mapViewData.GetBaseSprite(x, y), mapViewData.GetGarnishSprite(x, y));
    }
}

public class MapCreator {
	public event Action<int, int> hideLocationSpriteEvent = delegate{};
	public event Action<int, int> showLocationSpriteEvent = delegate{};
	public event Action<int, int> dimLocationSpriteEvent = delegate{};
	public event Action<int, int> undimLocationSpriteEvent = delegate{};
    public event Action<int, int> disableLocationSpriteEvent = delegate { };
    public event Action<int, int> enableLocationSpriteEvent = delegate { };
    public event Action<UnityEngine.Transform, int, int> createCombatMapSpriteEvent = delegate { };
	
	public void HideLocation(int x, int y) {
		hideLocationSpriteEvent(x, y);
	}
	
	public void ShowLocation(int x, int y) {
		showLocationSpriteEvent(x, y);
	}
	
	public void DimLocation(int x, int y) {
		dimLocationSpriteEvent(x, y);
	}

    public void UnDimLocation(int x, int y)
    {
        undimLocationSpriteEvent(x, y);
    }

    public void DisableLocationSprite(int x, int y)
    {
        disableLocationSpriteEvent(x, y);
    }

    public void EnableLocationSprite(int x, int y)
    {
        enableLocationSpriteEvent(x, y);
    }

    public void CreateCombatMapSprite(UnityEngine.Transform t, int x, int y)
    {
        createCombatMapSpriteEvent(t, x, y);
    }
}
