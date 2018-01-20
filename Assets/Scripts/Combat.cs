using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Combat {
    [Inject]public FactionManager factionManager { private get; set; }
    [Inject]public CombatTurnOrderVisualizer turnOrderVisualizer { private get; set; }
    [Inject]public PlayerCharacter player { private get; set; }
    [Inject]public ActiveLabelRequirements labelRequirements { private get; set; }

    List<CombatController> enemies;
    List<CombatController> allies;
    List<CombatController> combatants;
    HashSet<CombatController> diedThisRound = new HashSet<CombatController>();
    int combatIndex = 0;
    int charactersSetUp = 0;
    System.Action finishedCallback;

    public void Setup(List<CombatController> enemies, List<CombatController> allies, System.Action finishedCallback)
    {
        this.finishedCallback = finishedCallback;

        this.enemies = enemies;
        this.allies = allies;
        combatants = new List<CombatController>(enemies);
        combatants.AddRange(allies);
		combatants.ForEach(c => {
			c.GetCharacter().health.KilledEvent += () => CombatantDied(c);
			c.InitiativeModifiedEvent += UpdateTurnOrders;
		});

        GlobalEvents.CombatStarted();
    }

    public void SetupPlayerAmbush(System.Action callback)
    {
        player.PickAmbush(callback);
    }

    public void SetupEnemyAmbush(AIAbilityData ambushData, System.Action callback)
    {
        AIAbility ambush;
        if (ambushData == null)
            ambush = null;
        else
            ambush = ambushData.Create(enemies[0]);

        if(ambush != null)
            ambush.PerformAction(callback);
        else
            callback();
    }

    public void RunCombat()
    {
        UpdateTurnOrders();

        BeginRound();
    }

    void CombatantDied(CombatController c)
    {
        diedThisRound.Add(c);
        UpdateTurnOrders();
    }

    void UpdateTurnOrders()
    {
        var turnOrderCombatants = new List<CombatController>(combatants);
        turnOrderCombatants.RemoveAll(c => diedThisRound.Contains(c));
        SortCombatantsByInitiative(turnOrderCombatants);
        turnOrderVisualizer.TurnOrderAltered(0, turnOrderCombatants);
    }

    void BeginRound()
    {
        HandleInitiative();
        charactersSetUp = 0;
        combatants.ForEach(c => c.SetupForTurn(ACharacterFinishedSettingUp));
    }

    void ACharacterFinishedSettingUp()
    {
        charactersSetUp++;
        if (charactersSetUp >= combatants.Count)
            AllCharactersFinishedSettingUp();
    }

    void AllCharactersFinishedSettingUp()
    {
        HandleInitiative();
        combatIndex = 0;
        ActivateActiveCombatant();
    }

    void HandleInitiative()
    {
        turnOrderVisualizer.ClearThisTurnsTurnOrder();
        SortCombatantsByInitiative(combatants);
        turnOrderVisualizer.AddToTurnOrderDisplayStack(combatants);
    }

    void SortCombatantsByInitiative(List<CombatController> toSort)
    {
        toSort.Sort((a, b) => {
            var first = a.GetInitiative();
            var second = b.GetInitiative();
            //Guarantee two equal initiative characters will always sort the same way.
            if (first == second)
                return a.GetHashCode() - b.GetHashCode();
            else
                return second - first;
        });
    }
    
    void ActivateActiveCombatant()
    {
        if (combatIndex >= combatants.Count)
        {
            FinishRound();
            return;
        }

        //Skip combatants who are dead.
        var active = combatants[combatIndex];
        turnOrderVisualizer.SetActiveCharacter(active);
        if(diedThisRound.Contains(active))
        {
            combatIndex++;
            ActivateActiveCombatant();
            return;
        }

        active.Act(TurnFinished);
    }

    void TurnFinished()
    {
        combatIndex++;
        labelRequirements.ClearRequirements();
        labelRequirements.ClearLabels();

        if (factionManager.PlayerMembers.Count > 0 && factionManager.EnemyMembers.Count > 0)
            LeanTween.delayedCall(0.25f, ActivateActiveCombatant);
        else
            LeanTween.delayedCall(0.25f, FinishRound);
    }

    void FinishRound()
    {
        foreach(var dead in diedThisRound)
            combatants.Remove(dead);

        diedThisRound.Clear();

        if (factionManager.PlayerMembers.Count <= 0)
            LoseCombat();
        else if (factionManager.EnemyMembers.Count <= 0)
            WinCombat();
        else
            BeginRound();
    }

    void LoseCombat()
    {
        //TODO: Handle loss condition?
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void WinCombat()
    {
        //TODO:
        turnOrderVisualizer.Cleanup();
        finishedCallback();
        GlobalEvents.CombatEnded();
    }
}