using UnityEngine;

public class TargetHasAdjacentSpacetoMoveInto : InputTargetFilter {
	public DesertPathfinder pathfinding;
	public MapGraph mapGraph;

	public bool PassesFilter(Character owner, Vector2 position) {
		var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(owner.WorldPosition, position, mapGraph);
		return moveToPoint != Vector2.zero;
	}
}
