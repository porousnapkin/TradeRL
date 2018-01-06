public class StartCombatEvent : StoryActionEvent {
	[Inject] public CombatFactory combatFactory { private get; set; }
	public CombatEncounterData combatData;
    public CombatFactory.CombatInitiator initiator;

	public void Activate(System.Action callback) {
        combatFactory.CreateCombat(combatData, initiator, () => {});
        callback();
	}		
}