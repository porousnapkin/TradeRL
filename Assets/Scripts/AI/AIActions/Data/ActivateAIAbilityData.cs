public class ActivateAIAbilityData : AIActionData {
	public AIAbilityData ability;

	public override AIAction Create(AIController owner) {
		var action = new ActivateAIAbility();
		action.ability = ability.Create(owner);

		return action;
	}
}