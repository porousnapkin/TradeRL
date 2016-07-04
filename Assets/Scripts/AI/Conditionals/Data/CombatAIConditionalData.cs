using UnityEngine;
using System.Collections;

public abstract class CombatAIConditionalData : ScriptableObject {
    public bool not;

    public CombatAIConditional Create(CombatController controller)
    {
        var conditional = CreateConditional(controller);
        conditional.not = not;

        return conditional;
    }

    protected abstract CombatAIConditional CreateConditional(CombatController controller);
}
