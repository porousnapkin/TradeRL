using UnityEngine;

public class TargetOccupiedInputFilter : InputTargetFilter {
	public CombatGraph combatGraph;
	public bool mustBeFriend = false;
	public bool mustBeFoe = false;
	public Faction myFaction;

	public bool PassesFilter(Character owner, Vector2 position) {
		Character occupant = combatGraph.GetPositionOccupant((int)position.x, (int)position.y);
		return occupant != null && (!mustBeFoe || myFaction != occupant.myFaction) && (!mustBeFriend || myFaction == occupant.myFaction);
	}
}
