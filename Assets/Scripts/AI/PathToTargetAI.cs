using UnityEngine;

public class PathToTargetAI : NPCAI {
	public AIController controller;
	public Character target;
	public DesertPathfinder pathfinder;
	public CombatGraph combatGraph;

	public void RunTurn() {
		var path = pathfinder.SearchForPathOnMainMap(controller.character.Position, target.Position);
		if(path.Count > 1) {
			Character occupant = combatGraph.GetPositionOccupant((int)path[1].x, (int)path[1].y);
			if(occupant == null) {
				controller.Move(path[1]);
				controller.EndTurn();
			}
			else if(occupant == target)
				controller.Attack(occupant, () => controller.EndTurn());
		}
	}
}
