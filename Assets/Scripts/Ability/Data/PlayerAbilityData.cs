using UnityEngine; 

public class PlayerAbilityData : ScriptableObject {
	public AbilityCostData cost;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;

	public PlayerAbility Create(Character owner) {
		PlayerAbility a = new PlayerAbility ();
		a.cost = cost.Create(owner);
		a.targetPicker = targetPicker.Create(owner);
		a.activator = activator.Create(owner);
		return a;
	}
}