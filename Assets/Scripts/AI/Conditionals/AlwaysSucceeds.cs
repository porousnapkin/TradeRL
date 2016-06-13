using UnityEngine;
using System.Collections;

public class AlwaysSucceeds : CombatAIConditional {
    protected override bool Check()
    {
        return true;
    }
}
