using UnityEngine;
using System.Collections.Generic;

public class CombatAIData : ScriptableObject {
    public List<CombatAINodeData> nodes = new List<CombatAINodeData>();
    public AIAbilityData fallbackAbility;

    public CombatAI Create(CombatController controller)
    {
        var ai = new CombatAI();
        nodes.ForEach(n =>
        {
            ai.AddNode(n.Create(controller)); 
        });
        ai.fallbackAbility = fallbackAbility.Create(controller);

        return ai;
    }
}
