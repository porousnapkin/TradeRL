using UnityEngine;
using System.Collections.Generic;

public class AbilityButton : MonoBehaviour {
	public PlayerAbilityData abilityData;
	public GridHighlighter gridHighlighter;
	PlayerAbility ability;

	void Start() {
		//Just a test...
		Invoke("StupidTest", 0.2f);	
	}

	void StupidTest() {
		ability = abilityData.Create(PlayerController.Instance.playerCharacter);
	}

	public void Activate() {
		ability.Activate();
	}	
}