using UnityEngine;

public class AICharacterFactory {
	public static MapGraph mapGraph;
	public static DooberFactory dooberFactory;
	public static GameObject healthDisplayPrefab;
	public static FactionManager factionManager;
	public static TurnManager turnManager;

	public static Character CreateAICharacter(AICharacterData data, Faction faction, Vector2 startPosition) {
		var enemyGO = new GameObject(data.displayName);
		enemyGO.AddComponent<SpriteRenderer>().sprite = data.visuals;

		var aiController = enemyGO.AddComponent<AIController>();
		aiController.artGO = enemyGO;
		turnManager.RegisterEnemy(aiController);

		var enemyCharacter = new Character(data.hp);
		enemyCharacter.ownerGO = aiController.artGO;
		enemyCharacter.WorldPosition = startPosition;
		enemyCharacter.displayName = "<color=Orange>" + data.displayName + "</color>";
		enemyCharacter.myFaction = faction;
		factionManager.Register(enemyCharacter);

		aiController.character = enemyCharacter;
		aiController.mapGraph = mapGraph;
		foreach(var action in data.actions)
			aiController.AddAction(action.Create(aiController));

		new CombatDamageDooberHelper(enemyCharacter.health, aiController.combatModule, enemyCharacter, dooberFactory);
		var healthDisplayGO = GameObject.Instantiate(healthDisplayPrefab) as GameObject;
		healthDisplayGO.transform.SetParent(enemyGO.transform, true);
		healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
		healthDisplayGO.GetComponentInChildren<HealthDisplay>().health = enemyCharacter.health;


		return enemyCharacter;
	}
}