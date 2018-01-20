using UnityEngine;
using System.Collections.Generic;

public class AIAbilityData : ScriptableObject {
    public string abilityName;
    public string abilityDescription;
    public int initiativeMod = 0;
	public int cooldown = 0;
	public AIAbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public TargetedAnimationData animation;
	public string displayMessage = "Charge";
    public List<RestrictionData> restrictions = new List<RestrictionData>();
    public List<AbilityLabel> labels = new List<AbilityLabel>();

    public AIAbility Create(CombatController controller)
    {
        AIAbility ability = DesertContext.StrangeNew<AIAbility>();

        ability.abilityName = abilityName;
        ability.abilityDescription = abilityDescription;
        ability.targetPicker = targetPicker.Create(controller.character);
        ability.activator = activator.Create(controller);
        ability.cooldown = cooldown;
        ability.controller = controller;
        ability.animation = animation.Create(controller.character);
        ability.displayMessage = displayMessage;
        ability.restrictions = restrictions.ConvertAll(r => r.Create(controller.character));
        ability.labels = labels;
        ability.SetInitiativeModifiation(initiativeMod);

        return ability;
    }
}