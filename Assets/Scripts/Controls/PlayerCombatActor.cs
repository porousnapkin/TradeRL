using UnityEngine;
using System.Collections;

public class PlayerCombatActor : CombatActor {
    public PlayerAbility debugPlayerAbility;

    public void Act(System.Action callback)
    {
        debugPlayerAbility.Activate(callback);
    }
}
