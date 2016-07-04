using UnityEngine; 

public class PlayerAbilityData : ScriptableObject {
	public int cooldown = 3;
	public int effortCost = 1;
	public AbilityTargetPickerData targetPicker;
	public AbilityActivatorData activator;
	public TargetedAnimationData animation;
	public string abilityName;

	public PlayerAbility Create(CombatController owner) {
		var ability = DesertContext.StrangeNew<PlayerAbility>();

        ability.controller = owner;
        ability.character = owner.character;
        ability.cooldown = cooldown;
        ability.effortCost = effortCost;
        ability.abilityName = abilityName;
        ability.targetPicker = targetPicker.Create(owner.character);
        ability.activator = activator.Create(owner);
        ability.animation = animation.Create(owner.character);
        ability.Setup();

		return ability;
	}
}