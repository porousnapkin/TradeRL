using UnityEngine; 

public class PlayerAbilityData : ScriptableObject {
	public int cooldown = 3;
	public int effortCost = 1;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public LocationTargetedAnimationData animation;
	public string abilityName;

	public PlayerAbility Create(Character owner) {
		var ability = DesertContext.StrangeNew<PlayerAbility>();
		ability.Setup(this, owner);

		return ability;
	}
}