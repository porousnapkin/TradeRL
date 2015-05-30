using UnityEngine;

public class TargetHasAdjacentSpacetoMoveInto : InputTargetFilter {
	public DesertPathfinder pathfinding;

	public bool PassesFilter(Character owner, Vector2 position) {
		var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(owner.WorldPosition, position);
		return moveToPoint != Vector2.zero;
	}
}
