using UnityEngine;

public class AbilityButton : MonoBehaviour {
	public PlayerAbilityData abilityData;
	public GridHighlighter gridHighlighter;
	public UnityEngine.UI.Text nameText;
	public UnityEngine.UI.Button button;
	public TurnManager turnManager;
	PlayerAbility ability;

	void Start() {
		//Just a test...
		StupidTest();
		// Invoke("StupidTest", 0.2f);	
	}

	void StupidTest() {
		ability = abilityData.Create(PlayerController.Instance, PlayerController.Instance.playerCharacter);
		nameText.text = ability.abilityName;
		UpdateButtonStatus();

		turnManager.TurnEndedEvent += UpdateButtonStatus;	
	}

	public void Activate() {
		ability.Activate();
		UpdateButtonStatus();
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