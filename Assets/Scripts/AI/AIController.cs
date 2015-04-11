using UnityEngine;

public class AIController : MonoBehaviour, Controller {
	public GameObject artGO;	
	public Character character;
	public Character player;
	public DesertPathfinder pathfinder;
	public MapGraph mapGraph;
	NPCAI ai;
	CombatModule combatModule;
	System.Action turnFinishedDelegate;

	void Start() { 
		// var moveAI = new RandomMoveAI();
		// moveAI.controller = this;
		// moveAI.mapGraph = mapGraph;
		// ai = moveAI;

		var pathAI = new PathToTargetAI();
		pathAI.controller = this;
		pathAI.target = player;
		pathAI.pathfinder = pathfinder;
		pathAI.mapGraph = mapGraph;
		ai = pathAI;
		combatModule = new CombatModule();

		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.WorldPosition.x, (int)character.WorldPosition.y);
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		this.turnFinishedDelegate = turnFinishedDelegate;
		ai.RunTurn();
	}

	public void Move(Vector2 destination) {
		mapGraph.SetCharacterToPosition(character.WorldPosition, destination, character);
		AnimationController.Move(artGO, destination);
	}

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, target, attackFinished, () => combatModule.Attack(character, target));
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}
}