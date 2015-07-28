using UnityEngine;

public class Game : MonoBehaviour {
	public PlayerController playerController;
	public MapCreator mapCreator;

	//TODO: Get rid of these when the player character factory exists.	
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;
	public EffortDisplay effortDisplay;

	public PlayerAbilityData testAbility;
	public PlayerAbilityData testAbility2;

	Character playerCharacter;

	public StoryData storyData;

	void Start() {
		var mapGraph = new MapGraph(mapCreator.width, mapCreator.height);
		mapGraph.pathfinder = mapCreator.Pathfinder;
		var turnManager = new TurnManager();
		var factionManager = new FactionManager();
		var effort = new Effort();

		effortDisplay.SetEffort(effort);

		FactoryRegister.SetPathfinder(mapCreator.Pathfinder);
		FactoryRegister.SetMapGraph(mapGraph);
		FactoryRegister.SetFactionManager(factionManager);
		FactoryRegister.SetTurnManager(turnManager);
		FactoryRegister.SetEffort(effort);

		playerCharacter = new Character(50);
		var startPosition = mapCreator.GetRandomTownLocation();
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

		// storyData.Create();

		var rng = new RandomNameGenerator();
		string humanNames = "";
		string cityNames = "";
		string townNames = "";
		for(int i = 0; i < 20; i++) {
			humanNames += rng.GetHumanName() + ", ";
			townNames += rng.GetTownName() + ", ";
			cityNames += rng.GetCityName() + ", ";
		}
		Debug.Log("Humans: " + humanNames);
		Debug.Log("Cities: " + cityNames);
		Debug.Log("Towns: " + townNames);
	}	
}