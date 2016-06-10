using UnityEngine;

public class AICombatController : CombatController {
	[Inject] public GlobalTextArea textArea { private get; set; }
	[Inject] public CombatModule combatModule { private get; set; }

	public GameObject artGO;	
	public Character character;
	
	public event System.Action KilledEvent  = delegate{};
    public event System.Action ActEvent = delegate {};
	public event System.Action<Character> AttackEvent = delegate{};
    public event System.Action<bool> MoveEvent = delegate { };
	AIActioner actioner = new AIActioner();
    int initiative = 0;
	System.Action turnFinishedDelegate;

	public AICombatController() {}

    public void Init()
    {
        character.health.DamagedEvent += (dam) => AnimationController.Damaged(artGO);
        character.health.KilledEvent += Killed;
    }

    public void SetWorldPosition(Vector3 position)
    {
        artGO.transform.position = position;
    }

    public void RollInitiative()
    {
        initiative = character.speed + Random.Range(0, GlobalVariables.maxInitiativeRoll);
    }

    public int GetInitiative()
    {
        return initiative;
    }

	void Killed() {
		AnimationController.Die(artGO, KilledAnimationFinished);
		KilledEvent();
		textArea.AddDeathLine(character);
	}

	void KilledAnimationFinished() {
		GameObject.Destroy(artGO);
	}

	public void AddAction(AIAction action) {
		actioner.AddAction(action);
	}

	public void BeginTurn(System.Action turnFinishedDelegate) {
		this.turnFinishedDelegate = turnFinishedDelegate;
		var action = actioner.PickAction();
		if(action != null)
			action.PerformAction(() => {});
		else
			Debug.LogError("AI ERROR: " + character.displayName + " had no possible action to perform! Make sure it has actions it can always perform.");

        ActEvent();
	}

    public void MoveToMelee()
    {
        if (character.IsInMelee)
            return;

        character.IsInMelee = true;
        MoveEvent(true);
    }

    public void MoveToRanged()
    {
        if (!character.IsInMelee)
            return;

        character.IsInMelee = false;
        MoveEvent(false);
    }

	public void Attack(Character target, System.Action attackFinished) {
		AnimationController.Attack(artGO, character, target, attackFinished, () => Hit(target));
	}

    void Hit(Character target)
    {
        AttackEvent(target);
    	combatModule.Attack(character, target);
    }

	public void EndTurn() {
		turnFinishedDelegate();
	}

	public Character GetCharacter() {
		return character;
	}
}