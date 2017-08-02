using UnityEngine;
using System;
using System.Collections.Generic;
using strange.extensions.signal.impl;

public class MapPlayerController {
	[Inject(DesertPathfinder.MAP)] public DesertPathfinder pathfinder { private get; set; }
	[Inject] public MapGraph mapGraph {private get; set; }
	[Inject] public HiddenGrid hiddenGrid {private get; set; }
	[Inject] public GridInputCollector gridInputCollector {private get; set; }
	[Inject] public GameDate gameDate { private get; set; }
    [Inject] public MapData mapData { private get; set;  }
    [Inject] public KeyboardInput keyboardInput { private get; set; }
	
	public bool isPathing { get; set; }
	public Vector2 position { get; set; }
	Vector2 previousPosition;
    bool inCombat = false;
	
	public Signal movementStopped = new Signal();
	public Signal<Vector2, System.Action> animateMovement = new Signal<Vector2, System.Action>();
	public event Action<Vector2> teleportEvent = delegate{};
	
	List<Vector2> currentPath;
	bool onlyMoveOneStep = false;
	int stepsMoved = 0;
	
    //TODO: Attempting path removal. We'll see how it works.
	public List<Vector2> GetPathToPosition(Vector2 destination) {
        if (!mapData.IsImpassible(destination))
            return pathfinder.SearchForPathOnMainMap(position, destination);
        return new List<Vector2>();
	}
	
	[PostConstruct]
	public void Setup() {
        GlobalEvents.CombatStarted += CombatStarted;
        GlobalEvents.CombatEnded += CombatEnded;
        GlobalEvents.StoryStarted += StopMovement;
        GlobalEvents.EnemySpotted += StopMovement;
		gridInputCollector.mouseClickedPositionEvent += MouseClicked;
        keyboardInput.MoveKeyPressed += MoveKeyPressed;
	}

    ~MapPlayerController() {
        GlobalEvents.CombatStarted -= CombatStarted;
        GlobalEvents.CombatEnded -= CombatEnded;
        GlobalEvents.StoryStarted -= StopMovement;
        GlobalEvents.EnemySpotted += StopMovement;
		gridInputCollector.mouseClickedPositionEvent -= MouseClicked;
        keyboardInput.MoveKeyPressed -= MoveKeyPressed;
    }

    void CombatStarted()
    {
        inCombat = true;
        StopMovement();
    }

    void CombatEnded()
    {
        inCombat = false;
    }

    void MoveKeyPressed(Vector2 dir)
    {
        if (!inCombat)
            PathToPosition(position + dir);
    }

    void MouseClicked(Vector2 destination) {
        if (inCombat)
            return;

		if(isPathing) 
			StopMovement();
		else
			PathToPosition(destination);
	}
	
	public void StopMovement() {
        if(currentPath != null)
    		currentPath.Clear();
		isPathing = false;
	}
	
	public void PathToPosition(Vector2 destination) {
		if(isPathing)
			return;
		
		stepsMoved = 0;
		currentPath = GetPathToPosition(destination);
		if(currentPath.Count == 0)
			return;
		currentPath.RemoveAt(0);
		isPathing = true;
		
		if(currentPath.Count > 0)
			ContinuePathing();
	}
	
	public void MoveToPosition(Vector2 destination) {
		hiddenGrid.RevealSpotsNearPosition(destination);

		previousPosition = position;
		position = destination;
		animateMovement.Dispatch(destination, MoveAnimationFinished);

		mapGraph.PlayerPosition = destination;

        if(!DoesPositionHaveAnEvent())
            MovementFinished();
	}

    bool DoesPositionHaveAnEvent()
    {
        return mapGraph.DoesLocationHaveEvent((int)position.x, (int)position.y) || mapGraph.DoesLocationHaveTravelingStory((int)position.x, (int)position.y);
    }

    void MovementFinished()
    {
		//TODO: Ideally this is data driven. Would be nice to have dunes take 2 days, maybe other places take variable days, etc.
		gameDate.AdvanceDays(1);
    }
	
	void MoveAnimationFinished() {
        if (DoesPositionHaveAnEvent())
            HandlePositionEvent();
        else if(isPathing && currentPath.Count > 0)
            ContinuePathing();
        else if(isPathing)
            FinishPathing();
	}
	
	void HandlePositionEvent ()
	{
		StopMovement ();
		mapGraph.TriggerLocationEvent ((int)position.x, (int)position.y, MovementFinished);
	}
	
	void ContinuePathing() {
		if(stepsMoved > 0 && onlyMoveOneStep) {
			FinishPathing();
			return;
		}
		
		Vector2 nextSpot = currentPath[0];
		currentPath.RemoveAt(0);
		
		MoveToPosition(nextSpot);
		stepsMoved++;
	}
	
	void FinishPathing() {
		currentPath.Clear();
		isPathing = false;
	}

	public void MoveToPreviousPosition() {
		MoveToPosition(previousPosition);
	}
	
	public void LimitPathMovementToOneStep() {
		onlyMoveOneStep = true;
	}
	
	public void DontLimitPathMovement() {
		onlyMoveOneStep = false;
	}

	public void Teleport(Vector2 pos) {
		position = pos;
		previousPosition = pos;
		teleportEvent(pos);
	}
}
