using UnityEngine;

public class AbilityInitiativeModifierData : AbilityModifierData
{
	public int initiativeMod = 5;
	public string source = "Quick";
    public bool persistNewInitiative = true;

	public override AbilityModifier Create (CombatController owner)
	{
		var modifier = DesertContext.StrangeNew<AbilityInitiativeModifier>();
		modifier.initiativeModifier = initiativeMod;
		modifier.initiativeSource = source;
        modifier.owner = owner;
        modifier.persistNewInitiative = persistNewInitiative;

		return modifier;
	}
}
