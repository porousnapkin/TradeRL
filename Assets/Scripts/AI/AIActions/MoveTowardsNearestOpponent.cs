using UnityEngine;
using System.Collections.Generic;

public class MoveTowardsNearestOpponent : AIAction {
	public AIController controller;
	public DesertPathfinder pathfinder;
	public MapGraph mapGraph;
	public FactionManager factionManager;

	public int GetActionWeight() { 
		if(GetPathToTarget(GetTarget()).Count > 1)
			return 1; 
		else
			return 0;
	}

	List<Vector2> GetPathToTarget(Character target) {
		return pathfinder.SearchForPathOnMainMap(controller.character.WorldPosition, target.WorldPosition);
	}

	public void PerformAction() {
		Character target = GetTarget();

		var path = GetPathToTarget(target);
		if(path.Count > 1) {
			Character occupant = mapGraph.GetPositionOccupant((int)path[1].x, (int)path[1].y);
			if(occupant == null) {
				controller.Move(path[1]);
				controller.EndTurn();
			}
			else {
				Debug.LogError("AI ERROR: AI attempted to move into an occupied space");
				controller.EndTurn();
			}
		}
	} 

	Character GetTarget() {
		var opponents = factionManager.GetOpponents(controller.character);
		opponents.Sort((first, second) => Mathf.RoundToInt((controller.character.WorldPosition - first.WorldPosition).magnitude) - 
			Mathf.RoundToInt((controller.character.WorldPosition - second.WorldPosition).magnitude));

		return opponents[0];
	}
}