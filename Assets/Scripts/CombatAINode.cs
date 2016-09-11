using UnityEngine;
using System.Collections.Generic;

public class CombatAINode {
    List<CombatAIConditional> conditionals = new List<CombatAIConditional>();
    List<AIAbility> abilities = new List<AIAbility>();
    int activeActionIndex = 0;
    System.Action callback;

    public void AddConditional(CombatAIConditional conditional)
    {
        conditionals.Add(conditional);
    }

    public void AddAbility(AIAbility ability)
    {
        abilities.Add(ability);
    }

    public bool CanUse()
    {
        bool passes = true;
        conditionals.ForEach(c =>
        {
            if (!c.Passes())
                passes = false;
        });
        abilities.ForEach(a =>
        {
            if (!a.CanUse())
                passes = false;
        });

        return passes;
    }

    public void Perform(System.Action callback)
    {
        activeActionIndex = 0;
        this.callback = callback;
        PerformActiveAction();
    }

    void PerformActiveAction()
    {
        if(activeActionIndex >= abilities.Count)
        {
            callback();
            return;
        }

        abilities[activeActionIndex].PerformAction(() =>
        {
            activeActionIndex++;
            PerformActiveAction();
        });
    }
}
