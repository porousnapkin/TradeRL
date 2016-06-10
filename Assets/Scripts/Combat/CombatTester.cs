using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatTester : MonoBehaviour {
    public GameObject combatViewPrefab;
    public CombatEncounterData encounterData;

    void Start()
    {
        var go = GameObject.Instantiate(combatViewPrefab) as GameObject;
        go.transform.SetParent(transform);
        List<CombatController> enemies = new List<CombatController>();
        List<CombatController> allies = new List<CombatController>();

        var factory = DesertContext.StrangeNew<AICharacterFactory>();
        for(int i = 0; i < encounterData.characters.Count; i++)
        {
            var controller = factory.CreateAICharacter(encounterData.characters[i], Faction.Enemy);
            enemies.Add(controller);
        }
        CombatView.PositionEnemyCharacters(enemies);
        for(int i = 0; i < encounterData.characters.Count-1; i++)
        {
            var controller = factory.CreateAICharacter(encounterData.characters[i], Faction.Player);
            allies.Add(controller);
        }
        CombatView.PositionPlayerCharacters(allies);

        var combat = DesertContext.StrangeNew<Combat>();
        combat.RunCombat(enemies, allies);
    }
}
