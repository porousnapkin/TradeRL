using UnityEngine;
using System.Collections.Generic;

public class CombatTester : MonoBehaviour {
    public GameObject combatViewPrefab;
    public CombatEncounterData encounterData;
    public Sprite debugPlayerArt;
    public DebugCharacterCreator characterCreator;
    public DebugTeamCreator teamCreator;

    void Start()
    {
        teamCreator.Setup();
        characterCreator.CreateCharacter();

        var combatFactory = DesertContext.StrangeNew<CombatFactory>();
        combatFactory.CreateCombat(transform, encounterData, debugPlayerArt, combatViewPrefab);
    }
}
