using UnityEngine;
using System.Collections.Generic;

public class Combat {
	public FactionManager factionManager;	
	public TurnManager turnManager;
	public ICombatVisuals visuals;
	public int combatSize = 8;
	public PlayerController playerController;
	Vector2 startPosition;

	public void Setup() {
		turnManager.TurnEndedEvent += TurnEnded;
		visuals.Setup(combatSize, CalculateCombatCenter());
		startPosition = playerController.playerCharacter.WorldPosition;
	}

	Vector2 CalculateCombatCenter() {
		Vector2 enemyCenter = CalculateAverage(factionManager.EnemyMembers);
		Vector2 playerCenter = CalculateAverage(factionManager.PlayerMembers);

		return (enemyCenter + playerCenter) / 2;
	}

	Vector2 CalculateAverage(List<Character> characters) {
		var retVal = Vector2.zero;	
		foreach(var c in characters)
			retVal += c.WorldPosition;
		retVal /= characters.Count;

		return retVal;
	}

	void TurnEnded() {
		if(HasWon())
			Finish();
	}

	bool HasWon() {
		return factionManager.EnemyMembers.Count == 0;
	}

	void Finish() {
		playerController.ForceMoveToPosition(startPosition);
		CleanUp();
		visuals.CleanUp();
	}

	void CleanUp() {
		turnManager.TurnEndedEvent -= TurnEnded;
	}
}