using System.Collections.Generic;

public class AbilityDamageModifier : AbilityModifier
{
	public int damageMod = 2;
	public string damageSource = "super critical hit";

	public void BeforeActivation(CombatController owner, List<Character> targets) 
	{
		owner.character.attackModule.modifyOutgoingAttack += ModifyAttack;
	}

	void ModifyAttack (AttackData attack)
	{
		var mod = new DamageModifierData();
		mod.damageMod = damageMod;
		mod.damageModSource = damageSource;
		attack.damageModifiers.Add(mod);
	}

	public void ActivationEnded(CombatController owner, List<Character> targets) 
	{
		owner.character.attackModule.modifyOutgoingAttack -= ModifyAttack;
	}
}

