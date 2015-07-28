using UnityEngine;

public class StartCombatEvent : StoryActionEvent {
	public CombatEncounterData combatData;

	public void Activate() {
		combatData.Create();
	}		
}