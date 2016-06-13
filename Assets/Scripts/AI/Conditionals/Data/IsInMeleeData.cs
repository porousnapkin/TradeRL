using UnityEngine;
using System.Collections;

public class IsInMeleeData : CombatAIConditionalData {
    protected override CombatAIConditional CreateConditional(AICombatController controller)
    {
        var conditional = new IsInMelee();
        conditional.controller = controller;
        return conditional;
    }
}
