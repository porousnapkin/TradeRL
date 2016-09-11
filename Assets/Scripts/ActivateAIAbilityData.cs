public class ActivateAIAbilityData : AIActionData {
	public AIAbilityData ability;

	public override AIAction Create(CombatController controller) {
		var action = DesertContext.StrangeNew<ActivateAIAbility>();
        action.ability = ability.Create(controller);
		action.ability.Setup();
		return action;
	}
}