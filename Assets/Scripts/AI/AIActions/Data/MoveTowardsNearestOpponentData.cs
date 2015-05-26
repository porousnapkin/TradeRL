public class MoveTowardsNearestOpponentData : AIActionData {

	public override AIAction Create(AIController owner) {
		return AIActionFactory.CreateMoveTowardsNearestOpponent(owner);
    }
}