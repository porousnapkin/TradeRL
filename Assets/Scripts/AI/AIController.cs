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
		var action = actioner.PickAction();
		if(action != null)
			action.PerformAction();
		else
			Debug.LogError("AI ERROR: " + character.displayName + " had no possible action to perform! Make sure it has actions it can always perform.");
	}

	public void Move(Vector2 destination) {
		AnimationController.Move(artGO, destination);
		mapGraph.SetCharacterToPosition(character.WorldPosition, destination, character);
	}

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, character, target, attackFinished, () => combatModule.Attack(character, target));
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}
}