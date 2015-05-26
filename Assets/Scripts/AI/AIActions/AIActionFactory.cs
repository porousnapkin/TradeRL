public class AIActionFactory {
	public static DesertPathfinder pathfinder;
	public static MapGraph mapGraph;
	public static FactionManager factionManager;
	public static TurnManager turnManager;

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

	public static AIAbility CreateAIAbility(AIController controller, AIAbilityData data, int cooldown) {
		var ability = new AIAbility(turnManager);	
		ability.targetPicker = data.targetPicker.Create(controller.character);
		ability.activator = data.activator.Create(controller.character);
		ability.cooldown = cooldown;
		ability.controller = controller;
		return ability;
	}
}