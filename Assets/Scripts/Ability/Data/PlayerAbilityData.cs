using UnityEngine; 

public class PlayerAbilityData : ScriptableObject {
	public int cooldown = 3;
	public int effortCost = 1;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public LocationTargetedAnimationData animation;
	public string abilityName;

	public PlayerAbility Create(PlayerController controller, Character owner) {
		PlayerAbility a = AbilityFactory.CreatePlayerAbility(owner);
		a.cooldown = cooldown;
		a.effortCost = effortCost;
		a.targetPicker = targetPicker.Create(owner);
		a.activator = activator.Create(owner);
		a.animation = animation.Create(owner);
		a.abilityName = abilityName;

		a.controller = controller;
		return a;
	}
}