using UnityEngine;
using System.Collections;

public class ExpeditionFactory {
	public static GameDate gameDate;
	public static Inventory inventory;
	public static PlayerController controller;
	public static Character playerCharacter;
	static Expedition activeExpedition = null;
	public static Expedition ActiveExpedition { get { return activeExpedition; } }

	public static void BeginExpedition() {
		activeExpedition = new Expedition();
		activeExpedition.date = gameDate;
		activeExpedition.inventory = inventory;
		activeExpedition.controller = controller;
		activeExpedition.playerCharacter = playerCharacter;
		activeExpedition.Begin();
	}

	public static void FinishExpedition() {
		if(activeExpedition != null)
			activeExpedition.Finish();
	}
}
