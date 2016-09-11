using UnityEngine;

public class AbilityDamageModifierData : AbilityModifierData
{
	public int damageMod = 10;
	public string damageSource = "Slam";

	public override AbilityModifier Create (CombatController owner)
	{
		var modifier = DesertContext.StrangeNew<AbilityDamageModifier>();
		modifier.damageMod = damageMod;
		modifier.damageSource = damageSource;

		return modifier;
	}
}
