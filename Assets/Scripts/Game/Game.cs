using UnityEngine;

public class Game : MonoBehaviour {
	public PlayerHealthDisplay playerHealthDisplay;
	public PlayerController playerController;
	public MapCreator mapCreator;
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

		var turnManager = new TurnManager();
		turnManager.RegisterPlayer(playerController);
		turnManager.RegisterEnemy(aiController);
	}	
}