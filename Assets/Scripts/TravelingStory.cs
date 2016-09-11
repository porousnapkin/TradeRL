using UnityEngine;
using strange.extensions.signal.impl;

public class TravelingStory {
	[Inject] public StoryFactory storyFactory { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public EncounterFactory encounterFactory { private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
	public TravelingStoryAction action {private get; set;}
	public TravelingStoryAI ai {private get; set;}
	public event System.Action runningCloseAI = delegate{};
	public event System.Action runningFarAI = delegate{};

	public Vector2 WorldPosition { 
		get { return position; }
		set {
			mapGraph.TravelingStoryVacatesPosition(position);
			position = value;

			if(mapGraph.PlayerPosition == position) 
				Activate(() => {});
			else 
				mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		}
	}

	Vector2 position;
	public Signal<Vector2> movingToNewPositionSignal = new Signal<Vector2>();
	public Signal removeSignal = new Signal();
	public Signal<Vector2> teleportSignal = new Signal<Vector2>();
	public Signal<bool> isVisibleSignal = new Signal<bool>();

	public void Setup () {
		ai.runningCloseAI += () =>  runningCloseAI();
		ai.runningFarAI += () =>  runningFarAI();
		gameDate.DaysPassedEvent += HandleDaysPassed;
		VisibilityCheck();
	}

	void VisibilityCheck() {
		isVisibleSignal.Dispatch(IsVisible());
	}

	bool IsVisible() {
		return hiddenGrid.IsSpotVisible(WorldPosition);
	}

	public void Remove() {
		gameDate.DaysPassedEvent -= HandleDaysPassed;
		mapGraph.TravelingStoryVacatesPosition(position);
		removeSignal.Dispatch();
	}
	
	void HandleDaysPassed (int days) {
		VisibilityCheck();

		if(!IsVisible() || !ai.DoesAct()) {
			ai.FinishedMove(WorldPosition);
			return;
		}

		WorldPosition = ai.GetMoveToPosition(WorldPosition);
		movingToNewPositionSignal.Dispatch(WorldPosition);

		ai.FinishedMove(WorldPosition);
	}
	
	public void Activate(System.Action finishedDelegate) {
		Remove();

		action.Activate(finishedDelegate);
	}
	
	public void TeleportToPosition(Vector2 position) {
		WorldPosition = position;
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		teleportSignal.Dispatch(WorldPosition);
		ai.FinishedMove(WorldPosition);
	}
}

public interface TravelingStoryAction {
	void Activate(System.Action finishedDelegate);
}

public class TravelingStoryBeginStoryAction : TravelingStoryAction {
	[Inject] public StoryFactory storyFactory {private get; set;}
	public StoryData story {private get; set;}

	public void Activate(System.Action finishedDelegate) {
		storyFactory.CreateStory(story, finishedDelegate);
	}
}

public class TravelingStoryBeginCombatAction : TravelingStoryAction {
	[Inject] public EncounterFactory encounterFactory {private get; set;}
	public CombatEncounterData combatData {private get; set;}

	public void Activate(System.Action finishedDelegate) {
		encounterFactory.CreateEncounter(combatData);
	}
}