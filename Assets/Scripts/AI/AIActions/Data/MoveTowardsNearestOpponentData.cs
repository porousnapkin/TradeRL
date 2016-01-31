public class MoveTowardsNearestOpponentData : AIActionData {
	public override AIAction Create(AIController controller) {
		var action = DesertContext.StrangeNew<MoveTowardsNearestOpponent>();
		action.controller = controller;
		return action;
	}
}