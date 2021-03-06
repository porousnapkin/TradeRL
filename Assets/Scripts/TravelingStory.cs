using UnityEngine;

public interface TravelingStory
{
    Vector2 WorldPosition { get; set; }
    void Setup ();
    void Remove();
    void Activate(System.Action finishedDelegate, bool playerInitiated);
    void TeleportToPosition(Vector2 position);
}

public interface TravelingStoryMediated
{
    event System.Action runningCloseAI;
    event System.Action runningFarAI;
    event System.Action<Vector2, System.Action> movingToNewPositionSignal;
    event System.Action removeSignal;
    event System.Action<Vector2> teleportSignal;
    event System.Action<bool> isVisibleSignal;
}

public class TravelingStoryController : TravelingStory, TravelingStoryMediated
{
	[Inject] public StoryFactory storyFactory { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
    [Inject] public MapPlayerController mapPlayer { private get; set; }
    [Inject] public PlayerCharacter playerCharacter { private get; set; }
    [Inject] public GridMouseOverPopup popup { private get; set; }
	public TravelingStoryAction action {private get; set;}
	public TravelingStoryAI ai {private get; set;}

    public int stealthRating { private get; set; }
    bool isRevealed = false;
	Vector2 position;
    bool removed = false;
    bool activateAfterMoving = false;
	public Vector2 WorldPosition { 
		get { return position; }
		set
        {
            if (position == value)
                return;

            mapGraph.TravelingStoryVacatesPosition(position);

            popup.Clear(position, popupLocation);
            position = value;
            popupLocation = popup.ReserveSpace(position);

            RecordPopupText();

            if (mapGraph.PlayerPosition == position) {
                activateAfterMoving = true;
                mapPlayer.StopMovement();
                mapPlayer.BlockMovement();
            }
            else 
				mapGraph.SetTravelingStoryToPosition(WorldPosition, this);
		}
	}

    int popupLocation = 0;
    string descriptiveName = "Traveling Story Popup";
    public string PopupText
    {
        get { return descriptiveName + "\n" + ai.Describe();  }
        set
        {
            descriptiveName = value;
            RecordPopupText();
        }
    }

	public event System.Action runningCloseAI = delegate{};
	public event System.Action runningFarAI = delegate{};
    public event System.Action<Vector2, System.Action> movingToNewPositionSignal = delegate { };
	public event System.Action removeSignal = delegate { };
    public event System.Action<Vector2> teleportSignal = delegate { };
	public event System.Action<bool> isVisibleSignal = delegate { };

    [PostConstruct]
    public void PostConstruct()
    {
        popupLocation = popup.ReserveSpace(WorldPosition);
    }

	public void Setup () {
		ai.runningCloseAI += () =>  runningCloseAI();
		ai.runningFarAI += () =>  runningFarAI();
		gameDate.DaysPassedEvent += HandleDaysPassed;
		VisibilityCheck();
	}

	void VisibilityCheck()
	{
	    CheckForRevealed();
		isVisibleSignal(IsVisible() && isRevealed);
	}

    void CheckForRevealed()
    {
        if (isRevealed)
        {
            isRevealed = IsVisible();
        }
        else if(IsVisible())
        {
            int revealedDistance = CalculateRevealedDistance();
            isRevealed = Vector2.Distance(mapPlayer.position, WorldPosition) <= revealedDistance;
            if (isRevealed)
            {
                GlobalEvents.EnemySpotted();
                RecordPopupText();
            }
        }
    }

    int CalculateRevealedDistance()
    {
        var sightDistance = hiddenGrid.GetSightDistance();
        return Mathf.Min(sightDistance, sightDistance - stealthRating + playerCharacter.GetSpotBonus());
    }

    bool IsVisible() {
		return hiddenGrid.IsSpotVisible(WorldPosition);
	}

	public void Remove() {
        removed = true;

        gameDate.DaysPassedEvent -= HandleDaysPassed;
		mapGraph.TravelingStoryVacatesPosition(position);
		removeSignal();
	}
	
	void HandleDaysPassed (int days) {
		VisibilityCheck();

		if(!IsVisible() || !isRevealed || !ai.DoesAct()) {
			ai.FinishedMove(WorldPosition);
			return;
		}

		var newPos = ai.GetMoveToPosition(WorldPosition);
	    WorldPosition = newPos;
		movingToNewPositionSignal(newPos, () => MoveAnimFinished(newPos));
	}

    void MoveAnimFinished(Vector2 newPos)
    {
		ai.FinishedMove(WorldPosition);
        if (activateAfterMoving)
        {
            Activate(() => { }, false);
            mapPlayer.UnblockMovement();
            activateAfterMoving = false;
        }
    }
	
	public void Activate(System.Action finishedDelegate, bool playerInitiated) {
        if (removed)
            return;
		Remove();

		action.Activate(finishedDelegate, playerInitiated);
	}
	
	public void TeleportToPosition(Vector2 position) {
		WorldPosition = position;
		teleportSignal(WorldPosition);
		ai.FinishedMove(WorldPosition);
    }

    void RecordPopupText()
    {
        if(isRevealed)
            popup.Record(WorldPosition, PopupText, popupLocation);
    }
}