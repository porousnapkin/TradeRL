using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Combat {
    [Inject]public FactionManager factionManager { private get; set; }
    [Inject]public CombatTurnOrderVisualizer turnOrderVisualizer { private get; set; }
    [Inject]public PlayerCharacter player { private get; set; }
    [Inject]public ActiveLabelRequirements labelRequirements { private get; set; }

    List<CombatController> combatants;
    HashSet<CombatController> diedThisRound = new HashSet<CombatController>();
    int combatIndex = 0;
    System.Action finishedCallback;

    public void Setup(List<CombatController> enemies, List<CombatController> allies, System.Action finishedCallback)
    {
        this.finishedCallback = finishedCallback;

        combatants = new List<CombatController>(enemies);
        combatants.AddRange(allies);
		combatants.ForEach(c => {
			c.GetCharacter().health.KilledEvent += () => CombatantDied(c);
			c.InitiativeModifiedEvent += UpdateTurnOrders;
		});
        StackStartingInitiatives();

        GlobalEvents.CombatStarted();
    }

    public void SetupPlayerAmbush()
    {
        player.PickAmbush(RunCombat);
    }

    public void SetupEnemyAmbush(AIAbility ambush)
    {
        if(ambush != null)
            ambush.PerformAction(RunCombat);
        else
            RunCombat();
    }

    public void RunCombat()
    {
        SortCombatantsToStackDepth(combatants, 0);

        BeginRound();
    }

    void StackStartingInitiatives()
    {
        combatants.ForEach(c => c.RollInitiative());
        combatants.ForEach(c => c.PushInitiativeToStack());
        combatants.ForEach(c => c.PushInitiativeToStack());

        SortCombatantsToStackDepth(combatants, 0);
        turnOrderVisualizer.AddToTurnOrderDisplayStack(combatants);
        SortCombatantsToStackDepth(combatants, 1);
        turnOrderVisualizer.AddToTurnOrderDisplayStack(combatants);
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
        SortCombatantsToStackDepth(turnOrderCombatants, 0);
        turnOrderVisualizer.TurnOrderAltered(0, turnOrderCombatants);
        SortCombatantsToStackDepth(turnOrderCombatants, 1);
        turnOrderVisualizer.TurnOrderAltered(1, turnOrderCombatants);
    }

    void BeginRound()
    {
        combatIndex = 0;
        ActivateActiveCombatant();
    }

    void HandleInitiative()
    {
        PushNextTurnInitiative();
        SortCombatantsToStackDepth(combatants, 0);
    }

    void PushNextTurnInitiative()
    {
        combatants.ForEach(c => c.PushInitiativeToStack());
        SortCombatantsToStackDepth(combatants, 1);
        turnOrderVisualizer.AddToTurnOrderDisplayStack(combatants);
    }

    void SortCombatantsToStackDepth(List<CombatController> toSort, int stackDepth)
    {
        toSort.Sort((a, b) => {
            var first = a.GetInitiative(stackDepth);
            var second = b.GetInitiative(stackDepth);
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

        active.BeginTurn(TurnFinished);
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

        FinishTurnDisplay();

        if (factionManager.PlayerMembers.Count <= 0)
            LoseCombat();
        else if (factionManager.EnemyMembers.Count <= 0)
            WinCombat();
        else
            BeginRound();
    }

    void FinishTurnDisplay()
    {
        combatants.ForEach(c => c.ConsumeInitiative());
        turnOrderVisualizer.ClearThisTurnsTurnOrder();
        HandleInitiative();
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