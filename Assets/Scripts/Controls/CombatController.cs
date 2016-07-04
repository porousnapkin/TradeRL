using UnityEngine;

public class CombatController
{
    [Inject] public GlobalTextArea textArea { private get; set; }
    [Inject] public CombatModule combatModule { private get; set; }

    public GameObject artGO;
    public Character character;
    public CombatActor combatActor { private get; set; }

    public event System.Action KilledEvent = delegate { };
    public event System.Action ActEvent = delegate { };
    public event System.Action<bool> MoveEvent = delegate { };
    int initiative = 0;
    System.Action turnFinishedDelegate;

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