using UnityEngine;
using System.Collections;

public class RandomlySucceedsData : CombatAIConditionalData {
    [Range(0.0f, 1.0f)]
    public float successChance;

    protected override CombatAIConditional CreateConditional(AICombatController controller)
    {
        var conditional = new RandomlySucceeds();
        conditional.successChance = successChance;
        return conditional;
    }
}
