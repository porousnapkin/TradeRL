using UnityEngine;
using System.Collections.Generic;

public class DamageModifierData
{
    public int damageMod = 1;
    public string damageModSource = "critical hit";
}

public class AttackData {
	public int baseDamage;
	public int totalDamage
    {
        get
        {
            int damage = baseDamage;
            damageModifiers.ForEach(dm => damage += dm.damageMod);
            return Mathf.Max(damage, 0);
        }
    }
    public bool isCrit;
    public List<DamageModifierData> damageModifiers = new List<DamageModifierData>();

	public Character attacker;
	public Character target;
}

public class AttackModule {
    public int minDamage = 10;
    public int maxDamage = 12;
    public CombatGraph combatGraph;
	public event System.Action<AttackData> modifyOutgoingAttack = delegate{};

    public AttackData CreateCustomAttack(Character attacker, Character target, int minDamage, int maxDamage, bool isRangedAttack, bool canCrit)
    {
        var data = new AttackData();
        data.attacker = attacker;
        data.target = target;
        data.baseDamage = Random.Range(minDamage, maxDamage);
        if(canCrit)
            AddCritMod(data, attacker, target);
        if(!isRangedAttack)
            AddDistanceMod(data, attacker, target);
        target.defenseModule.ModifyIncomingAttack(data);

		FinalizeAttackData(data);

        return data;
    }

    public AttackData CreateAttack(Character attacker, Character target, bool isRangedAttack)
    {
        return CreateCustomAttack(attacker, target, minDamage, maxDamage, isRangedAttack, true);
    }

	void FinalizeAttackData(AttackData outgoing) 
	{
		modifyOutgoingAttack(outgoing);
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

    void AddDistanceMod(AttackData data, Character attacker, Character target)
    {
        if (attacker.IsInMelee && target.IsInMelee) {
            return;
        }
        if (attacker.IsInMelee || target.IsInMelee)
        {
            data.damageModifiers.Add(new DamageModifierData
            {
                damageMod = Mathf.RoundToInt(-data.baseDamage * GlobalVariables.meleeDamageModForStepRemoved),
                damageModSource = "Distance (-" + Mathf.RoundToInt((-GlobalVariables.meleeDamageModForStepRemoved + 1) * 100) + "%)"
            });
        }
        else {
            data.damageModifiers.Add(new DamageModifierData
            {
                damageMod = Mathf.RoundToInt(-data.baseDamage * GlobalVariables.meleeDamageModForTwoStepsRemoved),
                damageModSource = "Distance (-" + Mathf.RoundToInt((-GlobalVariables.meleeDamageModForTwoStepsRemoved + 1) * 100) + "%)"
            });
        }
    }
}
