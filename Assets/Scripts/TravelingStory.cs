using UnityEngine;
using strange.extensions.signal.impl;

public interface TravelingStory
{
    Vector2 WorldPosition { get; set; }
    void Setup ();
    void Remove();
    void Activate(System.Action finishedDelegate);
    void TeleportToPosition(Vector2 position);
}

public interface TravelingStoryMediated
{
    event System.Action runningCloseAI;
    event System.Action runningFarAI;
    event System.Action<Vector2> movingToNewPositionSignal;
    event System.Action removeSignal;
    event System.Action<Vector2> teleportSignal;
    event System.Action<bool> isVisibleSignal;
}

public class TravelingStoryImpl : TravelingStory, TravelingStoryMediated
{
	[Inject] public StoryFactory storyFactory { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public EncounterFactory encounterFactory { private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
	public TravelingStoryAction action {private get; set;}
	public TravelingStoryAI ai {private get; set;}

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
	public event System.Action runningCloseAI = delegate{};
	public event System.Action runningFarAI = delegate{};
    public event System.Action<Vector2> movingToNewPositionSignal = delegate { };
	public event System.Action removeSignal = delegate { };
    public event System.Action<Vector2> teleportSignal = delegate { };
	public event System.Action<bool> isVisibleSignal = delegate { };

	public void Setup () {
		ai.runningCloseAI += () =>  runningCloseAI();
		ai.runningFarAI += () =>  runningFarAI();
		gameDate.DaysPassedEvent += HandleDaysPassed;
		VisibilityCheck();
	}

	void VisibilityCheck() {
		isVisibleSignal(IsVisible());
	}

	bool IsVisible() {
		return hiddenGrid.IsSpotVisible(WorldPosition);
	}

	public void Remove() {
		gameDate.DaysPassedEvent -= HandleDaysPassed;
		mapGraph.TravelingStoryVacatesPosition(position);
		removeSignal();
	}
	
	void HandleDaysPassed (int days) {
		VisibilityCheck();

		if(!IsVisible() || !ai.DoesAct()) {
			ai.FinishedMove(WorldPosition);
			return;
		}

		WorldPosition = ai.GetMoveToPosition(WorldPosition);
		movingToNewPositionSignal(WorldPosition);

		ai.FinishedMove(WorldPosition);
	}
	
	public void Activate(System.Action finishedDelegate) {
		Remove();

		action.Activate(finishedDelegate);
	}
	
	public void TeleportToPosition(Vector2 position) {
		WorldPosition = position;
		mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		teleportSignal(WorldPosition);
		ai.FinishedMove(WorldPosition);
	}
}