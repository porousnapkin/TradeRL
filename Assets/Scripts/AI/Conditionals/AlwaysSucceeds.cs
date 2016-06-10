using UnityEngine;
using System.Collections;

public class AlwaysSucceeds : CombatAIConditional {
    public bool Passes()
    {
        return true;
    }
}
