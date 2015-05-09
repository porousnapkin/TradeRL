using UnityEngine;

public class TargetOccupiedInputFilter : InputTargetFilter {
	public MapGraph mapGraph;

	public bool PassesFilter(Vector2 position) {
		Character occupant = mapGraph.GetPositionOccupant((int)position.x, (int)position.y);
		return occupant != null;
	}
}
