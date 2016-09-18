using UnityEngine;

public class Location {
	[Inject] public MapCreator mapCreator {private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public StoryFactory storyFactory {private get; set; }
	[Inject] public GlobalTextArea textArea {private get; set;}
	[Inject] public MapPlayerController controller {private get; set;}
	[Inject] public GameDate gameDate {private get; set;}
	[Inject] public MapPlayerController mapPlayerController {private get; set;}
	[Inject] public HiddenGrid hiddenGrid {private get; set; }

	public int x;
	public int y;
	Vector2 locationVector;
	public LocationData data;
	int cooldownCounter = 0;
	bool secondStory = false;
	bool discovered = false;
    float discoveryChance = 1.0f;//0.05f;

	public void Setup() {
        //SetUndiscovered();
        SetDiscovered();
  
		locationVector = new Vector2(x,y); 
		gameDate.DaysPassedEvent += DaysPassed;
	}

	void DaysPassed(int days) {
		if( !discovered &&
			hiddenGrid.IsSpotVisible(locationVector) &&
			!gameDate.HasADailyEventOccuredToday &&
			Random.value < discoveryChance )
			SetDiscovered();

	}

	void SetUndiscovered() {
		if(discovered)
			mapGraph.RemoveEventAtLocation(x, y);

		discovered = false;
	}

	void SetDiscovered() {
		if(discovered)
			return;

        mapCreator.SetupLocationSprite(data.art, x, y);

		mapGraph.SetEventForLocation(x, y, (f) => LocationEntered(f));

        //TODO: Do we want this?
//		textArea.AddLine(data.discoverText);
		mapPlayerController.StopMovement();
		gameDate.DailyEventOccured();

		discovered = true;
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
	}

	void CooldownAdvance() {
		cooldownCounter++;
		if(cooldownCounter <= 0)
			CooldownFinished();
	}

	void CooldownFinished() {
		secondStory = false;

		mapCreator.ShowLocation(x, y);
	}

	void SetInactive() {
		mapCreator.DimLocation(x, y);
		mapGraph.RemoveEventAtLocation(x, y);
	}
}
