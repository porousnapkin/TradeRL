using UnityEngine;

public class AbilityButton : MonoBehaviour {
	public PlayerAbilityData abilityData;
	public GridHighlighter gridHighlighter;
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	public TurnManager turnManager;
	PlayerAbility ability;

	void Start() {
		#warning "used some really stupid singleton shit to make this work. Need to fix it big time."
		/*
		 ability = abilityData.Create(MapPlayerView.Instance, MapPlayerView.Instance.playerCharacter);
		nameText.text = ability.abilityName;
		UpdateButtonStatus();

		turnManager.TurnEndedEvent += UpdateButtonStatus;*/
	}

	public void Activate() {
		ability.Activate();
	}	

	void UpdateButtonStatus() {
		if(ability != null)
			button.interactable = ability.CanUse();
		if(ability.TurnsRemainingOnCooldown > 0)
			nameText.text = ability.abilityName + "\n" + ability.TurnsRemainingOnCooldown;
		else
			nameText.text = ability.abilityName;
	}
}