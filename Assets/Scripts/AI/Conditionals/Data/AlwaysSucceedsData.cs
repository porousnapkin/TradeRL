using UnityEngine;
using System.Collections;

public class AlwaysSucceedsData : CombatAIConditionalData {
    public override CombatAIConditional Create(AICombatController controller)
    {
        return new AlwaysSucceeds();
    }

}
