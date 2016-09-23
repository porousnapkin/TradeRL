using UnityEngine;

public class StartCombatEvent : StoryActionEvent {
	[Inject] public CombatFactory combatFactory { private get; set; }
	public CombatEncounterData combatData;
    public CombatFactory.CombatInitiator initiator;

	public void Activate() {
        combatFactory.CreateCombat(combatData, initiator);
	}		
}