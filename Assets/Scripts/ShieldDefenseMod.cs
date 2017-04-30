using System;
using UnityEngine;

public class ShieldDefenseMod : AttackModifier
{
    int shieldAmount = 0;
    public int Value
    {
        get
        {
            return shieldAmount;
        }
        set
        {
            shieldAmount = value;
            ShieldChangedEvent();
        }
    }
    public event System.Action ShieldChangedEvent = delegate { };
    int lastUseAmount;

    public void ModifyAttack(AttackData attack)
    {
        if (shieldAmount <= 0)
            return;

        lastUseAmount = Mathf.Min(shieldAmount, attack.baseDamage);
        Value -= lastUseAmount;

        attack.damageModifiers.Add(new DamageModifierData
        {
            damageMod = -lastUseAmount,
            damageModSource = "shield"
        });
    }

    public void SendFinalizedAttack(AttackData attack)
    {
        var totalModifiers = attack.totalModifiers;
        var extraModifierBuffer = attack.baseDamage + totalModifiers;
        if (extraModifierBuffer < 0)
            Value -= lastUseAmount - extraModifierBuffer;
        else
            Value -= lastUseAmount;
    }

    public static ShieldDefenseMod Maker ()
    {
        return DesertContext.StrangeNew<ShieldDefenseMod>();
    }

    public const string defaultModName = "Shield";

    public static ShieldDefenseMod GetFrom(Character character)
    {
        return (ShieldDefenseMod)character.defenseModule.attackModifierSet.GetActiveModifier(ShieldDefenseMod.defaultModName, ShieldDefenseMod.Maker);
    }
}

