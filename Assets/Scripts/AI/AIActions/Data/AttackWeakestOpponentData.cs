public class AttackWeakestOpponentData : AIActionData {
	public override AIAction Create(AICombatController controller) {
		var action = DesertContext.StrangeNew<AttackWeakestOpponent>();
		action.controller = controller;
		return action;
	}
}