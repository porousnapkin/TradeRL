using UnityEngine;

public class EndCombatEventData : StoryActionEventData {
	public override StoryActionEvent Create() {
		var ece = new EndCombatEvent();

#warning "Need a new way to do this. Probably through injection?"
		//ece.activeCombat = CombatFactory.mostRecentCombat;

		return ece;
	}
}