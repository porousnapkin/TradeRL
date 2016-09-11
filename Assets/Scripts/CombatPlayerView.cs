using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;

public class CombatPlayerView : DesertView {

	/*
	protected override void Start() {
		base.Start();
		
		playerCharacter.health.DamagedEvent += (dam) => AnimationController.Damaged(characterGO);
		playerCharacter.health.KilledEvent += Killed;
	}
	
	void Killed() {
		AnimationController.Die(characterGO, KilledAnimationFinished);
		KilledEvent();
		GlobalTextArea.Instance.AddDeathLine(playerCharacter);
	}
	
	void KilledAnimationFinished() {
		//TODO: If not reseting, this is necessary.
		// GameObject.Destroy(characterGO);
		// GameObject.Destroy(gameObject);
		Invoke("Reset", 0.5f);
	}

	void Reset() {
		Application.LoadLevel(Application.loadedLevelName);
	}	

	*/
}
