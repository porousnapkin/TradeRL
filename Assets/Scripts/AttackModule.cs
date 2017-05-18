using UnityEngine;
using System.Collections.Generic;

public class DamageModifierData
{
    public int damageMod = 1;
    public string damageModSource = "critical hit";
}

public class AttackData {
	public int baseDamage;
    public int totalModifiers
    {
        get
        {
            int total = 0;
            damageModifiers.ForEach(dm => total += dm.damageMod);
            return total;
        }
    }
	public int totalDamage
    {
        get
        {
            int damage = baseDamage;
            damage += totalModifiers;
            return Mathf.Max(damage, 0);
        }
    }
    public bool isCrit;
    public List<DamageModifierData> damageModifiers = new List<DamageModifierData>();
    public List<AbilityLabel> labels;

    public Character attacker;
	public Character target;
}

public class AttackModule {
    public int minDamage = 10;
    public int maxDamage = 12;
	public event System.Action<AttackData> modifyOutgoingAttack = delegate{};
    public AttackModifierSet attackModifierSet = new AttackModifierSet();
    public List<AbilityLabel> activeLabels;
    List<CounterAttack> counterAttacks = new List<CounterAttack>();

    public AttackData CreateCustomAttack(Character attacker, Character target, int minDamage, int maxDamage, bool canCrit)
    {
        var data = new AttackData();
        data.attacker = attacker;
        data.target = target;
        data.baseDamage = Random.Range(minDamage, maxDamage+1);
        if(canCrit)
            AddCritMod(data, attacker, target);

		FinalizeAttackData(data);

        target.defenseModule.ModifyIncomingAttack(data);
        data.labels = activeLabels;

        return data;
    }

    public AttackData CreateAttack(Character attacker, Character target)
    {
        return CreateCustomAttack(attacker, target, minDamage, maxDamage, false);
    }

	void FinalizeAttackData(AttackData outgoing) 
	{
        attackModifierSet.ApplyAttackModifier(outgoing);
		modifyOutgoingAttack(outgoing);
        attackModifierSet.SendFinalizedAttack(outgoing);
	}

    void AddCritMod(AttackData data, Character attacker, Character target)
    {
        data.isCrit = Random.value < GlobalVariables.baseCritChance;

        if (data.isCrit)
        {
            data.damageModifiers.Add(new DamageModifierData
            {
                damageMod = Mathf.RoundToInt(Random.Range(minDamage, maxDamage) * GlobalVariables.critDamageBonus),
                damageModSource = "Critical hit"
            });
        }
    }

    public List<AttackData> CreateCounterAttacks(AttackData incomingAttack)
    {
        List<AttackData> outAttacks = new List<AttackData>();
        counterAttacks.ForEach(c =>
        {
            if (c.CanCounter(incomingAttack))
                outAttacks.Add(c.CreateCounterAttack(incomingAttack));
        });
        return outAttacks;
    }

    public void AddCounterAttack(CounterAttack counterAttack)
    {
        counterAttacks.Add(counterAttack);
    }

    public void RemoveCounterAttack(CounterAttack counterAttack)
    {
        counterAttacks.Remove(counterAttack);
    }
}