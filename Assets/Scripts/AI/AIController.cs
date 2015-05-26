using UnityEngine;

public class AIController : MonoBehaviour, Controller {
	public GameObject artGO;	
	public Character character;
	public MapGraph mapGraph;
	public CombatModule combatModule = new CombatModule();
	AIActioner actioner = new AIActioner();
	System.Action turnFinishedDelegate;

	void Start() { 
		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.WorldPosition.x, (int)character.WorldPosition.y);
	}

	public void AddAction(AIAction action) {
		actioner.AddAction(action);
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		this.turnFinishedDelegate = turnFinishedDelegate;
		actioner.PickAction().PerformAction();
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