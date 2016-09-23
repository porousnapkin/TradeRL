using UnityEngine;

public class StartCombatEventData : StoryActionEventData {
	public CombatEncounterData combatData;
    public CombatFactory.CombatInitiator initiator = CombatFactory.CombatInitiator.Neither;

	public override StoryActionEvent Create() {
		var e = DesertContext.StrangeNew<StartCombatEvent>();
		e.combatData = combatData;
	    e.initiator = initiator;

		return e;
	}
}