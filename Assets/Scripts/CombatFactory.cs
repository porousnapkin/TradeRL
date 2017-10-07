using System.Collections.Generic;
using UnityEngine;

public class CombatFactory {
    public enum CombatInitiator
    {
        Player,
        Enemy,
        Neither
    }
    [Inject] public PlayerTeam playerTeam { private get; set; }
    [Inject] public MapViewData mapViewData { private get; set; }

    public Combat CreateCombat(List<AICharacterData> characters, CombatInitiator combatInitiator, System.Action finishedCallback)
    {
        return CreateCombat(characters, null, combatInitiator, finishedCallback);
    }

    public Combat CreateCombat(CombatEncounterData encounterData, CombatInitiator combatInitiator, System.Action finishedCallback)
    {
        return CreateCombat(encounterData.characters, encounterData.ambushAbility, combatInitiator, finishedCallback);
    }

    public Combat CreateCombat(List<AICharacterData> characters, AIAbilityData ambushAbility, CombatInitiator combatInitiator, System.Action finishedCallback)
    {
        var go = GameObject.Instantiate(CombatReferences.Get().combatViewPrefab) as GameObject;
        //THIS IS SO HACKY! We should find a better way to pass this in here.
        var parent = GameObject.Find("ApplicationRoot").transform;
        go.transform.SetParent(parent);
        var combatView = go.GetComponent<CombatView>();

        var enemies = CreateCombatants(characters);
        combatView.PlaceCharacters(enemies, Faction.Enemy);

        var allies = playerTeam.GetCombatAlliesControllers();
        var playerFactory = DesertContext.StrangeNew<PlayerCombatCharacterFactory>();
        var player = playerFactory.CreatePlayerCombatCharacter();
        allies.Add(player);
        combatView.PlaceCharacters(allies, Faction.Player);

        combatView.SetupVisuals(mapViewData);

        var combat = DesertContext.StrangeNew<Combat>();
        combat.Setup(enemies, allies, () => { GameObject.Destroy(go); finishedCallback(); });

        if (combatInitiator == CombatInitiator.Enemy && ambushAbility != null)
            combat.SetupEnemyAmbush(CreateAmbushAbility(enemies[0], ambushAbility));
        else if (combatInitiator == CombatInitiator.Player)
            combat.SetupPlayerAmbush();
        else
            combat.RunCombat();

        return combat;
    }

    public List<CombatController> CreateCombatants(List<AICharacterData> characters)
    {
        var enemies = new List<CombatController>();
        var aiFactory = DesertContext.StrangeNew<AICharacterFactory>();
        for(int i = 0; i < characters.Count; i++)
        {
            var controller = aiFactory.CreateCombatController(characters[i], Faction.Enemy);
            enemies.Add(controller);
        }

        return enemies;
    }

    public AIAbility CreateAmbushAbility(CombatController owner, AIAbilityData ambushAbility)
    {
        return ambushAbility.Create(owner);
    }
}