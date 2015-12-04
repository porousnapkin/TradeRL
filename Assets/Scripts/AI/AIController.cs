using UnityEngine;

public class AIController : MonoBehaviour, Controller {
	[HideInInspector] public GameObject artGO;	
	[HideInInspector] public Character character;
	[HideInInspector] public MapGraph mapGraph;
	
	public System.Action KilledEvent  = delegate{};
	AIActioner actioner = new AIActioner();
	System.Action turnFinishedDelegate;

	void Start() { 
		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.GraphPosition.x, (int)character.GraphPosition.y);

		character.health.DamagedEvent += (dam) => AnimationController.Damaged(artGO);
		character.health.KilledEvent += Killed;
	}

	void Killed() {
		AnimationController.Die(artGO, KilledAnimationFinished);
		KilledEvent();
		mapGraph.VacatePosition(character.GraphPosition);
		GlobalTextArea.Instance.AddDeathLine(character);
	}

	void KilledAnimationFinished() {
		GameObject.Destroy(artGO);
		GameObject.Destroy(gameObject);
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
		mapGraph.SetCharacterToPosition(character.GraphPosition, destination, character);
	}

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, character, target, attackFinished, () => CombatModule.Attack(character, target));
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}

	public Character GetCharacter() {
		return character;
	}
}