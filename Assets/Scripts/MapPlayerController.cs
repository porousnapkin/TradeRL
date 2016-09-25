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
        var adjacentPos = Grid.GetAdjacentValidPositions((int)position.x, (int)position.y);
        adjacentPos.RemoveAll(v => mapData.IsHill(v));
        adjacentPos.Sort((f, s) => (int)((Vector2.Distance(f, destination) - Vector2.Distance(s, destination)) * 100));
        List<Vector2> path = new List<Vector2>();
        path.Add(position);
        if (adjacentPos.Count > 0)
            path.Add(adjacentPos[0]);
        return path;
	}
	
	[PostConstruct]
	public void Setup() {
        GlobalEvents.CombatStarted += () => inCombat = true;
        GlobalEvents.CombatEnded += () => inCombat = false;
		gridInputCollector.mouseClickedPositionEvent += MouseClicked;
	}
	
	~MapPlayerController() {
		gridInputCollector.mouseClickedPositionEvent -= MouseClicked;
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

		//TODO: Ideally this is data driven. Would be nice to have dunes take 2 days, maybe other places take variable days, etc.
		gameDate.AdvanceDays(1);
	}
	
	void MoveAnimationFinished() {
		if (mapGraph.DoesLocationHaveEvent ((int)position.x, (int)position.y) || mapGraph.DoesLocationHaveTravelingStory((int)position.x, (int)position.y))
			HandlePositionEvent ();
		else if(isPathing && currentPath.Count > 0)
			ContinuePathing();
		else if(isPathing)
			FinishPathing();
	}
	
	void HandlePositionEvent ()
	{
		StopMovement ();
		mapGraph.TriggerLocationEvent ((int)position.x, (int)position.y, () =>  {});
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
