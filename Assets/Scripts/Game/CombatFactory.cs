using UnityEngine;

public class CombatFactory {
	public static TurnManager turnManager;
	public static FactionManager factionManager;
	public static CombatVisuals combatVisualsPrefab;
	public static PlayerController playerController;

	public static Combat CreateCombat() {
		var combat = new Combat();	
		combat.visuals = (GameObject.Instantiate(combatVisualsPrefab.gameObject) as GameObject).GetComponent<CombatVisuals>();
		combat.turnManager = turnManager;
		combat.factionManager = factionManager;
		combat.playerController = playerController;
		combat.Setup();

		return combat;
	}	
}