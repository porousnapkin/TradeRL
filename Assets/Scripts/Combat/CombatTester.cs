using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CombatTester : MonoBehaviour {
    public GameObject combatViewPrefab;
    public CombatEncounterData encounterData;
    public List<AICharacterData> playerAllies;
    public List<PlayerAbilityData> playerAbilities;
    public Sprite debugPlayerArt;

    void Start()
    {
        var go = GameObject.Instantiate(combatViewPrefab) as GameObject;
        go.transform.SetParent(transform);
        List<CombatController> enemies = new List<CombatController>();
        List<CombatController> allies = new List<CombatController>();

        var aiFactory = DesertContext.StrangeNew<AICharacterFactory>();
        var playerFactory = DesertContext.StrangeNew<PlayerCombatCharacterFactory>();
        for(int i = 0; i < encounterData.characters.Count; i++)
        {
            var controller = aiFactory.CreateAICharacter(encounterData.characters[i], Faction.Enemy);
            controller.artGO.transform.parent = transform;
            enemies.Add(controller);
        }
        CombatView.PlaceCharacters(enemies, Faction.Enemy);

        for(int i = 0; i < playerAllies.Count; i++)
        {
            var controller = aiFactory.CreateAICharacter(playerAllies[i], Faction.Player);
            controller.artGO.transform.parent = transform;
            allies.Add(controller);
        }
        var player = playerFactory.CreatePlayerCombatCharacter(debugPlayerArt, playerAbilities);
        player.artGO.transform.parent = transform;
        allies.Add(player);
        CombatView.PlaceCharacters(allies, Faction.Player);

        var combat = DesertContext.StrangeNew<Combat>();
        combat.RunCombat(enemies, allies);
    }
}
