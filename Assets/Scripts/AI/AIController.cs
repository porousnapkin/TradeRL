using UnityEngine;
using System.Collections;

public class AIController : MonoBehaviour, Controller {
	public GameObject artGO;	
	public Character character;
	public Character player;
	public DesertPathfinder pathfinder;
	NPCAI ai;
	System.Action turnFinishedDelegate;

	void Start() { 
		// var moveAI = new RandomMoveAI();
		// moveAI.controller = this;
		// ai = moveAI;

		var pathAI = new PathToTargetAI();
		pathAI.controller = this;
		pathAI.target = player;
		pathAI.pathfinder = pathfinder;
		ai = pathAI;

		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.WorldPosition.x, (int)character.WorldPosition.y);
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		this.turnFinishedDelegate = turnFinishedDelegate;
		ai.RunTurn();
	}

	public void Move(Vector2 destination) {
		character.WorldPosition = destination;
		LeanTween.move(artGO, Grid.GetCharacterWorldPositionFromGridPositon((int)destination.x, (int)destination.y), GlobalVariables.travelTime)
			.setEase(LeanTweenType.easeOutQuad);
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}
}