using UnityEngine;

public class AIAbilityData : ScriptableObject {
	public int cooldown = 5;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;

	public AIAbility Create(AIController controller) {
		return AIActionFactory.CreateAIAbility(controller, this, cooldown);
	}
}