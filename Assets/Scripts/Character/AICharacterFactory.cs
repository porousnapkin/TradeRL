using UnityEngine;

public class AICharacterFactory {
	[Inject] public FactionManager factionManager { private get; set; }

	public CombatController CreateAICharacter(AICharacterData data, Faction faction) {
		var enemyGO = CreateGameObject(data);

		var enemyCharacter = CreateCharacter(data, faction, enemyGO);
        enemyGO.GetComponentInChildren<CharacterMouseInput>().owner = enemyCharacter;
        enemyCharacter.IsInMelee = Random.value > 0.5f;
        
		DesertContext.QuickBind(enemyCharacter);

		var aiController = CreateAIController(enemyGO);
		HookCharacterIntoController(aiController, enemyCharacter, data);
		SetupHealthVisuals(enemyCharacter, enemyGO);
        aiController.Init();

		DesertContext.FinishQuickBind<Character>();

		return aiController;
	}

	GameObject CreateGameObject(AICharacterData data) {
        var prefab = CombatReferences.Get().combatCharacterPrefab;
        var enemyGO = GameObject.Instantiate(prefab) as GameObject;
        enemyGO.name = data.displayName;
		enemyGO.GetComponent<SpriteRenderer>().sprite = data.visuals;
		return enemyGO;
	}

	CombatController CreateAIController(GameObject go) {
		var aiController = DesertContext.StrangeNew<CombatController>();
		aiController.artGO = go;

		return aiController;
	}

	Character CreateCharacter(AICharacterData data, Faction f, GameObject artGO) {
		var aiCharacter = DesertContext.StrangeNew<Character>();
		aiCharacter.Setup(data.hp);
		aiCharacter.ownerGO = artGO;
		aiCharacter.attackModule = CreateAttackModule(data);
		aiCharacter.defenseModule = CreateDefenseModule(data);
		aiCharacter.displayName = "<color=Orange>" + data.displayName + "</color>";
		aiCharacter.myFaction = f;
        aiCharacter.speed = data.initiative;
		factionManager.Register(aiCharacter);

		return aiCharacter;
	}

	AttackModule CreateAttackModule(AICharacterData data) {
		var attackModule = new AttackModule();
		attackModule.minDamage = data.minDamage;
		attackModule.maxDamage = data.maxDamage;

		return attackModule;
	}

	DefenseModule CreateDefenseModule(AICharacterData data) {
		var defenseModule = new DefenseModule();
		defenseModule.damageReduction = data.damageReduction;

		return defenseModule;
	}

	void HookCharacterIntoController(CombatController aiController, Character character, AICharacterData data) {
		aiController.KilledEvent += () => factionManager.Unregister(character);
		aiController.character = character;
        aiController.combatActor = data.combatAI.Create(aiController);
	}

	void SetupHealthVisuals(Character enemyCharacter, GameObject enemyGO) {
		var dooberHelper = DesertContext.StrangeNew<CombatDamageDooberHelper>();
		dooberHelper.Setup(enemyCharacter.health, enemyGO);

        DesertContext.QuickBind(enemyCharacter.health);
		var healthDisplayGO = GameObject.Instantiate(PrefabGetter.healthDisplayPrefab) as GameObject;
        DesertContext.FinishQuickBind<Health>();

		healthDisplayGO.transform.SetParent(enemyGO.transform, true);
		healthDisplayGO.transform.localPosition = new Vector3(0, 0.5f, 0);
	}
}
