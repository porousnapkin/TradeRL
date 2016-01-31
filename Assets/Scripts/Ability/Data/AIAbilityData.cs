using UnityEngine;

public class AIAbilityData : ScriptableObject {
	public int cooldown = 5;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public LocationTargetedAnimationData animation;
	public string displayMessage = "Charge";
}