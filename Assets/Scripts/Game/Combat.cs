using UnityEngine;
using System.Collections.Generic;

public class Combat {
	public FactionManager factionManager;	
	public TurnManager turnManager;
	public ICombatVisuals visuals;
	public int combatSize = 8;
	public PlayerController playerController;
	public MapGraph mapGraph; 
	public StoryData combatEdgeStory; 
	Vector2 startPosition;
	Vector2 center;

	public void Setup() {
		turnManager.TurnEndedEvent += TurnEnded;
		center = CalculateCombatCenter();
		SetupCombatEdges();
		visuals.Setup(combatSize, center);
		startPosition = playerController.playerCharacter.WorldPosition;
		playerController.LimitPathMovementToOneStep();
		mapGraph.isInCombat = true;
	}

	void SetupCombatEdges() {
		for(int x = -combatSize; x <= combatSize; x++) {
			for(int y = -combatSize; y <= combatSize; y++) {
				if(!(y == combatSize || y == -combatSize || x == combatSize || x == -combatSize))
					continue;

				var location = (center + new Vector2(x, y));
				mapGraph.SetEventForLocation((int)location.x, (int)location.y, EdgeOfCombatStory, true);
			}
		}
	}

	void EdgeOfCombatStory(System.Action finishedStory) {
		var storyVisuals = combatEdgeStory.Create(finishedStory);
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

	public void EndPrematurely() {
		foreach(var c in factionManager.EnemyMembers) 
			c.health.Kill();
		Finish();
	}

	void Finish() {
		playerController.DontLimitPathMovement();
		CleanUp();
		visuals.PlayFinished(() => playerController.ForceMoveToPosition(startPosition, 0.25f));
		mapGraph.isInCombat = false;
	}

	void CleanUp() {
		turnManager.TurnEndedEvent -= TurnEnded;

		ClearCombatEdges();
	}

	void ClearCombatEdges() {
		mapGraph.ClearCombatEvents();
	}
}