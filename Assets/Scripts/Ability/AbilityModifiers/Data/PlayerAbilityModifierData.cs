using UnityEngine;

public class PlayerAbilityModifierData : ScriptableObject
{
	public int cooldown = 0;
	public string name = "";
	public AbilityModifierData abilityModifier;
	
	public PlayerAbilityModifier Create(CombatController controller)
	{
		var modifier = DesertContext.StrangeNew<PlayerAbilityModifier>();
		modifier.abilityModifier = abilityModifier.Create(controller);
		modifier.cooldown = cooldown;
		modifier.name = name;

		return modifier;
	}
}
