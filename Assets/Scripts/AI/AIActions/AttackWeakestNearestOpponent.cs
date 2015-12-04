using UnityEngine;
using System.Collections.Generic;

public class AttackWeakestNearestOpponent : AIAction {
	public AIController controller;
	public DesertPathfinder pathfinder;
	public MapGraph mapGraph;
	public FactionManager factionManager;

	public int GetActionWeight() { 
		if(GetTarget() == null)
			return 0; 
		else
			return 1;
	}

	public void PerformAction() {
		Character target = GetTarget();

		if(target != null)
			controller.Attack(target, () => controller.EndTurn());
		else {
			Debug.LogError("AI ERROR: Attempted to attack when no target was found");
			controller.EndTurn();
		}
	} 

	Character GetTarget() {
		var opponents = factionManager.GetOpponents(controller.character);
		var adjacentOpponents = opponents.FindAll((c) => (controller.character.GraphPosition - c.GraphPosition).magnitude < 2.0f);
		if(adjacentOpponents.Count == 0)
			return null;

		adjacentOpponents.Sort((first, second) => first.health.Value - second.health.Value);

		return adjacentOpponents[0];
	}
}