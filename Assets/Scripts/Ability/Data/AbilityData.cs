using UnityEngine; 

public class AbilityData : ScriptableObject {
	public AbilityCostData cost;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;

	public Ability Create(Character owner) {
		Ability a = new Ability();
		a.cost = cost.Create(owner);
		a.targetPicker = targetPicker.Create(owner);
		a.activator = activator.Create(owner);
		return a;
	}
}