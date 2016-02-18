using UnityEngine;
using System.Collections.Generic;

public class MoveTowardsNearestOpponent : AIAction {
	public AIController controller { private get; set; }

	[Inject(DesertPathfinder.COMBAT)] public DesertPathfinder pathfinder { private get; set; }
	[Inject] public CombatGraph combatGraph { private get; set; }
	[Inject] public FactionManager factionManager { private get; set; }

	public int GetActionWeight() { 
		if(GetPathToTarget(GetTarget()).Count > 1)
			return 1; 
		else
			return 0;
	}

	List<Vector2> GetPathToTarget(Character target) {
		return pathfinder.SearchForPathOnMainMap(controller.character.Position, target.Position);
	}

	public void PerformAction() {
		Character target = GetTarget();

		var path = GetPathToTarget(target);
		if(path.Count > 1) {
			Character occupant = combatGraph.GetPositionOccupant((int)path[1].x, (int)path[1].y);
			if(occupant == null) {
				controller.Move(path[1]);
				controller.EndTurn();
			}
			else {
				controller.EndTurn();
			}
		}
	} 

	Character GetTarget() {
		var opponents = factionManager.GetOpponents(controller.character);
		opponents.Sort((first, second) => Mathf.RoundToInt((controller.character.Position - first.Position).magnitude) - 
			Mathf.RoundToInt((controller.character.Position - second.Position).magnitude));

		return opponents[0];
	}
}