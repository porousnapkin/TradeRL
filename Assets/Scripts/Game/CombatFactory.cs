using UnityEngine;

public class CombatFactory {
	public static TurnManager turnManager;
	public static FactionManager factionManager;
	public static CombatVisuals combatVisualsPrefab;
	public static PlayerController playerController;
	public static Combat mostRecentCombat;
	public static MapGraph mapGraph;
	public static StoryData combatEdgeStoryData;
	public static MapCreator mapCreator;
	public static Sprite combatMapSprite;
	public static GameObject combatMapPrefab;

	public static Combat CreateCombat() {
		CreateCombatMap();
		return null;

		/*var combat = new Combat();	
		var visuals = (GameObject.Instantiate(combatVisualsPrefab.gameObject) as GameObject).GetComponent<CombatVisuals>();
		visuals.enemySprites = factionManager.EnemyMembers.ConvertAll(m => m.ownerGO.GetComponentInChildren<SpriteRenderer>());

		combat.visuals = visuals;
		combat.turnManager = turnManager;
		combat.factionManager = factionManager;
		combat.playerController = playerController;
		combat.mapGraph = mapGraph;
		combat.combatEdgeStory = combatEdgeStoryData;
		combat.Setup();

		mostRecentCombat = combat;

		return combat;*/
	}

	static CombatMap CreateCombatMap() {
		var combatMap = new CombatMap();
		combatMap.sprite = combatMapSprite;
		var combatMapGO = (GameObject.Instantiate(combatMapPrefab) as GameObject);
		var combatMapVisuals = combatMapGO.GetComponent<CombatMapVisuals>();
		combatMap.combatParent = (GameObject.Instantiate(combatMapPrefab) as GameObject).transform;
		combatMap.inputCollector = combatMapVisuals.inputCollector;
		combatMap.Setup();
		return combatMap;
	}
}