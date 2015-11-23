using UnityEngine;
using System.Collections;

public class Location {
	public int x;
	public int y;
	public MapGraph mapGraph;
	public MapCreator mapCreator;
	public LocationData data;
	public TurnManager turnManager;
	int cooldownCounter = 0;
	bool secondStory = false;

	public void Setup() {
		SetActive();
	}

	void SetActive() {
		mapCreator.ShowSprite(x, y);
		mapCreator.SetupLocationSprite(data.art, x, y);
		mapGraph.SetEventForLocation(x, y, (f) => LocationEntered(f), false);
	}

	void LocationEntered(System.Action finishedAction) {
		if(secondStory)
			data.secondStory.Create (finishedAction);
		else
			data.firstStory.Create (finishedAction);

		if(data.type == LocationType.ActiveStoryWithCooldown && cooldownCounter <= 0)
			SetupOnCooldown();
		else if(data.type == LocationType.OneOffStory)
			SetInactive();
	}

	void SetupOnCooldown() {
		secondStory = true;
		mapCreator.DimSprite(x, y);
		cooldownCounter = data.cooldownTurns;
		turnManager.TurnEndedEvent += CooldownAdvance;
	}

	void CooldownAdvance() {
		cooldownCounter++;
		if(cooldownCounter <= 0)
			CooldownFinished();
	}

	void CooldownFinished() {
		secondStory = false;
		turnManager.TurnEndedEvent += CooldownAdvance;
		mapCreator.ShowSprite(x, y);
	}

	void SetInactive() {
		mapCreator.DimSprite(x, y);
		mapGraph.RemoveEventAtLocation(x, y, false);
	}
}
