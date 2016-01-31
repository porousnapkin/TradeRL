using UnityEngine;

public class TargetHasAdjacentSpaceToMoveInto : InputTargetFilter {
#warning "Should be a combat pathfinder"
	[Inject] public DesertPathfinder pathfinding { private get; set; }
	[Inject] public MapGraph mapGraph { private get; set; }

	public bool PassesFilter(Character owner, Vector2 position) {
#warning "Fix this with the combat pathfinder. This function shouldn't exist on Pathfinder."
	//	var moveToPoint = pathfinding.FindAdjacentPointMovingFromDirection(owner.Position, position, mapGraph);
	//	return moveToPoint != Vector2.zero;
		return false;
	}
}
