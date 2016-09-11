using UnityEngine;
using System.Collections;

public class CustomAttackAbilityData : AbilityActivatorData
{
    public int minDamage = 10;
    public int maxDamage = 12;
    public bool canCrit = true;
    public bool isRangedAttack = false;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<CustomAttackAbility>();

        a.controller = owner;
        a.minDamage = minDamage;
        a.maxDamage = maxDamage;
        a.canCrit = canCrit;
        a.isRangedAttack = isRangedAttack;

        return a;
    }
}
