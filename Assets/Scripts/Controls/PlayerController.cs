using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour, Controller {
	static PlayerController instance;
	public static PlayerController Instance { get { return instance; }}
	
	GridHighlighter gridHighlighter;
	public GridHighlighter worldGridHighlighter;
	public GridHighlighter combatGridHighlighter;
	public GameObject characterPrefab;
	public float travelTime = 0.25f;
	public HiddenGrid hiddenGrid;
	GameObject worldCharacterGO;
	GameObject combatCharacterGO;
	Vector2 worldGraphPosition;
	bool isPathing = false;
	bool isMoving = false;
	bool isMyTurn = false;
	Vector2 lastDestination;
	List<Vector2> path;
	System.Action turnFinishedDelegate;
	Vector2 previousPosition;
	bool isInCombat = false;
	public GameObject ActiveCharacterGO { get { return isInCombat? combatCharacterGO : worldCharacterGO; }}
	public GameObject CombatCharacterGO { get { return combatCharacterGO; }}
	public GameObject WorldCharacterGO { get { return worldCharacterGO; }}

	DesertPathfinder pathfinder;
	[HideInInspector] public DesertPathfinder worldPathfinder;
	[HideInInspector] public DesertPathfinder combatPathfinder;
	MapGraph mapGraph;
	[HideInInspector] public MapGraph worldMapGraph;
	[HideInInspector] public MapGraph combatMapGraph;
	[HideInInspector] public Character playerCharacter;

	public event System.Action KilledEvent  = delegate{};
	public event System.Action<Vector2> LocationEnteredEvent = delegate{};

	void Awake() {
		instance = this;

		worldCharacterGO = CreateCharacterGO ();
		combatCharacterGO = CreateCharacterGO ();
		combatCharacterGO.SetActive (false);
	}

	GameObject CreateCharacterGO() {
		var cGO = GameObject.Instantiate(characterPrefab) as GameObject;
		cGO.transform.SetParent(transform);
		return cGO;
	}

	public void SetWorldPosition(Vector2 worldPosition) {
		worldGraphPosition = worldPosition;
	}

	void Start() {
		playerCharacter.health.DamagedEvent += (dam) => AnimationController.Damaged(worldCharacterGO);
		playerCharacter.health.DamagedEvent += (dam) => AnimationController.Damaged(combatCharacterGO);
		playerCharacter.health.KilledEvent += Killed;

		worldCharacterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)worldGraphPosition.x, 
		                                                                                    (int)worldGraphPosition.y);

		hiddenGrid.SetPosition(playerCharacter.GraphPosition);
	}

	void Killed() {
		AnimationController.Die(combatCharacterGO, KilledAnimationFinished);
		KilledEvent();
		GlobalTextArea.Instance.AddDeathLine(playerCharacter);
	}

	void KilledAnimationFinished() {
		//TODO: If not reseting, this is necessary.
		// GameObject.Destroy(characterGO);
		// GameObject.Destroy(combatCharacterGO);
		// GameObject.Destroy(gameObject);
		Invoke("Reset", 0.5f);
	}

	void Reset() {
		Application.LoadLevel(Application.loadedLevelName);
	}

	public void BeginCombat() {
		gridHighlighter = combatGridHighlighter;
		mapGraph = combatMapGraph;
		pathfinder = combatPathfinder;
	}

	public void FinishCombat() {
	}

	public void MouseOverPoint(Vector2 destination) {
		lastDestination = destination;
		if(!isPathing)
			DrawPathToPosition(destination);	
	}

	void DrawPathToPosition(Vector2 destination) {
		var path = pathfinder.SearchForPathOnMainMap(playerCharacter.GraphPosition, destination);
		gridHighlighter.DrawPath(path);
	}

	public void ClickedOnPosition(Vector2 destination) {
		if(!isMyTurn)
			return;
		if(isPathing)
			CancelPathing();
		else
			PathToPosition(destination);
	}

	void CancelPathing() {
		LeanTween.cancel(worldCharacterGO);
		LeanTween.cancel(combatCharacterGO);
		StopAllCoroutines();
		isPathing = false;
		worldCharacterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)worldGraphPosition.x, 
		                                                                                    (int)worldGraphPosition.y);
		combatCharacterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)playerCharacter.GraphPosition.x, 
		                                                                                     (int)playerCharacter.GraphPosition.y);
		DrawPathToPosition(lastDestination);
	}

	void PathToPosition(Vector2 destination) {
		path = pathfinder.SearchForPathOnMainMap(GetCurrentPosition(), destination);
		if(path.Count > 0)
			path.RemoveAt(0);

		if(isInCombat)
			gridHighlighter.HideHighlights();
		else
			isPathing = true;

		TravelOnPath();
	}

	void GetCurrentPosition() {
		if (isInCombat)
			return playerCharacter.GraphPosition;
		else
			return worldGraphPosition;
	}

	void TravelOnPath() {
		if(path.Count <= 0) {
			CancelPath();
			return;
		}

		Character occupant = mapGraph.GetPositionOccupant((int)path[0].x, (int)path[0].y);
		if(occupant != null)
			MoveIntoOccupiedSpace(occupant);
		else 
			Move(path[0], true);

		path.RemoveAt(0);
		if(path.Count <= 0)
			FinishedPathing();
	}

	void MoveIntoOccupiedSpace(Character occupant) {
		if(isInCombat)
			Attack(occupant);
		else
			CancelPath();
	}

	void CancelPath() {
		turnFinishedDelegate();
		FinishedPathing();
	}

	void FinishedPathing() {
		gridHighlighter.HideHighlights();
			
		isPathing = false;
	}

	void Update() {
		if(isMyTurn && !isMoving && isPathing)
			TravelOnPath();

		hiddenGrid.SetPosition(worldGraphPosition);
	}

	void Move(Vector2 position, bool endsTurn, float speedMod = 1.0f) {
		isMoving = true;
		AnimationController.Move(worldCharacterGO, position, () => FinishedMove(position), speedMod);
		previousPosition = playerCharacter.GraphPosition;
		mapGraph.SetCharacterToPosition(previousPosition, position, playerCharacter);
		if(!mapGraph.DoesLocationHaveEvent((int)position.x, (int)position.y)) {
			if(endsTurn)
				EndTurn();
		}
	}

	void FinishedMove(Vector2 position) {
		LocationEnteredEvent(position);
		if(mapGraph.DoesLocationHaveEvent((int)position.x, (int)position.y))
			mapGraph.TriggerLocationEvent((int)position.x, (int)position.y, () => {});
	
		isMoving = false;
	}

	public void ForceMoveToPosition(Vector2 position, float speedMod = 1.0f) {
		Move(position, false, speedMod);	
	}

	public void ForceMoveToPreviousPosition() {
		Move(previousPosition, false);
	}

	void Attack(Character target) {
		AnimationController.Attack(combatCharacterGO, playerCharacter, target, turnFinishedDelegate, () => CombatModule.Attack(playerCharacter, target));
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
}
