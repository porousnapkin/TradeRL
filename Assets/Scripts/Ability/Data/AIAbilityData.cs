using UnityEngine;

public class AIAbilityData : ScriptableObject {
	public int cooldown = 5;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public LocationTargetedAnimationData animation;
	public string displayMessage = "Charge";

    public AIAbility Create(AICombatController controller)
    {
        AIAbility ability = DesertContext.StrangeNew<AIAbility>();

        ability.targetPicker = targetPicker.Create(controller.character);
        ability.activator = activator.Create(controller.character);
        ability.cooldown = cooldown;
        ability.controller = controller;
        ability.animation = animation.Create(controller.character);
        ability.displayMessage = displayMessage;

        return ability;
    }
}