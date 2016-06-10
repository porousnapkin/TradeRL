using UnityEngine;
using System.Collections.Generic;

public class Combat {
    [Inject] public FactionManager factionManager { private get; set; }

    List<CombatController> combatants;
    HashSet<CombatController> diedThisRound = new HashSet<CombatController>();
    int combatIndex = 0;

    public void RunCombat(List<CombatController> enemies, List<CombatController> allies)
    {
        combatants = new List<CombatController>(enemies);
        combatants.AddRange(allies);
        combatants.ForEach(c => c.GetCharacter().health.KilledEvent += () => CombatantDied(c));
        BeginRound();
    }
    
    void CombatantDied(CombatController c)
    {
        diedThisRound.Add(c);
    }

    void BeginRound()
    {
        HandleInitiative();
        combatIndex = 0;
        ActivateActiveCombatant();
    }

    void HandleInitiative()
    {
        combatants.ForEach(c => c.RollInitiative());
        combatants.Sort((a, b) => a.GetInitiative() - b.GetInitiative());
    }
    
    void ActivateActiveCombatant()
    {
        if (combatIndex >= combatants.Count)
        {
            FinishRound();
            return;
        }

        //Skip combatants who are dead.
        Debug.Log("combat index " + combatIndex);
        var active = combatants[combatIndex];
        if(diedThisRound.Contains(active))
        {
            combatIndex++;
            ActivateActiveCombatant();
            return;
        }

        var activeEnemies = factionManager.GetOpponents(active.GetCharacter());
        active.Attack(activeEnemies[Random.Range(0, activeEnemies.Count)], AttackFinished);
    }

    void AttackFinished()
    {
        combatIndex++;
        LeanTween.delayedCall(0.5f, ActivateActiveCombatant);
    }

    void FinishRound()
    {
        foreach(var dead in diedThisRound)
        {
            combatants.Remove(dead);
        }
        diedThisRound.Clear();

        if (factionManager.PlayerMembers.Count <= 0)
            LoseCombat();
        else if (factionManager.EnemyMembers.Count <= 0)
            WinCombat();
        else
            LeanTween.delayedCall(2.0f, BeginRound);
    }

    void LoseCombat()
    {
        //TODO:
    }

    void WinCombat()
    {
        //TODO:
    }
}