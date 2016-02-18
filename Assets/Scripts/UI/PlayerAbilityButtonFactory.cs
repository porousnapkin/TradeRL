using UnityEngine;

public class PlayerAbilityButtonFactory {

	public AbilityButton CreatePlayerAbilityButton(PlayerAbilityData ability) {
		DesertContext.QuickBind(ability);
		var buttonGO = GameObject.Instantiate(PrefabGetter.abilityButtonPrefab) as GameObject;
		DesertContext.FinishQuickBind<PlayerAbilityData>();

		var button = buttonGO.GetComponent<AbilityButton>();
		var buttonsParent = PrefabGetter.playerAbilityButtonsParent;
		var buttons = buttonsParent.GetComponent<PlayerAbilityButtons>();
		buttons.AddButton(button);

		buttonGO.transform.SetParent(PrefabGetter.playerAbilityButtonsParent);

		return button;
	}
}