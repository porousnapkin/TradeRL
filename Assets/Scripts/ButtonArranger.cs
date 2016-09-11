using System.Collections.Generic;
using UnityEngine;

public class ButtonArranger 
{
	public GameObject buttonPrefab;
	public Transform parentTransform;

	public AbilityButton CreateButton(PlayerActivatedPower ability) 
	{
		var buttonGO = GameObject.Instantiate(buttonPrefab) as GameObject;
		var button = buttonGO.GetComponent<AbilityButton>();
		button.Setup(ability);
		button.transform.SetParent(parentTransform, false);

		return button;
	}

	public void ArrangeButtons(List<AbilityButton> buttons) 
	{
		for(int i = 0; i < buttons.Count; i++) 
		{
			var button = buttons[i];
			var rt = button.gameObject.GetComponent<RectTransform>();
			rt.anchoredPosition = new Vector2(rt.rect.width / 2 + i * rt.rect.width, rt.rect.height / 2);
		}
	}	
}

