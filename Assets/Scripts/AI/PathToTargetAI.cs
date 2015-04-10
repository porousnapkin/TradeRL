using UnityEngine;

public class PathToTargetAI : NPCAI {
	public AIController controller;
	public Character target;
	public DesertPathfinder pathfinder;

	public void RunTurn() {
		var path = pathfinder.SearchForPathOnMainMap(controller.character.WorldPosition, target.WorldPosition);
		if(path.Count > 1)
			controller.Move(path[1]);

		controller.EndTurn();
	}
}
