using UnityEngine;

public class AbilityButton : MonoBehaviour {
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	public Color selectableColor = Color.white;
	public Color inUseColor = Color.red;
	PlayerActivatedPower ability;
	public event System.Action<PlayerActivatedPower> called;
	bool isSelected = false;

	public bool IsSelected()
	{
		return isSelected;
	}

	public void Setup(PlayerActivatedPower ability) {
		this.ability = ability;
		nameText.text = ability.GetName();
		UpdateButtonStatus();
	}

	public void ToggleSelected()
	{
		isSelected = !isSelected;
		if(isSelected)
			SetSelected();
		else
			SetUnselected();
	}

	public void SetSelected()
	{
		isSelected = true;
		var colors = button.colors;
		colors.normalColor = inUseColor;
		button.colors = colors;
		button.GetComponent<UnityEngine.UI.Image>().color = inUseColor;
	}

	public void SetUnselected()
	{
		isSelected = false;
		var colors = button.colors;
		colors.normalColor = selectableColor;
		button.colors = colors;
		button.GetComponent<UnityEngine.UI.Image>().color = selectableColor;
	}

	public void Activate() {
        called(ability);
	}

	public void UpdateButtonStatus() {
		if(ability != null)
			button.interactable = ability.CanUse();
		if(ability.TurnsRemainingOnCooldown > 0)
			nameText.text = ability.GetName() + "\n" + ability.TurnsRemainingOnCooldown;
		else
			nameText.text = ability.GetName();
	}
}

