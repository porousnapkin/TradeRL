using UnityEngine;

public class Game : MonoBehaviour {
	public PlayerController playerController;
	public MapCreator mapCreator;

	//TODO: Get rid of these when the player character factory exists.	
	public GameObject healthDisplayPrefab;
	public DooberFactory dooberFactory;

	public PlayerAbilityData testAbility;


	Character playerCharacter;

	public AICharacterData enemyData;

	void Start() {
		var mapGraph = new MapGraph(mapCreator.width, mapCreator.height);
		var turnManager = new TurnManager();
		var factionManager = new FactionManager();

		FactoryRegister.SetPathfinder(mapCreator.Pathfinder);
		FactoryRegister.SetMapGraph(mapGraph);
		FactoryRegister.SetFactionManager(factionManager);
		FactoryRegister.SetTurnManager(turnManager);

		playerCharacter = new Character(50);
		playerCharacter.ownerGO = playerController.CharacterGO;
		playerCharacter.displayName = "<color=#008080>Player</color>";
		playerCharacter.myFaction = Faction.Player;
		playerController.playerCharacter = playerCharacter;
		playerController.pathfinder = mapCreator.Pathfinder;
		playerController.mapGraph = mapGraph;
		new CombatDamageDooberHelper(playerCharacter.health, playerController.combatModule, playerCharacter, dooberFactory);
		var playerHealthGO = GameObject.Instantiate(healthDisplayPrefab) as GameObject;
		playerHealthGO.transform.SetParent(playerController.CharacterGO.transform, false);
		playerHealthGO.transform.localPosition = new Vector3(0, 0.5f, 0);
		playerHealthGO.GetComponentInChildren<HealthDisplay>().health = playerCharacter.health;
		turnManager.RegisterPlayer(playerController);
		factionManager.Register(playerCharacter);

		enemyData.Create(Faction.Enemy);

		PlayerAbilityButtonFactory.CreatePlayerAbilityButton(testAbility);
	}	
}