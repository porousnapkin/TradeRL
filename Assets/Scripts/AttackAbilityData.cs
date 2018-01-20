using System;
using System.Collections.Generic;

public class AttackAbilityData : AbilityActivatorData
{
    public int numberOfAttacksPerTarget = 1;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<AttackAbility>();

        a.numberOfAttacksPerTarget = numberOfAttacksPerTarget;
        a.controller = owner;

        return a;
    }
}

