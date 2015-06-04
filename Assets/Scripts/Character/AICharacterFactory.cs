using UnityEngine;

public class AICharacterFactory {
	public static MapGraph mapGraph;
	public static DooberFactory dooberFactory;
	public static GameObject healthDisplayPrefab;
	public static FactionManager factionManager;
	public static TurnManager turnManager;

	public static Character CreateAICharacter(AICharacterData data, Faction faction, Vector2 startPosition) {
		var enemyGO = CreateGameObject(data);

		var aiController = CreateAIController(enemyGO);
		var enemyCharacter = CreateCharacter(data, faction, aiController, startPosition);
		SetupHealthVisuals(enemyCharacter, aiController, enemyGO);

		return enemyCharacter;
	}

	static GameObject CreateGameObject(AICharacterData data) {
		var enemyGO = new GameObject(data.displayName);
		enemyGO.AddComponent<SpriteRenderer>().sprite = data.visuals;
		return enemyGO;
	}

	static AIController CreateAIController(GameObject go) {
		var aiController = go.AddComponent<AIController>();
		aiController.artGO = go;
		turnManager.RegisterEnemy(aiController);
		aiController.KilledEvent += () => turnManager.Unregister(aiController);

		return aiController;
	}

	static Character CreateCharacter(AICharacterData data, Faction f, AIController aiController, Vector2 startPosition) {
		var aiCharacter = new Character(data.hp);
		aiCharacter.ownerGO = aiController.artGO;
		aiCharacter.WorldPosition = startPosition;
		aiCharacter.attackModule = CreateAttackModule(data);
		aiCharacter.defenseModule = CreateDefenseModule(data);
		mapGraph.SetCharacterToPosition(startPosition, startPosition, aiCharacter);
		aiCharacter.displayName = "<color=Orange>" + data.displayName + "</color>";
		aiCharacter.myFaction = f;
		factionManager.Register(aiCharacter);

		HookCharacterIntoController(aiController, aiCharacter, data);

		return aiCharacter;
	}

	static AttackModule CreateAttackModule(AICharacterData data) {
		var attackModule = new AIAttackModule();
		attackModule.attackValue = data.attack;
		attackModule.minDamage = data.minDamage;
		attackModule.maxDamage = data.maxDamage;

		return attackModule;
	}

	static DefenseModule CreateDefenseModule(AICharacterData data) {
		var defenseModule = new AIDefenseModule();
		defenseModule.defenseValue = data.defense;
		defenseModule.damageReduction = data.damageReduction;

		return defenseModule;
	}

	static void HookCharacterIntoController(AIController aiController, Character character, AICharacterData data) {
		aiController.KilledEvent += () => factionManager.Unregister(character);
		aiController.character = character;
		aiController.mapGraph = mapGraph;
		foreach(var action in data.actions)
			aiController.AddAction(action.Create(aiController));
	}

	static void SetupHealthVisuals(Character enemyCharacter, AIController aiController, GameObject enemyGO) {
		new CombatDamageDooberHelper(enemyCharacter.health, aiController.combatModule, enemyCharacter, dooberFactory);
		var healthDisplayGO = GameObject.Instantiate(healthDisplayPrefab) as GameObject;
		healthDisplayGO.transform.SetParent(enemyGO.transform, true);
		healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
		healthDisplayGO.GetComponentInChildren<HealthDisplay>().health = enemyCharacter.health;
	}
}