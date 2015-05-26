public class AIActionFactory {
	public static DesertPathfinder pathfinder;
	public static MapGraph mapGraph;
	public static FactionManager factionManager;
	public static Character debugTarget;

	public static MoveTowardsNearestOpponent CreateMoveTowardsNearestOpponent(AIController controller) {
		var action = new MoveTowardsNearestOpponent();
		action.controller = controller;
		action.pathfinder = pathfinder;
		action.mapGraph = mapGraph;
		action.factionManager = factionManager;
		return action;
	}

	public static AttackWeakestNearestOpponent CreateAttackWeakestNearestOpponent(AIController controller) {
		var action = new AttackWeakestNearestOpponent();
		action.controller = controller;
		action.pathfinder = pathfinder;
		action.mapGraph = mapGraph;
		action.factionManager = factionManager;
		return action;
	}
}