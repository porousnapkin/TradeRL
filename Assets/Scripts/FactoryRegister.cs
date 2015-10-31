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
		CombatFactory.mapGraph = mapGraph;
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
		StoryFactory.effort = effort;
	}

	public static void SetPlayerCharacter(Character player) {
		EncounterFactory.player = player;	
		ExpeditionFactory.playerCharacter = player;
	}

	public static void SetPlayerController(PlayerController controller) {
		CombatFactory.playerController = controller;
		StoryFactory.playerController = controller;
		ExpeditionFactory.controller = controller;
	}

	public static void SetTownsAndCities(TownsAndCities townsAndCities) {
		CityActionFactory.townsAndCities = townsAndCities;
		
		//TODO: this will be important someday	
	}

	public static void SetInventory(Inventory inventory) {
		CityActionFactory.inventory = inventory;
		ExpeditionFactory.inventory = inventory;
	}

	public static void SetGameDate(GameDate gameDate) {
		ExpeditionFactory.gameDate = gameDate;
	}
}