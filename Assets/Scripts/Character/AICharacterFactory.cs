using UnityEngine;

public class AICharacterFactory {
	[Inject] public CombatGraph combatGraph { private get; set; }
	[Inject] public FactionManager factionManager { private get; set; }
	[Inject] public TurnManager turnManager { private get; set; }

	public Character CreateAICharacter(AICharacterData data, Faction faction, Vector2 startPosition) {
		var enemyGO = CreateGameObject(data);

		var enemyCharacter = CreateCharacter(data, faction, enemyGO, startPosition);

		DesertContext.QuickBind(enemyCharacter);

		var aiController = CreateAIController(enemyGO);
		HookCharacterIntoController(aiController, enemyCharacter, data);
		SetupHealthVisuals(enemyCharacter, enemyGO);

		DesertContext.FinishQuickBind<Character>();

		return enemyCharacter;
	}

	GameObject CreateGameObject(AICharacterData data) {
		var enemyGO = new GameObject(data.displayName);
		enemyGO.AddComponent<SpriteRenderer>().sprite = data.visuals;
		return enemyGO;
	}

	AIController CreateAIController(GameObject go) {
		var aiController = go.AddComponent<AIController>();
		aiController.artGO = go;
		turnManager.RegisterEnemy(aiController);
		aiController.KilledEvent += () => turnManager.Unregister(aiController);

		return aiController;
	}

	Character CreateCharacter(AICharacterData data, Faction f, GameObject artGO, Vector2 startPosition) {
		var aiCharacter = DesertContext.StrangeNew<Character>();
		aiCharacter.Setup(data.hp);
		aiCharacter.ownerGO = artGO;
		aiCharacter.Position = startPosition;
		aiCharacter.attackModule = CreateAttackModule(data);
		aiCharacter.defenseModule = CreateDefenseModule(data);
		combatGraph.SetCharacterToPosition(startPosition, startPosition, aiCharacter);
		aiCharacter.displayName = "<color=Orange>" + data.displayName + "</color>";
		aiCharacter.myFaction = f;
		factionManager.Register(aiCharacter);

		return aiCharacter;
	}

	AttackModule CreateAttackModule(AICharacterData data) {
		var attackModule = new AIAttackModule();
		attackModule.attackValue = data.attack;
		attackModule.minDamage = data.minDamage;
		attackModule.maxDamage = data.maxDamage;
		attackModule.combatGraph = combatGraph;

		return attackModule;
	}

	DefenseModule CreateDefenseModule(AICharacterData data) {
		var defenseModule = new AIDefenseModule();
		defenseModule.defenseValue = data.defense;
		defenseModule.damageReduction = data.damageReduction;

		return defenseModule;
	}

	void HookCharacterIntoController(AIController aiController, Character character, AICharacterData data) {
		aiController.KilledEvent += () => factionManager.Unregister(character);
		aiController.character = character;
		aiController.combatGraph = combatGraph;
		foreach(var action in data.actions)
			aiController.AddAction(action.Create(aiController));
	}

	void SetupHealthVisuals(Character enemyCharacter, GameObject enemyGO) {
		var dooberHelper = DesertContext.StrangeNew<CombatDamageDooberHelper>();
		dooberHelper.Setup(enemyCharacter.health, enemyCharacter);
		var healthDisplayGO = GameObject.Instantiate(PrefabGetter.healthDisplayPrefab) as GameObject;
		healthDisplayGO.transform.SetParent(enemyGO.transform, true);
		healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
	}
}
