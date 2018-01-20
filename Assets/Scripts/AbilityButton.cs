using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityButton : MonoBehaviour {
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	public Color selectableColor = Color.white;
	public Color inUseColor = Color.red;
	PlayerActivatedPower ability;
	public event System.Action<PlayerActivatedPower> called;
	bool isSelected = false;
    public UIImageRaycasterPopup popup;

	public bool IsSelected()
	{
		return isSelected;
	}

    public bool IsAbilityForThisPower(PlayerActivatedPower power)
    {
        return ability == power;
    }

	public void Setup(PlayerActivatedPower ability) {
		this.ability = ability;
		nameText.text = ability.GetName();
        var space = popup.ReserveSpace();
        popup.Record(ability.GetDescription(), space);
		UpdateButtonStatus();

        ability.GetVisualizers().ForEach(v => v.SetupVisualization(gameObject));
	}

	public void ToggleSelected()
	{
        if (!isSelected)
        {
            SetSelected();
        }
        else
        {
            SetUnselected();
            Refund();
        }
	}

	public void SetSelected()
	{
        if (isSelected)
            return;

		isSelected = true;
		var colors = button.colors;
		colors.normalColor = inUseColor;
		button.colors = colors;
		button.GetComponent<UnityEngine.UI.Image>().color = inUseColor;
        ability.PrePurchase();
	}

	public void SetUnselected()
	{
        if (!isSelected)
            return;

		isSelected = false;
		var colors = button.colors;
		colors.normalColor = selectableColor;
		button.colors = colors;
		button.GetComponent<UnityEngine.UI.Image>().color = selectableColor;
	}

    public void Refund()
    {
        ability.RefundUse();
    }

	public void Activate() {
        called(ability);
	}

	public void UpdateButtonStatus() {
		if(ability != null)
			button.interactable = ability.CanUse() || isSelected;
		if(ability.TurnsRemainingOnCooldown > 0)
			nameText.text = ability.GetName() + "\n" + ability.TurnsRemainingOnCooldown;
		else
			nameText.text = ability.GetName();
	}
}

