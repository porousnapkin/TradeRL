using UnityEngine;
using strange.extensions.mediation.impl;

public class AIController : DesertView, Controller {
	[HideInInspector] public GameObject artGO;	
	[HideInInspector] public Character character;
	[HideInInspector] public CombatGraph combatGraph;
	
	public event System.Action KilledEvent  = delegate{};
	public event System.Action<Character> AttackEvent = delegate{};
	AIActioner actioner = new AIActioner();
	System.Action turnFinishedDelegate;

	protected override void Start() { 
		base.Start();

		artGO.transform.position = Grid.GetCharacterWorldPositionFromGridPositon((int)character.Position.x, (int)character.Position.y);

		character.health.DamagedEvent += (dam) => AnimationController.Damaged(artGO);
		character.health.KilledEvent += Killed;
	}

	void Killed() {
		AnimationController.Die(artGO, KilledAnimationFinished);
		KilledEvent();
		combatGraph.VacatePosition(character.Position);
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
		combatGraph.SetCharacterToPosition(character.Position, destination, character);
	}

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, character, target, attackFinished, () => AttackEvent(target));
	}

	public void EndTurn() {
		turnFinishedDelegate();
	}

	public Character GetCharacter() {
		return character;
	}
}

public class AIControllerMediator : Mediator {
	[Inject] public AIController view { private get; set; }
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public CombatModule combatModule { private get; set; }

	public override void OnRegister() {
		view.KilledEvent += Died;
		view.AttackEvent += Attack;
    }

    void Died() {
		textArea.AddDeathLine(view.character);
    }

    void Attack(Character target) {
    	combatModule.Attack(view.character, target);
    }

    public override void OnRemove() {
		view.KilledEvent -= Died;
		view.AttackEvent -= Attack;
    }
}