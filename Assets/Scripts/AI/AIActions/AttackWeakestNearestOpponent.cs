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

	List<Vector2> GetPathToTarget(Character target) {
		return pathfinder.SearchForPathOnMainMap(controller.character.WorldPosition, target.WorldPosition);
	}

	public void PerformAction() {
		Character target = GetTarget();

		var path = GetPathToTarget(target);
		Character occupant = mapGraph.GetPositionOccupant((int)path[1].x, (int)path[1].y);
		if(occupant != null)
			controller.Attack(occupant, () => controller.EndTurn());
		else {
			Debug.LogError("AI ERROR: Attempted to attack when no target was found");
			controller.EndTurn();
		}
		if(occupant != target) {
			Debug.LogError("AI ERROR: Attacked someone other than my actual target");
			controller.EndTurn();
		}
	} 

	Character GetTarget() {
		var opponents = factionManager.GetOpponents(controller.character);
		var adjacentOpponents = opponents.FindAll((c) => (controller.character.WorldPosition - c.WorldPosition).magnitude < 2.0f);
		if(adjacentOpponents.Count == 0)
			return null;

		adjacentOpponents.Sort((first, second) => first.health.Value - second.health.Value);

		return adjacentOpponents[0];
	}
}