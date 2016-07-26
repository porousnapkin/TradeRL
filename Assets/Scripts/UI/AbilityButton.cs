using UnityEngine;
using strange.extensions.mediation.impl;

public class AbilityButton : DesertView {
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	PlayerActivatedPower ability;
	public event System.Action<PlayerActivatedPower> called;

	public void Setup(PlayerActivatedPower ability) {
		this.ability = ability;
		nameText.text = ability.GetName();
		UpdateButtonStatus();
	}

	public void Activate() {
        called(ability);
	}	

    void Update()
    {
        //Might be better to do this with events instead of polling?
        UpdateButtonStatus();
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

