using UnityEngine;
using System.Collections;

public class RandomlySucceeds : CombatAIConditional {
    public float successChance;

    protected override bool Check()
    {
        return Random.value < successChance;
    }
}
