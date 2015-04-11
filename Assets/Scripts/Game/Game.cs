using UnityEngine;

public class Game : MonoBehaviour {
	public HealthDisplay playerHealthDisplay;
	public PlayerController playerController;
	public MapCreator mapCreator;
	public GameObject healthDisplayPrefab;
	Character playerCharacter;

	public Sprite enemySprite;

	void Awake() {
		playerCharacter = new Character();
		playerController.playerCharacter = playerCharacter;
		playerController.pathfinder = mapCreator.Pathfinder;
		playerHealthDisplay.health = playerCharacter.health;

		var enemyGO = new GameObject("Enemy");
		enemyGO.AddComponent<SpriteRenderer>().sprite = enemySprite;
		var aiController = enemyGO.AddComponent<AIController>();
		aiController.artGO = enemyGO;
		var enemyCharacter = new Character();
		enemyCharacter.WorldPosition = new Vector2(45, 45);
		aiController.character = enemyCharacter;
		aiController.player = playerCharacter;
		aiController.pathfinder = mapCreator.Pathfinder;
		var healthDisplayGO = GameObject.Instantiate(healthDisplayPrefab) as GameObject;
		healthDisplayGO.transform.SetParent(enemyGO.transform, true);
		healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
		healthDisplayGO.GetComponentInChildren<HealthDisplay>().health = enemyCharacter.health;

		var turnManager = new TurnManager();
		turnManager.RegisterPlayer(playerController);
		turnManager.RegisterEnemy(aiController);

		var mapGraph = new MapGraph(mapCreator.width, mapCreator.height);
		playerController.mapGraph = mapGraph;
		aiController.mapGraph = mapGraph;
	}	
}