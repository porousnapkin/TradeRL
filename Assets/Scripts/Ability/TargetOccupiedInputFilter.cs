using UnityEngine;

public class TargetOccupiedInputFilter : InputTargetFilter {
	public MapGraph mapGraph;
	public bool mustBeFriend = false;
	public bool mustBeFoe = false;
	public Faction myFaction;

	public bool PassesFilter(Vector2 position) {
		Character occupant = mapGraph.GetPositionOccupant((int)position.x, (int)position.y);
		return occupant != null && (!mustBeFoe || myFaction != occupant.myFaction) && (!mustBeFriend || myFaction == occupant.myFaction);
	}
}
