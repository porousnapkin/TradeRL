using UnityEngine;

public class PlayerAbilityButtonFactory {
	public static GameObject buttonPrefab;	
	public static GameObject abilityButtonCanvas;
	public static TurnManager turnManager;
	public static PlayerAbilityButtons buttons;

	public static AbilityButton CreatePlayerAbilityButton(PlayerAbilityData ability) {
		var buttonGO = GameObject.Instantiate(buttonPrefab) as GameObject;
		var button = buttonGO.GetComponent<AbilityButton>();
		button.turnManager = turnManager;
		button.abilityData = ability;
		buttons.AddButton(button);
		buttonGO.transform.SetParent(abilityButtonCanvas.transform);

		return button;
	}
}