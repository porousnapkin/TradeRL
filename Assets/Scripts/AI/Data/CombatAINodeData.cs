using UnityEngine;
using System.Collections.Generic;

public class CombatAINodeData : ScriptableObject {
    public List<CombatAIConditionalData> conditionals = new List<CombatAIConditionalData>();
    public List<AIAbilityData> abilities = new List<AIAbilityData>();

    public CombatAINode Create(AICombatController controller)
    {
        var node = new CombatAINode();
        conditionals.ForEach(c =>
        {
            node.AddConditional(c.Create(controller));
        });
        abilities.ForEach(a =>
        {
            node.AddAbility(a.Create(controller));
        });

        return node;
    }
}
