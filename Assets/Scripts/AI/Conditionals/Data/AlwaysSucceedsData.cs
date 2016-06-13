using UnityEngine;
using System.Collections;

public class AlwaysSucceedsData : CombatAIConditionalData {
    protected override CombatAIConditional CreateConditional(AICombatController controller)
    {
        return new AlwaysSucceeds();
    }
}
