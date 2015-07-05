using UnityEngine;

public class EndCombatEventData : StoryActionEventData {
	public override StoryActionEvent Create() {
		var ece = new EndCombatEvent();
		ece.activeCombat = CombatFactory.mostRecentCombat;

		return ece;
	}
}