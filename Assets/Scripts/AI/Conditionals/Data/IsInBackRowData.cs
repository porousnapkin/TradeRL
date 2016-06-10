using UnityEngine;
using System.Collections;

public class IsInBackRowData : CombatAIConditionalData {
    public override CombatAIConditional Create(AICombatController controller)
    {
        var conditional = new IsInBackRow();
        conditional.controller = controller;
        return conditional;
    }
}
