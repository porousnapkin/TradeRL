using UnityEngine;

public class CombatFactory {
	//TODO: Check these
	/*const string combatVisualsPrefabPath = "Prefabs/Combat/CombatMap";
	const string combatSpritePath = "CombatSprite";
	const string combatMapPrefabPath = "Prefabs/Combat/CombatMap";

	[Inject] public TurnManager turnManager { private get; set; }
	[Inject] public FactionManager factionManager { private get; set; }
	[Inject] public MapPlayerView playerController { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }
	[Inject] public StoryData combatEdgeStoryData { private get; set; }
	Sprite combatMapSprite;
	GameObject combatMapPrefab;
	GameObject combatVisualsPrefab;*/

	public Combat CreateCombat() {
		CreateCombatMap();
		return null;

		/*var combat = new Combat();	
		var visuals = (GameObject.Instantiate(combatVisualsPrefab) as GameObject).GetComponent<CombatVisuals>();
		visuals.enemySprites = factionManager.EnemyMembers.ConvertAll(m => m.ownerGO.GetComponentInChildren<SpriteRenderer>());

		combat.visuals = visuals;
		combat.turnManager = turnManager;
		combat.factionManager = factionManager;
		combat.playerController = playerController;
		combat.mapGraph = mapGraph;
		combat.combatEdgeStory = combatEdgeStoryData;
		combat.Setup();

		return combat;*/
	}

	CombatMap CreateCombatMap() {
		/*var combatMap = new CombatMap();
		combatMap.sprite = combatMapSprite;
		var combatMapGO = (GameObject.Instantiate(combatMapPrefab) as GameObject);
		var combatMapVisuals = combatMapGO.GetComponent<CombatMapVisuals>();
		combatMap.combatParent = (GameObject.Instantiate(combatMapPrefab) as GameObject).transform;
		combatMap.inputCollector = combatMapVisuals.inputCollector;
		combatMap.Setup();
		return combatMap;*/

		return null;
	}
}