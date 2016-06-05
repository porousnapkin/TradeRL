public class ActivateAIAbilityData : AIActionData {
	public AIAbilityData ability;

	public override AIAction Create(AICombatController controller) {
		var action = DesertContext.StrangeNew<ActivateAIAbility>();
		action.ability = DesertContext.StrangeNew<AIAbility>();
		action.ability.Setup(controller, ability);
		return action;
	}
}