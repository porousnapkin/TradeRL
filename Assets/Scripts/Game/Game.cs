using UnityEngine;
using System.Linq;
using System.Collections;

public class Game : MonoBehaviour {
	public PlayerController playerController;
	public MapCreator mapCreator;

	//TODO: Get rid of these when the player character factory exists.	
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;
	public EffortDisplay effortDisplay;
	public DaysDisplay daysDisplay;

	public PlayerAbilityData testAbility;
	public PlayerAbilityData testAbility2;

	Character playerCharacter;

	public DestinationDoober destination;

	public InventoryDisplay inventoryDisplay;

	public Transform canvasParent;

	void Start() {
		var mapGraph = new MapGraph(mapCreator.width, mapCreator.height);
		mapGraph.pathfinder = mapCreator.Pathfinder;
		var turnManager = new TurnManager();
		var factionManager = new FactionManager();
		var effort = new Effort();

		effortDisplay.SetEffort(effort);

		var townsAndCities = mapCreator.GetTownsAndCities();
		townsAndCities.SetupCityAndTownEvents(mapGraph, canvasParent);
		FactoryRegister.SetTownsAndCities(townsAndCities);
		FactoryRegister.SetPathfinder(mapCreator.Pathfinder);
		FactoryRegister.SetMapGraph(mapGraph);
		FactoryRegister.SetMapCreator(mapCreator);
		FactoryRegister.SetFactionManager(factionManager);
		FactoryRegister.SetTurnManager(turnManager);
		FactoryRegister.SetEffort(effort);
		FactoryRegister.SetPlayerSkills(new PlayerSkills());
		var inventory = new Inventory();
		FactoryRegister.SetInventory(inventory);
		var gameDate = new GameDate();
		FactoryRegister.SetGameDate(gameDate);
		townsAndCities.Setup(gameDate);
		daysDisplay.SetGameDate(gameDate);

		playerCharacter = new Character(50);

		var starterTown = townsAndCities.GetTownFurthestFromCities();
		var sortedTowns = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint(starterTown.worldPosition);
		townsAndCities.DiscoverLocation(sortedTowns[Random.Range(1, 4)]);
		inventoryDisplay.inventory = inventory;
		inventoryDisplay.Setup();
		var startPosition = starterTown.worldPosition;

		playerCharacter.WorldPosition = new Vector2(50, 50);
		mapGraph.SetCharacterToPosition(startPosition, startPosition, playerCharacter);
		playerCharacter.ownerGO = playerController.CharacterGO;
		playerCharacter.displayName = "<color=#008080>Player</color>";
		var am = new TestAttackModule();
		am.mapGraph = mapGraph;
		playerCharacter.attackModule = am;
		playerCharacter.defenseModule = new TestDefenseModule();
		playerCharacter.myFaction = Faction.Player;
		playerController.playerCharacter = playerCharacter;
		playerController.pathfinder = mapCreator.Pathfinder;
		playerController.mapGraph = mapGraph;
		new CombatDamageDooberHelper(playerCharacter.health, playerCharacter, dooberFactory);
		var playerHealthGO = GameObject.Instantiate(healthDisplayPrefab) as GameObject;
		playerHealthGO.transform.SetParent(playerController.CharacterGO.transform, false);
		playerHealthGO.transform.localPosition = new Vector3(0, 0.5f, 0);
		playerHealthGO.GetComponentInChildren<HealthDisplay>().health = playerCharacter.health;
		turnManager.RegisterPlayer(playerController);
		playerController.KilledEvent += () => turnManager.Unregister(playerController);
		factionManager.Register(playerCharacter);
		playerController.KilledEvent += () => factionManager.Unregister(playerCharacter);

		FactoryRegister.SetPlayerCharacter(playerCharacter);
		FactoryRegister.SetPlayerController(playerController);

		PlayerAbilityButtonFactory.CreatePlayerAbilityButton(testAbility);
		PlayerAbilityButtonFactory.CreatePlayerAbilityButton(testAbility2);

		var sortedTAC = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint(startPosition);
		sortedTAC.RemoveAll(t => t == starterTown);
		var destLoc = sortedTAC.First().worldPosition;
		destination.destinationPosition = destLoc;

		var cityDisplayGO = CityActionFactory.CreateDisplayForCity(starterTown);
		cityDisplayGO.transform.SetParent(canvasParent, false);

		LocationFactory.CreateLocations();

		Resources.Load<TravelingStoryData> ("TravelingStory/HyenaAttack").Create(playerCharacter.WorldPosition + new Vector2(3, 3));
	}	
}































