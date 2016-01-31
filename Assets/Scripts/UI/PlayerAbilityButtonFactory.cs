using UnityEngine;

#warning "Need to figure out how to get the prefabs in here."
public class PlayerAbilityButtonFactory {
	GameObject buttonPrefab;	
	GameObject abilityButtonCanvas;
	PlayerAbilityButtons buttons;
	[Inject] public TurnManager turnManager {private get; set;}

	public AbilityButton CreatePlayerAbilityButton(PlayerAbilityData ability) {
		var buttonGO = GameObject.Instantiate(buttonPrefab) as GameObject;
		var button = buttonGO.GetComponent<AbilityButton>();
		button.turnManager = turnManager;
		button.abilityData = ability;
		buttons.AddButton(button);
		buttonGO.transform.SetParent(abilityButtonCanvas.transform);

		return button;
	}
}