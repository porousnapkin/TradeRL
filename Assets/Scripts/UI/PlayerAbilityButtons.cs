using UnityEngine;
using System.Collections.Generic;

public class PlayerAbilityButtons : MonoBehaviour {
	List<AbilityButton> buttons = new List<AbilityButton>();

	public void AddButton(AbilityButton ab) {
		buttons.Add(ab);	
		ArrangeButtons();
	}

	void ArrangeButtons() {
		for(int i = 0 ;i < buttons.Count; i++) {
			var button = buttons[i];
			var rt = button.gameObject.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2(rt.rect.width / 2 + i * rt.rect.width, rt.anchoredPosition.y);
		}
	}

	public void RemoveButton(AbilityButton ab) {
		buttons.Remove(ab);	
		ArrangeButtons();
	}
}