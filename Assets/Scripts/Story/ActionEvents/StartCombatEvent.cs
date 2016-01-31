using UnityEngine;

public class StartCombatEvent : StoryActionEvent {
	[Inject] public EncounterFactory encounterFactory { private get; set; }
	public CombatEncounterData combatData;

	public void Activate() {
		encounterFactory.CreateEncounter(combatData);
	}		
}