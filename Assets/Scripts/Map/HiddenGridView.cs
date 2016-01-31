using UnityEngine;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using System;

public class HiddenGridView : DesertView {
	public int sightDistance = 5;
	public bool startHidden = true;

	public event Action<int,int> hideSpriteEvent = delegate{};
	public event Action<int,int> showSpriteEvent = delegate{};

	public void Setup(int width, int height) {
		if(!startHidden)
			for(int x = 0; x < width; x++)
				for(int y = 0; y < height; y++)
					hideSpriteEvent(x, y);
	}

	public void SetPosition(Vector2 position) {
		for(int x = (int)position.x - sightDistance; x < position.x + sightDistance; x++)
			for(int y = (int)position.y - sightDistance; y < position.y + sightDistance; y++)
				if(Vector2.Distance(position, new Vector2(x, y)) < sightDistance)
					showSpriteEvent(x, y);
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

		hiddenGrid.revealSpotsNearPositionEvent += view.SetPosition;

		mapCreator.finishedCreatingMapVisualsEvent += () =>  view.Setup(mapData.Width, mapData.Height);
	}

	public override void OnRemove() {
		view.hideSpriteEvent -= mapCreator.HideLocation;
		view.showSpriteEvent -= mapCreator.ShowLocation;

		hiddenGrid.revealSpotsNearPositionEvent -= view.SetPosition;
	}
}

#warning "Look into if any functionality can be moved into this class
public class HiddenGrid {
	public event System.Action<Vector2> revealSpotsNearPositionEvent = delegate{};
	
	public void RevealSpotsNearPosition(Vector2 pos) {
		revealSpotsNearPositionEvent(pos);
	}
}
