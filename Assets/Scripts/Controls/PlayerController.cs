using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerController : MonoBehaviour, Controller {
	static PlayerController instance;
	public static PlayerController Instance { get { return instance; }}
	
	public DesertPathfinder pathfinder;
	public GridHighlighter gridHighlighter;
	public GameObject characterPrefab;
	GameObject characterGO;
	bool isPathing = false;
	bool isMoving = false;
	bool isMyTurn = false;
	public GameObject CharacterGO { get { return characterGO; }}
	Vector2 lastDestination;
	public float travelTime = 0.25f;
	public HiddenGrid hiddenGrid;
	public MapGraph mapGraph;
	public Character playerCharacter;
	public CombatModule combatModule = new CombatModule();
	List<Vector2> path;
	System.Action turnFinishedDelegate;

	void Awake() {
		instance = this;

		characterGO = GameObject.Instantiate(characterPrefab) as GameObject;
		characterGO.transform.SetParent(transform);
	}

	void Start() {
		playerCharacter.WorldPosition = new Vector2(50, 50);
		characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)playerCharacter.WorldPosition.x, (int)playerCharacter.WorldPosition.y);

		hiddenGrid.SetPosition(playerCharacter.WorldPosition);
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
			characterGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)playerCharacter.WorldPosition.x, (int)playerCharacter.WorldPosition.y);
			DrawPathToPosition(lastDestination);
		}
	}

	void PathToPosition(Vector2 destination) {
		path = pathfinder.SearchForPathOnMainMap(playerCharacter.WorldPosition, destination);
		if(path.Count > 0)
			path.RemoveAt(0);
		isPathing = true;

		TravelOnPath();
	}

	void TravelOnPath() {
		isMoving = true;

		Character occupant = mapGraph.GetPositionOccupant((int)path[0].x, (int)path[0].y);
		if(occupant != null)
			Attack(occupant);
		else {
			Move(path[0]);
			turnFinishedDelegate();
		}

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
	}

	void Move(Vector2 position) {
		AnimationController.Move(characterGO, playerCharacter.WorldPosition, position);
		mapGraph.SetCharacterToPosition(playerCharacter.WorldPosition, position, playerCharacter);
		hiddenGrid.SetPosition(playerCharacter.WorldPosition);
	}

	void Attack(Character target) {
		AnimationController.Attack(characterGO, playerCharacter, target, turnFinishedDelegate, () => combatModule.Attack(playerCharacter, target));
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		isMyTurn = true;
		this.turnFinishedDelegate = turnFinishedDelegate;
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}
}