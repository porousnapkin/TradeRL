using UnityEngine;

public class FactoryRegister {
	public static void SetPathfinder(DesertPathfinder pathfinder) {
		AIActionFactory.pathfinder = pathfinder;	
		AbilityFactory.pathfinding = pathfinder;
		AbilityTargetPickerFactory.pathfinding = pathfinder;
	}

	public static void SetMapGraph(MapGraph mapGraph) {
		AIActionFactory.mapGraph = mapGraph;
		AICharacterFactory.mapGraph = mapGraph;
		AbilityFactory.mapGraph = mapGraph;
		AbilityTargetPickerFactory.mapGraph = mapGraph;
		AnimationFactory.mapGraph = mapGraph;
	}

	public static void SetFactionManager(FactionManager factionManager) {
		AIActionFactory.factionManager = factionManager;
		AICharacterFactory.factionManager = factionManager;
	}

	public static void SetTurnManager(TurnManager turnManager) {
		AIActionFactory.turnManager = turnManager;
		AbilityFactory.turnManager = turnManager;
		AICharacterFactory.turnManager = turnManager;
		PlayerAbilityButtonFactory.turnManager = turnManager;
	}

	public static void SetEffort(Effort effort) {
		AbilityFactory.effort = effort;	
	}
}