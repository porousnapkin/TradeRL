using UnityEngine;
using System.Linq;
using System.Collections;

#warning "I think this class needs to be destroyed..."
public class Game : MonoBehaviour {

	public MapData mapData; 

	public MapPlayerView mapPlayerController;
	public MapCreatorView mapCreator;

	//TODO: Get rid of these when the player character factory exists.	
	public GameObject healthDisplayPrefab;
	public DooberFactoryView dooberFactory;
	public EffortDisplay effortDisplay;
	public DaysDisplay daysDisplay;

	public PlayerAbilityData testAbility;
	public PlayerAbilityData testAbility2;

	Character playerCharacter;
	MapGraph mapGraph;
	GameDate gameDate;
	TownsAndCities townsAndCities;
	Town starterTown;
	Vector2 startPosition;
	TurnManager turnManager;
	FactionManager factionManager;

	public DestinationDoober destination;

	public InventoryDisplay inventoryDisplay;

	public Transform canvasParent;
	public bool startGame = true;

	void Start() {
		SetupBasics();
		//if(startGame)
		//	BeginGame();
	}

	void SetupBasics() {
		RegisterMapData();
		RegisterTurnAndFactionManagers ();
		RegisterEffort ();
		SettingInventory ();
		SetupGameDate ();

		SetupPlayerCharacter ();

		GlobalEvents.GameSetupEvent();
	}

	void RegisterMapData() {
		//mapGraph = new MapGraph(mapCreator.width, mapCreator.height);
		//mapGraph.pathfinder = mapCreator.Pathfinder;
		//FactoryRegister.SetPathfinder(mapCreator.Pathfinder);
		//FactoryRegister.SetMapGraph(mapGraph);
		//FactoryRegister.SetMapCreator(mapCreator);
	}

	void RegisterTurnAndFactionManagers ()
	{
		//turnManager = new TurnManager ();
		//FactoryRegister.SetTurnManager (turnManager);
		//factionManager = new FactionManager ();
		//FactoryRegister.SetFactionManager (factionManager);
	}

	void RegisterEffort ()
	{
		var effort = new Effort ();
		//effortDisplay.SetEffort (effort);
		//FactoryRegister.SetEffort (effort);
	}

	void SettingInventory ()
	{
		var inventory = new Inventory ();
		//FactoryRegister.SetInventory (inventory);
		//inventoryDisplay.inventory = inventory;
		//inventoryDisplay.Setup ();
	}

	void SetupGameDate ()
	{
		gameDate = new GameDate ();
		//FactoryRegister.SetGameDate (gameDate);
		//daysDisplay.SetGameDate (gameDate);
	}

	void SetupPlayerCharacter ()
	{
#warning "Need to put the health in player character that strange Singleton'd at some point"
		playerCharacter = new Character ();
		playerCharacter.Setup(50);
		SetupCharacterDataDisplay ();
		SetupPlayerCombatDetails ();
		SetupPlayerMovement ();
		SetupTestAbilities ();
		RegisterPlayerToFactories ();
	}

	void SetupCharacterDataDisplay ()
	{
		playerCharacter.ownerGO = mapPlayerController.CharacterGO;
		playerCharacter.displayName = "<color=#008080>Player</color>";
		var dooberHelper = DesertContext.StrangeNew<CombatDamageDooberHelper>(); 
		dooberHelper.Setup(playerCharacter.health, playerCharacter);
		var playerHealthGO = GameObject.Instantiate (healthDisplayPrefab) as GameObject;
		playerHealthGO.transform.SetParent (mapPlayerController.CharacterGO.transform, false);
		playerHealthGO.transform.localPosition = new Vector3 (0, 0.5f, 0);
		playerHealthGO.GetComponentInChildren<HealthDisplay> ().health = playerCharacter.health;
	}

	void SetupPlayerCombatDetails ()
	{
		var am = new TestAttackModule ();
		//am.mapGraph = mapGraph;
		playerCharacter.attackModule = am;
		playerCharacter.defenseModule = new TestDefenseModule ();
		playerCharacter.myFaction = Faction.Player;
		//mapPlayerController.playerCharacter = playerCharacter;

		//TODO: The map controller should have nothing at all to do with the turn manager
		//turnManager.RegisterPlayer (mapPlayerController);
		//playerController.KilledEvent += () => turnManager.Unregister (mapPlayerController);

		factionManager.Register (playerCharacter);
		//TODO: unregister?
		//mapPlayerController.KilledEvent += () => factionManager.Unregister (playerCharacter);
	}

	void SetupPlayerMovement ()
	{
		//playerController.pathfinder = mapCreator.Pathfinder;
		//mapPlayerController.mapGraph = mapGraph;
	}

	void SetupTestAbilities ()
	{
#warning "create new test abilities"
		//PlayerAbilityButtonFactory.CreatePlayerAbilityButton (testAbility);
		//PlayerAbilityButtonFactory.CreatePlayerAbilityButton (testAbility2);
	}

	void RegisterPlayerToFactories ()
	{
		//FactoryRegister.SetPlayerCharacter (playerCharacter);
		//FactoryRegister.SetPlayerController (mapPlayerController);
	}
	
	void BeginGame() {
		mapData.CreateMap();

		//TODO: Wait for mediators to fill up, like a frame, I think...
		//createMapVisualsSignal.Dispatch();
		
		SetupTownsAndCitiesObject ();
		SetupStartCity ();
		SetupFirstTradeDestination ();
		CreateTownUIForStartTown ();

		//TODO: Need to inject the locationfactory somewhere.
		//LocationFactory.CreateLocations();

		//Debug setup some fights for testing...
		//Resources.Load<TravelingStoryData> ("TravelingStory/HyenaAttack").Create(playerCharacter.WorldPosition + new Vector2(3, 3));
		//Resources.Load<TravelingStoryData> ("TravelingStory/HyenaAttack").Create(playerCharacter.WorldPosition + new Vector2(6, 6));

		GlobalEvents.GameBeganEvent();
	}

	void SetupTownsAndCitiesObject ()
	{
		Debug.Log ("Getting towns and cities");

//		townsAndCities = mapCreator.GetTownsAndCities ();
//		townsAndCities.SetupCityAndTownEvents (mapGraph, canvasParent);
		//FactoryRegister.SetTownsAndCities (townsAndCities);
//		townsAndCities.Setup (gameDate);
	}

	void SetupStartCity ()
	{
		starterTown = townsAndCities.GetTownFurthestFromCities ();
		var sortedTowns = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (starterTown.worldPosition);
		townsAndCities.DiscoverLocation (sortedTowns [Random.Range (1, 4)]);
		startPosition = starterTown.worldPosition;
		//mapGraph.SetCharacterToPosition (startPosition, startPosition, playerCharacter);
	}

	void SetupFirstTradeDestination ()
	{
		var sortedTAC = townsAndCities.GetTownsAndCitiesSortedByDistanceFromPoint (startPosition);
		sortedTAC.RemoveAll (t => t == starterTown);
		var destLoc = sortedTAC.First ().worldPosition;
		destination.destinationPosition = destLoc;
	}

	void CreateTownUIForStartTown ()
	{
#warning "todo setup"
		//var cityDisplayGO = CityActionFactory.CreateDisplayForCity (starterTown);
		//cityDisplayGO.transform.SetParent (canvasParent, false);
	}
}































