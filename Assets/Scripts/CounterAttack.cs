using System.Collections.Generic;
using UnityEngine;

public class CounterAttack
{
    internal List<AbilityLabel> labels;

    public bool randomlyActivates { private get; set; }
    public float randomActivationChance { private get; set; }
    public bool onlyCountersMeleeAttacks { private get; set; }
    public int minDamage { private get; set; }
    public int maxDamage { private get; set; }
    public bool canCrit { private get; set; }

    public bool CanCounter(AttackData incomingAttack)
    {
        if (randomlyActivates && Random.value > randomActivationChance)
            return false;
        if (onlyCountersMeleeAttacks && !IsInMelee(incomingAttack))
            return false;
        return true;
    }

    bool IsInMelee(AttackData incomingAttack)
    {
        return incomingAttack.labels.Contains(AbilityLabel.Melee);
    }

    public AttackData CreateCounterAttack(AttackData incomingAttack)
    {
        var attackModule = incomingAttack.target.attackModule;

        var bd = new ModifiedBaseDamage();
        bd.minDamage = minDamage;
        bd.maxDamage = maxDamage;
        attackModule.OverrideBaseDamage(bd);
        attackModule.activeLabels = labels;
        var attack = attackModule.CreateAttack(incomingAttack.target, incomingAttack.attacker);
        attackModule.RemoveBaseDamageOverride();

        return attack;
    }
}