using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using System.Collections.Generic;
using strange.extensions.signal.impl;
using System;

public class MapCreatorMediator : Mediator {
	[Inject] public MapCreatorView view { private get; set; }
	[Inject] public MapData mapData { private get; set; }
	[Inject] public MapCreator mapCreator { private get; set; }

	SpriteRenderer[,] baseSprites;
	SpriteRenderer[,] garnishSprites;

	public override void OnRegister() {
		mapData.Setup(new MapData.ViewData {
			width = view.width,
			height = view.height,
			minDistanceFromCities = view.minDistanceFromCities,
			numCities = view.numCities,
			minDistanceFromTowns = view.minDistanceFromTowns,
			numTowns = view.numTowns
		});

		baseSprites = new SpriteRenderer[view.width, view.height];
		garnishSprites = new SpriteRenderer[view.width, view.height];

		mapCreator.createMapVisualsEvent += CreateTilesForMap;
		mapCreator.hideLocationSpriteEvent += HideSprite;
		mapCreator.showLocationSpriteEvent += ShowSprite;
		mapCreator.dimLocationSpriteEvent += DimSprite;
		mapCreator.setupSpecialLocationSpriteEvent += SetupLocationSprite;
	}

	public override void OnRemove() {
		mapCreator.createMapVisualsEvent -= CreateTilesForMap;
		mapCreator.hideLocationSpriteEvent -= HideSprite;
		mapCreator.showLocationSpriteEvent -= ShowSprite;
		mapCreator.dimLocationSpriteEvent -= DimSprite;
		mapCreator.setupSpecialLocationSpriteEvent -= SetupLocationSprite;
	}

	public void CreateTilesForMap() {
		for(int x = 0; x < view.width; x++) 
			for(int y = 0; y < view.height; y++) 
				CreateTileForPosition(x, y);
	}
	
	void CreateTileForPosition(int x, int y) {
		MapCreatorView.CreatedTileData tileData;

		Vector2 pos = new Vector2(x, y);
		if(mapData.IsCity(pos))
		   tileData = view.CreateTileForPosition(x, y, MapCreatorView.TileType.City);
		else if(mapData.IsTown(pos))
		   tileData = view.CreateTileForPosition(x, y, MapCreatorView.TileType.Town);
		else if(mapData.IsHill(pos))
			tileData = view.CreateTileForPosition(x, y, MapCreatorView.TileType.Dune);
		else
			tileData = view.CreateTileForPosition(x, y, MapCreatorView.TileType.Ground);

		baseSprites[x,y] = tileData.baseSprite;
		garnishSprites[x, y] = tileData.garnishSprite;
	}

	public void HideSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;

		if(baseSprites[x,y] != null)
			view.HideBaseSprite(baseSprites[x, y]);
		if(garnishSprites[x,y] != null)
			view.HideGarnishSprite(garnishSprites[x,y]);
	}
	
	public void ShowSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;
		
		if(baseSprites[x,y] != null)
			view.ShowSprite (baseSprites[x,y]);
		if(garnishSprites[x,y] != null)
			view.ShowSprite (garnishSprites[x,y]);
	}
	
	public void SetupLocationSprite(Sprite s, int x, int y) {
		view.SetupLocationSprite(s, baseSprites[x,y], garnishSprites[x,y]);
	}

	public void DimSprite(int x, int y) {
		if(!mapData.CheckPosition(x, y))
			return;

		if(baseSprites[x,y] != null)
			view.DimSprite (baseSprites[x,y]);
		if(garnishSprites[x,y] != null)
			view.DimSprite(garnishSprites[x,y]);
	}
}

#warning "Really should move more functionality back here.."
public class MapCreator {
	public event Action createMapVisualsEvent = delegate{};
	public event Action finishedCreatingMapVisualsEvent = delegate{};
	public event Action<int, int> hideLocationSpriteEvent = delegate{};
	public event Action<int, int> showLocationSpriteEvent = delegate{};
	public event Action<int, int> dimLocationSpriteEvent = delegate{};
	public event Action<Sprite, int, int> setupSpecialLocationSpriteEvent = delegate{};
	
	public void CreateMap() {
		createMapVisualsEvent();
		finishedCreatingMapVisualsEvent();
	}
	
	public void HideLocation(int x, int y) {
		hideLocationSpriteEvent(x, y);
	}
	
	public void ShowLocation(int x, int y) {
		showLocationSpriteEvent(x, y);
	}
	
	public void DimLocation(int x, int y) {
		dimLocationSpriteEvent(x, y);
	}
	
	public void SetupLocationSprite(Sprite s, int x, int y) {
		setupSpecialLocationSpriteEvent(s, x, y);
	}
}
