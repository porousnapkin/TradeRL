using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlayerAbilityData : ScriptableObject 
{
    public int initiativeMod = 0;
	public int cooldown = 0;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public TargetedAnimationData animation;
	public string abilityName;
    public string description;
    public List<RestrictionData> restrictions = new List<RestrictionData>();
    public List<AbilityCostData> costs = new List<AbilityCostData>();
    public List<AbilityLabel> labels = new List<AbilityLabel>();

	public PlayerAbility Create(CombatController owner) {
		var ability = DesertContext.StrangeNew<PlayerAbility>();

        ability.controller = owner;
        ability.character = owner.character;
        ability.cooldown = cooldown;
        ability.abilityName = abilityName;
        ability.abilityDescription = description;
        ability.targetPicker = targetPicker.Create(owner.character);
        ability.activator = activator.Create(owner);
        ability.animation = animation.Create(owner.character);
        ability.restrictions = restrictions.ConvertAll(r => r.Create(owner.character));
        ability.costs = costs.ConvertAll(c => c.Create(owner.character));
        ability.labels = labels;
        ability.SetInitiativeModifiation(initiativeMod);
        ability.Setup();

		return ability;
	}
}