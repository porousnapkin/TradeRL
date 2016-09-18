public class TravelingStoryBeginCombatAction : TravelingStoryAction {
    [Inject] public EncounterFactory encounterFactory {private get; set;}
    public CombatEncounterData combatData {private get; set;}

    public void Activate(System.Action finishedDelegate) {
        encounterFactory.CreateEncounter(combatData);
    }
}