public class TravelingStoryBeginCombatAction : TravelingStoryAction {
	[Inject] public CombatFactory combatFactory { private get; set; }
    public CombatEncounterData combatData {private get; set;}

    public void Activate(System.Action finishedDelegate, bool playerInitiated) {
        combatFactory.CreateCombat(combatData, playerInitiated? CombatFactory.CombatInitiator.Player : CombatFactory.CombatInitiator.Enemy);
    }
}