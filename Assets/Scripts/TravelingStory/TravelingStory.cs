using UnityEngine;
using strange.extensions.signal.impl;

public class TravelingStory {
	[Inject] public StoryFactory storyFactory { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public EncounterFactory encounterFactory { private get; set; }
	public TravelingStoryAction action {private get; set;}
	public TravelingStoryAI ai {private get; set;}

	Vector2 WorldPosition { 
		get { return position; }
		set {
			mapGraph.TravelingStoryVacatesPosition(position);
			mapGraph.RemoveEventAtLocation((int)position.x, (int)position.y);
			position = value;

			if(mapGraph.playerPosition == position) {
				Activate(() => {});
			}
			else {
				mapGraph.SetEventForLocation((int)position.x, (int)position.y, (f) => Activate(f));
				mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
			}
		}
	}

	Vector2 position;
	public Signal<Vector2> movingToNewPositionSignal = new Signal<Vector2>();
	public Signal activatedSignal = new Signal();
	public Signal<Vector2> teleportSignal = new Signal<Vector2>();

	[PostConstruct]
	public void Setup () {
		gameDate.DaysPassedEvent += HandleDaysPassed;
	}

	public void Remove() {
		gameDate.DaysPassedEvent -= HandleDaysPassed;
		mapGraph.TravelingStoryVacatesPosition(position);
	}
	
	void HandleDaysPassed (int days) {
		if(!ai.DoesAct())
			return;

		WorldPosition = ai.GetMoveToPosition(WorldPosition);
		movingToNewPositionSignal.Dispatch(WorldPosition);

		ai.FinishedMove(WorldPosition);
	}
	
	void Activate(System.Action finishedDelegate) {
		action.Activate(finishedDelegate);

		activatedSignal.Dispatch();
		Remove();
	}
	
	public void TeleportToPosition(Vector2 position) {
		WorldPosition = position;
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		teleportSignal.Dispatch(WorldPosition);
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