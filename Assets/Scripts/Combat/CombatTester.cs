using UnityEngine;

public class CombatTester : MonoBehaviour {
    public CombatEncounterData encounterData;
    public DebugCharacterCreator characterCreator;
    public DebugTeamCreator teamCreator;

    void Start()
    {
        teamCreator.Setup();
        characterCreator.CreateCharacter();

        var combatFactory = DesertContext.StrangeNew<CombatFactory>();
        combatFactory.CreateCombat(encounterData);
    }
}
