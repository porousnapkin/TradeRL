using UnityEngine;
using System.Collections.Generic;

public class CombatController
{
    [Inject] public GlobalTextArea textArea { private get; set; }
    [Inject] public CombatModule combatModule { private get; set; }

    public GameObject artGO { get; set; }
    public Character character { get; set; }
    public CombatActor combatActor { private get; set; }

    public event System.Action KilledEvent = delegate { };
    public event System.Action ActEvent = delegate { };
    public event System.Action<bool> MoveEvent = delegate { };
	public event System.Action InitiativeModifiedEvent = delegate{};
    List<int> initiativeStack = new List<int>();
    System.Action turnFinishedDelegate;

    public void Init()
    {
        character.controller = this;
        character.health.DamagedEvent += Damaged;
        character.health.KilledEvent += Killed;
        GlobalEvents.CombatEnded += Cleanup;
    }

    void Damaged(int amount)
    {
        AnimationController.Damaged(artGO);
    }

    void Cleanup()
    {
        character.health.DamagedEvent -= Damaged;
        character.health.KilledEvent -= Killed;
        GlobalEvents.CombatEnded -= Cleanup;
        combatActor.Cleanup();
    }

    public void SetWorldPosition(Vector3 position)
    {
        artGO.transform.position = position;
    }

    public void RollAndPushInitiativeToStack()
    {
        var newInitiative = character.speed + Random.Range(0, GlobalVariables.maxInitiativeRoll);
        initiativeStack.Add(newInitiative);
    }

    public int GetInitiative(int depth)
    {
        return initiativeStack[depth];
    }

    public void ConsumeInitiative()
    {
        initiativeStack.RemoveAt(0);
    }

	public void SetInitiative(int depth, int initiative)
	{
		initiativeStack[depth] = initiative;
		InitiativeModifiedEvent();
	}

    void Killed()
    {
        AnimationController.Die(artGO, KilledAnimationFinished);
        KilledEvent();
        textArea.AddDeathLine(character);
    }

    void KilledAnimationFinished()
    {
        GameObject.Destroy(artGO);
    }

    public void BeginTurn(System.Action turnFinishedDelegate)
    {
        this.turnFinishedDelegate = turnFinishedDelegate;

        combatActor.Act(EndTurn);

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

    public void EndTurn()
    {
        turnFinishedDelegate();
    }

    public Character GetCharacter()
    {
        return character;
    }
}