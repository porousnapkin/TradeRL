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

    public void ModifyAttack(AttackData attack)
    {
        if (shieldAmount <= 0)
            return;

        var shieldUseAmount = Mathf.Min(shieldAmount, attack.baseDamage);
        Value -= shieldUseAmount;

        attack.damageModifiers.Add(new DamageModifierData
        {
            damageMod = -shieldUseAmount,
            damageModSource = "shield"
        });
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

