using UnityEngine;

public class AIAbilityData : ScriptableObject {
	public int cooldown = 5;
	public AIAbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public TargetedAnimationData animation;
	public string displayMessage = "Charge";

    public AIAbility Create(CombatController controller)
    {
        AIAbility ability = DesertContext.StrangeNew<AIAbility>();

        ability.targetPicker = targetPicker.Create(controller.character);
        ability.activator = activator.Create(controller);
        ability.cooldown = cooldown;
        ability.controller = controller;
        ability.animation = animation.Create(controller.character);
        ability.displayMessage = displayMessage;

        return ability;
    }
}