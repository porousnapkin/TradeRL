using UnityEngine;
using strange.extensions.mediation.impl;

public class AbilityButton : DesertView {
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	PlayerAbility ability;

	public void Setup(PlayerAbility ability) {
		this.ability = ability;
		nameText.text = ability.abilityName;
		UpdateButtonStatus();
	}

	public void Activate() {
		ability.Activate();
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

public class AbilityButotnMediator : Mediator {
	[Inject] public AbilityButton view { private get; set; }
	[Inject] public PlayerAbilityData abilityData { private get; set; }
	[Inject(Character.PLAYER)] public Character player {private get; set; }

	public override void OnRegister ()
	{
		var ability = abilityData.Create(player);

		view.Setup(ability);
	}
}

