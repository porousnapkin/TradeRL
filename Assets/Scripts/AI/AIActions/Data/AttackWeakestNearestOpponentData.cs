public class AttackWeakestNearestOpponentData : AIActionData {

	public override AIAction Create(AIController owner) {
		return AIActionFactory.CreateAttackWeakestNearestOpponent(owner);
    }
}