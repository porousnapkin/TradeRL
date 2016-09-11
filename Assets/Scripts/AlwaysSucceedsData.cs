using UnityEngine;
using System.Collections;

public class AlwaysSucceedsData : CombatAIConditionalData {
    protected override CombatAIConditional CreateConditional(CombatController controller)
    {
        return new AlwaysSucceeds();
    }
}
