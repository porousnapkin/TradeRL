using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour, Controller {
	static PlayerController instance;
	public static PlayerController Instance { get { return instance; }}
	
	public GridHighlighter gridHighlighter;
	public GameObject characterPrefab;
	public float travelTime = 0.25f;
	public HiddenGrid hiddenGrid;
	GameObject characterGO;
	bool isPathing = false;
	bool isMoving = false;
	bool isMyTurn = false;
	bool onlyMoveOneStep = false;
	Vector2 lastDestination;
	List<Vector2> path;
	System.Action turnFinishedDelegate;
	Vector2 previousPosition;
	public GameObject CharacterGO { get { return characterGO; }}

	[HideInInspector] public DesertPathfinder pathfinder;
	[HideInInspector] public MapGraph mapGraph;
	[HideInInspector] public Character playerCharacter;

	public event System.Action KilledEvent  = delegate{};
	public event System.Action<Vector2> LocationEnteredEvent = delegate{};

	void Awake() {
		instance = this;

		characterGO = GameObject.Instantiate(characterPrefab) as GameObject;
		characterGO.transform.SetParent(transform);
	}

	void Start() {
		playerCharacter.health.DamagedEvent += (dam) => AnimationController.Damaged(characterGO);
		playerCharacter.health.KilledEvent += Killed;

		characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)playerCharacter.WorldPosition.x, 
			(int)playerCharacter.WorldPosition.y);

		hiddenGrid.SetPosition(playerCharacter.WorldPosition);
	}

	void Killed() {
		AnimationController.Die(characterGO, KilledAnimationFinished);
		KilledEvent();
		GlobalTextArea.Instance.AddDeathLine(playerCharacter);
	}

	void KilledAnimationFinished() {
		//TODO: If not reseting, this is necessary.
		// GameObject.Destroy(characterGO);
		// GameObject.Destroy(gameObject);
		Invoke("Reset", 0.5f);
	}

	void Reset() {
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void MouseOverPoint(Vector2 destination) {
		lastDestination = destination;
		if(!isPathing)
			DrawPathToPosition(destination);	
	}

	void DrawPathToPosition(Vector2 destination) {
		var path = pathfinder.SearchForPathOnMainMap(playerCharacter.WorldPosition, destination);
		gridHighlighter.DrawPath(path);
	}

	public void ClickedOnPosition(Vector2 destination) {
		if(!isMyTurn)
			return;
		if(!isPathing)
			PathToPosition(destination);
		else {
			LeanTween.cancel(characterGO);
			StopAllCoroutines();
			isPathing = false;
			characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)playerCharacter.WorldPosition.x, 
				(int)playerCharacter.WorldPosition.y);
			DrawPathToPosition(lastDestination);
		}
	}

	void PathToPosition(Vector2 destination) {
		path = pathfinder.SearchForPathOnMainMap(playerCharacter.WorldPosition, destination);
		if(path.Count > 0)
			path.RemoveAt(0);

		if(onlyMoveOneStep)
			gridHighlighter.HideHighlights();
		else
			isPathing = true;

		TravelOnPath();
	}

	void TravelOnPath() {
		isMoving = true;

		if(path.Count <= 0) {
			turnFinishedDelegate();
			FinishedPathing();
			return;
		}

		Character occupant = mapGraph.GetPositionOccupant((int)path[0].x, (int)path[0].y);
		if(occupant != null)
			Attack(occupant);
		else 
			Move(path[0], true);

		path.RemoveAt(0);
		if(path.Count <= 0)
			FinishedPathing();

		Invoke("FinishedMove", travelTime);
	}

	void FinishedPathing() {
		gridHighlighter.HideHighlights();
			
		isPathing = false;
	}

	void FinishedMove() {
		isMoving = false;
	}

	void Update() {
		if(isMyTurn && !isMoving && isPathing)
			TravelOnPath();

		hiddenGrid.SetPosition(playerCharacter.WorldPosition);
	}

	void Move(Vector2 position, bool endsTurn, float speedMod = 1.0f) {
		AnimationController.Move(characterGO, position, () => FinishedMove(position), speedMod);
		previousPosition = playerCharacter.WorldPosition;
		mapGraph.SetCharacterToPosition(playerCharacter.WorldPosition, position, playerCharacter);
		if(!mapGraph.DoesLocationHaveEvent((int)position.x, (int)position.y)) {
			hiddenGrid.SetPosition(playerCharacter.WorldPosition);
			if(endsTurn)
				EndTurn();
		}
	}

	void FinishedMove(Vector2 position) {
		LocationEnteredEvent(position);
		if(mapGraph.DoesLocationHaveEvent((int)position.x, (int)position.y))
			mapGraph.TriggerLocationEvent((int)position.x, (int)position.y, () => {});
	}

	public void ForceMoveToPosition(Vector2 position, float speedMod = 1.0f) {
		Move(position, false, speedMod);	
	}

	public void ForceMoveToPreviousPosition() {
		Move(previousPosition, false);
	}

	void Attack(Character target) {
		AnimationController.Attack(characterGO, playerCharacter, target, turnFinishedDelegate, () => CombatModule.Attack(playerCharacter, target));
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		isMyTurn = true;
		this.turnFinishedDelegate = turnFinishedDelegate;
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}

	public Character GetCharacter() {
		return playerCharacter;
	}

	public void LimitPathMovementToOneStep() {
		onlyMoveOneStep = true;
	}

	public void DontLimitPathMovement() {
		onlyMoveOneStep = false;
	}
}
