using UnityEngine;
using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;

public class HiddenGridView : DesertView {
	public int sightDistance = 5;
	public bool startHidden = true;
    public int disableSubGridSize = 10;
    private int width;
    private int height;
    private int subGridWidth = 0;
    private int subGridHeight = 0;
    private List<Vector2> activeSubGrids = new List<Vector2>();

	public event Action<int,int> hideSpriteEvent = delegate{};
	public event Action<int,int> showSpriteEvent = delegate{};
	public event Action<int,int> dimSpriteEvent = delegate{};
    public event Action<int, int> disableSpriteEvent = delegate { };
    public event Action<int, int> enableSpriteEvent = delegate { };

	public void Setup(int width, int height) {
        this.width = width;
        this.height = height;
		if(!startHidden)
			for(int x = 0; x < width; x++)
				for(int y = 0; y < height; y++)
					disableSpriteEvent(x, y);

        subGridWidth = Mathf.CeilToInt(width / (float)disableSubGridSize);
        subGridHeight = Mathf.CeilToInt(width / (float)disableSubGridSize);
	}

	public void SetPosition(Vector2 position) {
        Vector2 subGridPosition = new Vector2(Mathf.FloorToInt(position.x / disableSubGridSize), Mathf.FloorToInt(position.y / disableSubGridSize));
        var newActiveSubGrids = GetActiveSubgridPositions(subGridPosition);

        foreach (var checkSubGridPosition in activeSubGrids)
            if (!newActiveSubGrids.Contains(checkSubGridPosition))
                DisableSubGrid(checkSubGridPosition);
        foreach (var checkSubGridPosition in newActiveSubGrids)
            if (!activeSubGrids.Contains(checkSubGridPosition))
                EnableSubGrid(checkSubGridPosition);

        activeSubGrids = newActiveSubGrids;

        for (int x = (int)position.x - sightDistance - 1; x < position.x + sightDistance + 1; x++) {
			for(int y = (int)position.y - sightDistance - 1; y < position.y + sightDistance + 1; y++) {
				if(Vector2.Distance(position, new Vector2(x, y)) < sightDistance)
					showSpriteEvent(x, y);
				else
					dimSpriteEvent(x, y);
			}
		}
	}

    private List<Vector2> GetActiveSubgridPositions(Vector2 subGridPosition)
    {
        var newActive = new List<Vector2>();

        for (int xMod = -1; xMod <= 1; xMod++)
        {
            for (int yMod = -1; yMod <= 1; yMod++)
            {
                var x = subGridPosition.x + xMod;
                var y = subGridPosition.y + yMod;

                if (x >= 0 && x < subGridWidth && y >= 0 && y < subGridHeight)
                {
                    newActive.Add(new Vector2(x, y));
                }
            }
        }

        return newActive;
    }

    private void DisableSubGrid(Vector2 checkSubGridPosition)
    {
        for(int subX = 0; subX < disableSubGridSize; subX++)
        {
            for(int subY = 0; subY < disableSubGridSize; subY++)
            {
                var x = (int)checkSubGridPosition.x * disableSubGridSize + subX;
                var y = (int)checkSubGridPosition.y * disableSubGridSize + subY;

                if (x >= 0 && x < width && y >= 0 && y < height)
                    disableSpriteEvent(x, y);
            }
        }
    }

    private void EnableSubGrid(Vector2 checkSubGridPosition)
    {
        for (int subX = 0; subX < disableSubGridSize; subX++)
        {
            for (int subY = 0; subY < disableSubGridSize; subY++)
            {
                var x = (int)checkSubGridPosition.x * disableSubGridSize + subX;
                var y = (int)checkSubGridPosition.y * disableSubGridSize + subY;

                if (x >= 0 && x < width && y >= 0 && y < height)
                    enableSpriteEvent(x, y);
            }
        }
    }
}

public class HiddenGridMediator : Mediator {
	[Inject] public HiddenGridView view { private get; set; }
	[Inject] public MapCreator mapCreator { private get; set; }
	[Inject] public MapData mapData { private get; set; }
	[Inject] public HiddenGrid hiddenGrid { private get; set; }
	
	public override void OnRegister ()
	{
		view.hideSpriteEvent += mapCreator.HideLocation;
		view.showSpriteEvent += mapCreator.ShowLocation;
		view.dimSpriteEvent += mapCreator.DimLocation;
		view.disableSpriteEvent += mapCreator.DisableLocationSprite;
		view.enableSpriteEvent += mapCreator.EnableLocationSprite;

		hiddenGrid.revealSpotsNearPositionEvent += view.SetPosition;
		hiddenGrid.sightDistance = view.sightDistance;

		mapCreator.finishedCreatingMapVisualsEvent += () =>  view.Setup(mapData.Width, mapData.Height);
	}

	public override void OnRemove() {
		view.hideSpriteEvent -= mapCreator.HideLocation;
		view.showSpriteEvent -= mapCreator.ShowLocation;
		view.dimSpriteEvent -= mapCreator.DimLocation;
		view.disableSpriteEvent -= mapCreator.DisableLocationSprite;

		hiddenGrid.revealSpotsNearPositionEvent -= view.SetPosition;
	}
}

public class HiddenGrid {
	[Inject] public MapPlayerController player { private get; set; }
	public event System.Action<Vector2> revealSpotsNearPositionEvent = delegate{};
	public int sightDistance { private get; set; }
	
	public void RevealSpotsNearPosition(Vector2 pos) {
		revealSpotsNearPositionEvent(pos);
	}

	public bool IsSpotVisible(Vector2 pos) {
		return Vector2.Distance(pos, player.position) < sightDistance;
	}

    public int GetSightDistance()
    {
        return sightDistance;
    }
}
