using UnityEngine;
using System.Collections;

public class AttackAbilityData : AbilityActivatorData
{
    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<AttackAbility>();

        a.controller = owner;

        return a;
    }
}
