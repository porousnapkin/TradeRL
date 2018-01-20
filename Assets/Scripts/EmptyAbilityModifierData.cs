using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class EmptyAbilityModifierData : AbilityModifierData
{
    public override AbilityModifier Create(CombatController owner)
    {
        return new EmptyAbilityModifier();
    }
}

