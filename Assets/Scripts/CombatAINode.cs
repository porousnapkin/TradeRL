using UnityEngine;
using System.Collections.Generic;

public class CombatAINode {
    List<CombatAIConditional> conditionals = new List<CombatAIConditional>();
    List<AIAbility> abilities = new List<AIAbility>();
    AIAbility activeAbility;
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
        bool passesConditionals = conditionals.TrueForAll(c => c.Passes());
        bool hasAUsableAbility = abilities.Exists(a => a.CanUse());
        return passesConditionals && hasAUsableAbility;
    }

    public void SetupAction(System.Action<AIAbility> callback)
    {
        var usableAbilities = new List<AIAbility>(abilities);
        usableAbilities.RemoveAll(a => !a.CanUse());

        activeAbility = usableAbilities[Random.Range(0, usableAbilities.Count)];
        activeAbility.Prepare(() => callback(activeAbility));
    }

    public void Perform(System.Action callback)
    {
        if (activeAbility.CanUse())
            activeAbility.PerformAction(callback);
        else
            callback();
    }
}
