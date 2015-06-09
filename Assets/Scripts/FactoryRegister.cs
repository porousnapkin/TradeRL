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
		EncounterFactory.mapGraph = mapGraph;
	}

	public static void SetFactionManager(FactionManager factionManager) {
		AIActionFactory.factionManager = factionManager;
		AICharacterFactory.factionManager = factionManager;
		CombatFactory.factionManager = factionManager;
	}

	public static void SetTurnManager(TurnManager turnManager) {
		AIActionFactory.turnManager = turnManager;
		AbilityFactory.turnManager = turnManager;
		AICharacterFactory.turnManager = turnManager;
		PlayerAbilityButtonFactory.turnManager = turnManager;
		CombatFactory.turnManager = turnManager;
	}

	public static void SetEffort(Effort effort) {
		AbilityFactory.effort = effort;	
	}

	public static void SetPlayerCharacter(Character player) {
		EncounterFactory.player = player;	
	}

	public static void SetPlayerController(PlayerController controller) {
		CombatFactory.playerController = controller;
	}
}