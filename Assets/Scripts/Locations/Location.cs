using UnityEngine;
using System.Collections;

public class Location {
	[Inject] public MapCreator mapCreator {private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public TurnManager turnManager { private get; set; }
	[Inject] public StoryFactory storyFactory {private get; set; }

	public int x;
	public int y;
	public LocationData data;
	int cooldownCounter = 0;
	bool secondStory = false;

	public void Setup() {
		SetActive();
	}

	void SetActive() {
		mapCreator.ShowLocation(x, y);
		mapCreator.SetupLocationSprite(data.art, x, y);

		mapGraph.SetEventForLocation(x, y, (f) => LocationEntered(f));
	}

	void LocationEntered(System.Action finishedAction) {
		if(secondStory)
			storyFactory.CreateStory(data.secondStory, finishedAction);
		else
			storyFactory.CreateStory(data.firstStory, finishedAction);

		if(data.activationType == LocationType.ActiveStoryWithCooldown && cooldownCounter <= 0)
			SetupOnCooldown();
		else if(data.activationType  == LocationType.OneOffStory)
			SetInactive();
	}

	void SetupOnCooldown() {
		secondStory = true;

		mapCreator.DimLocation(x, y);
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

		mapCreator.ShowLocation(x, y);
	}

	void SetInactive() {
		mapCreator.DimLocation(x, y);
		mapGraph.RemoveEventAtLocation(x, y);
	}
}
