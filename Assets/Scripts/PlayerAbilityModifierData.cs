using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityModifierData : ScriptableObject
{
	public int cooldown = 0;
	public string abilityName = "";
    public string description = "";
	public AbilityModifierData abilityModifier;
    public List<AbilityCostData> costs = new List<AbilityCostData>();
    public bool hasLabelRequirements = false;
    public List<AbilityLabel> labelRequirements = new List<AbilityLabel>();
	
	public PlayerAbilityModifier Create(CombatController controller)
	{
		var modifier = DesertContext.StrangeNew<PlayerAbilityModifier>();
		modifier.abilityModifier = abilityModifier.Create(controller);
		modifier.cooldown = cooldown;
		modifier.name = abilityName;
        modifier.description = description;
        modifier.costs = costs.ConvertAll(c => c.Create(controller.character));
        modifier.hasLabelRequirements = hasLabelRequirements;
        modifier.labelRequirements = labelRequirements;

		return modifier;
	}
}
