using UnityEngine;

public class CombatFactory {
	public static TurnManager turnManager;
	public static FactionManager factionManager;
	public static CombatVisuals combatVisualsPrefab;
	public static PlayerController playerController;

	public static Combat CreateCombat() {
		var combat = new Combat();	
		var visuals = (GameObject.Instantiate(combatVisualsPrefab.gameObject) as GameObject).GetComponent<CombatVisuals>();
		visuals.enemySprites = factionManager.EnemyMembers.ConvertAll(m => m.ownerGO.GetComponentInChildren<SpriteRenderer>());

		combat.visuals = visuals;
		combat.turnManager = turnManager;
		combat.factionManager = factionManager;
		combat.playerController = playerController;
		combat.Setup();

		return combat;
	}	
}