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
	public static CombatMap combatMap;
	public static CombatMapVisuals combatMapVisuals;
	public static GameObject combatCamera;
	public static Sprite combatMapSprite;//TODO: Get rid of this thing...

	public static Combat CreateCombat() {
		combatCamera.SetActive (true);
		combatMap.Show ();

		playerController.BeginCombat ();

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

	public static CombatMap CreateCombatMap() {
		var combatMap = new CombatMap();
		combatMap.sprite = combatMapSprite;
		var combatMapGO = combatMapVisuals.gameObject;
		combatMap.combatParent = combatMapGO.transform;
		combatMap.inputCollector = combatMapVisuals.inputCollector;
		combatMap.visuals = combatMapVisuals;
		combatMap.Setup();

		return combatMap;
	}
}