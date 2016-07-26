using System;
using System.Collections.Generic;

public class AbilityDamageModifier : AbilityModifier
{
	public int damageMod = 2;
	public string damageSource = "super critical hit";

	public void BeforeActivation(Character owner, List<Character> targets) 
	{
		owner.attackModule.modifyOutgoingAttack += ModifyAttack;
	}

	void ModifyAttack (AttackData attack)
	{
		var mod = new DamageModifierData();
		mod.damageMod = damageMod;
		mod.damageModSource = damageSource;
		attack.damageModifiers.Add(mod);
	}

	public void ActivationEnded(Character owner, List<Character> targets) 
	{
		owner.attackModule.modifyOutgoingAttack -= ModifyAttack;
	}
}

