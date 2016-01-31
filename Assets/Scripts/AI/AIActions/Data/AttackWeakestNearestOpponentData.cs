public class AttackWeakestNearestOpponentData : AIActionData {
	public override AIAction Create(AIController controller) {
		var action = DesertContext.StrangeNew<AttackWeakestNearestOpponent>();
		action.controller = controller;
		return action;
	}
}