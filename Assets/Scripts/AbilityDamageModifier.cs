using System.Collections.Generic;

public class AbilityDamageModifier : AbilityModifier
{
	public int damageMod = 2;
	public string damageSource = "super critical hit";

    public void PrepareActivation(List<Character> targets, System.Action callback)
    {
        targets.ForEach(t =>
        {
            t.attackModule.modifyOutgoingAttack += ModifyAttack;
        });
        callback();
    }

    public void BeforeActivation(List<Character> targets, System.Action callback) 
	{
        callback();
	}

	void ModifyAttack (AttackData attack)
	{
		var mod = new DamageModifierData();
		mod.damageMod = damageMod;
		mod.damageModSource = damageSource;
		attack.damageModifiers.Add(mod);
	}

	public void ActivationEnded(List<Character> targets, System.Action callback) 
	{
        targets.ForEach(t =>
        {
            t.attackModule.modifyOutgoingAttack -= ModifyAttack;
        });
        callback();
	}
}

