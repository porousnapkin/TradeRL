using UnityEngine;

public class AIController : MonoBehaviour, Controller {
	public GameObject artGO;	
	public Character character;
	public Character player;
	public DesertPathfinder pathfinder;
	public MapGraph mapGraph;
	public CombatModule combatModule = new CombatModule();
	NPCAI ai;
	System.Action turnFinishedDelegate;

	void Start() { 
		var pathAI = new PathToTargetAI();
		pathAI.controller = this;
		pathAI.target = player;
		pathAI.pathfinder = pathfinder;
		pathAI.mapGraph = mapGraph;
		ai = pathAI;

		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.WorldPosition.x, (int)character.WorldPosition.y);
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		this.turnFinishedDelegate = turnFinishedDelegate;
		ai.RunTurn();
	}

	public void Move(Vector2 destination) {
		AnimationController.Move(artGO, character.WorldPosition, destination);
		mapGraph.SetCharacterToPosition(character.WorldPosition, destination, character);
	}

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, character, target, attackFinished, () => combatModule.Attack(character, target));
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}
}