using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System;

public class HiddenGridView : DesertView {
	public int sightDistance = 5;
	public bool startHidden = true;

	public event Action<int,int> hideSpriteEvent = delegate{};
	public event Action<int,int> showSpriteEvent = delegate{};
	public event Action<int,int> dimSpriteEvent = delegate{};

	public void Setup(int width, int height) {
		if(!startHidden)
			for(int x = 0; x < width; x++)
				for(int y = 0; y < height; y++)
					hideSpriteEvent(x, y);
	}

	public void SetPosition(Vector2 position) {
		for(int x = (int)position.x - sightDistance - 1; x < position.x + sightDistance + 1; x++) {
			for(int y = (int)position.y - sightDistance - 1; y < position.y + sightDistance + 1; y++) {
				if(Vector2.Distance(position, new Vector2(x, y)) < sightDistance)
					showSpriteEvent(x, y);
				else
					dimSpriteEvent(x, y);
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

		hiddenGrid.revealSpotsNearPositionEvent += view.SetPosition;
		hiddenGrid.sightDistance = view.sightDistance;

		mapCreator.finishedCreatingMapVisualsEvent += () =>  view.Setup(mapData.Width, mapData.Height);
	}

	public override void OnRemove() {
		view.hideSpriteEvent -= mapCreator.HideLocation;
		view.showSpriteEvent -= mapCreator.ShowLocation;

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
}
