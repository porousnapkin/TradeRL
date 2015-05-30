using UnityEngine;

public class AIAbilityData : ScriptableObject {
	public int cooldown = 5;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public LocationTargetedAnimationData animation;

	public AIAbility Create(AIController controller) {
		var ability = AIActionFactory.CreateAIAbility(controller, this, cooldown);
		ability.animation = animation.Create();
		return ability;
	}
}