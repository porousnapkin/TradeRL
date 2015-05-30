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
	}

	public static void SetDooberFactory(DooberFactory dooberFactory) {
		AICharacterFactory.dooberFactory = dooberFactory;
	}

	public static void SetHealthDisplayPrefab(GameObject healthDisplayPrefab) { 
		AICharacterFactory.healthDisplayPrefab = healthDisplayPrefab;
	}
}