using UnityEngine;
using strange.extensions.mediation.impl;

public class AbilityButton : DesertView {
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	PlayerAbility ability;
    public event System.Action<PlayerAbility> called;

	public void Setup(PlayerAbility ability) {
		this.ability = ability;
		nameText.text = ability.abilityName;
		UpdateButtonStatus();
	}

	public void Activate() {
        called(ability);
	}	

	public void UpdateButtonStatus() {
		if(ability != null)
			button.interactable = ability.CanUse();
		if(ability.TurnsRemainingOnCooldown > 0)
			nameText.text = ability.abilityName + "\n" + ability.TurnsRemainingOnCooldown;
		else
			nameText.text = ability.abilityName;
	}
}

