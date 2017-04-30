using UnityEngine;

public class DodgeDefenseMod : AttackModifier
{
    public float dodgeChance = 0f;

    public void ModifyAttack(AttackData attack)
    {
        if (Random.value > dodgeChance)
            return;

        attack.damageModifiers.Add(new DamageModifierData
        {
            damageMod = -Mathf.RoundToInt(attack.baseDamage / 2.0f),
            damageModSource = "dodge"
        });
    }

    public void SendFinalizedAttack(AttackData attack)
    {
    }

    public static DodgeDefenseMod Maker()
    {
        return DesertContext.StrangeNew<DodgeDefenseMod>();
    }
}

