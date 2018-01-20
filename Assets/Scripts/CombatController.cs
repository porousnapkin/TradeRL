using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

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

    public class InitiativeModifier
    {
        public int amount;
        public bool removeAtTurnEnd = true;
        public string description = "";
    }
    List<InitiativeModifier> initiativeModifiers = new List<InitiativeModifier>();
    private Action turnFinishedDelegate;

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
        initiativeModifiers.Clear();
        character.health.DamagedEvent -= Damaged;
        character.health.KilledEvent -= Killed;
        GlobalEvents.CombatEnded -= Cleanup;
        combatActor.Cleanup();
    }

    public void SetWorldPosition(Vector3 position)
    {
        artGO.transform.position = position;
    }

    public int GetInitiative()
    {
        return character.speed + initiativeModifiers.Sum(modifier => modifier.amount);
    }

	public void AddInitiativeModifier(InitiativeModifier modifier)
	{
        initiativeModifiers.Add(modifier);
        InitiativeModifiedEvent();
    }

    public void RemoveInitiativeModifier(InitiativeModifier modifier)
    {
        initiativeModifiers.Remove(modifier);
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

    public void SetupForTurn(System.Action setupFinished)
    {
        combatActor.SetupAction(() =>
        {
            var attackData = new AttackData();
            if (character.attackModule.activeLabels.Contains(AbilityLabel.Attack))
                attackData = character.attackModule.SimulateAttack(character);
            character.broadcastPreparedAttackEvent(attackData);
            setupFinished();
        });
    }

    public void Act(System.Action actFinished)
    {
        this.turnFinishedDelegate = actFinished;
        GlobalEvents.CombatantTurnStart(character);

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
        initiativeModifiers.RemoveAll(i => i.removeAtTurnEnd);
        turnFinishedDelegate();
        character.actionFinishedEvent();
    }

    public Character GetCharacter()
    {
        return character;
    }
}