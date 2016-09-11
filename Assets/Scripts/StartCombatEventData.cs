using UnityEngine;

public class StartCombatEventData : StoryActionEventData {
	public CombatEncounterData combatData;

	public override StoryActionEvent Create() {
		var e = DesertContext.StrangeNew<StartCombatEvent>();
		e.combatData = combatData;

		return e;
	}
}