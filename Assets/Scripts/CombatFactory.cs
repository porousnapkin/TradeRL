using UnityEngine;

public class CombatFactory {
    public enum CombatInitiator
    {
        Player,
        Enemy,
        Neither
    }
    [Inject] public PlayerTeam playerTeam { private get; set; }

	public Combat CreateCombat(CombatEncounterData encounterData, CombatInitiator combatInitiator) {
        var go = GameObject.Instantiate(CombatReferences.Get().combatViewPrefab) as GameObject;
        //THIS IS SO HACKY! We should find a better way to pass this in here.
        var parent = GameObject.Find("ApplicationRoot").transform;
        go.transform.SetParent(parent);

        var enemies = encounterData.CreateCombatants();
        enemies.ForEach(e => e.artGO.transform.SetParent(go.transform));
        CombatView.PlaceCharacters(enemies, Faction.Enemy);

        var allies = playerTeam.GetCombatAlliesControllers();
        allies.ForEach(a => a.artGO.transform.SetParent(go.transform));

        var playerFactory = DesertContext.StrangeNew<PlayerCombatCharacterFactory>();
		var player = playerFactory.CreatePlayerCombatCharacter();
        player.artGO.transform.SetParent(go.transform);
        allies.Add(player);
        CombatView.PlaceCharacters(allies, Faction.Player);

        var combat = DesertContext.StrangeNew<Combat>();
        combat.Setup(enemies, allies, () => GameObject.Destroy(go));

        if(combatInitiator == CombatInitiator.Enemy)
            combat.SetupEnemyAmbush(encounterData.CreateAmbushAbility(enemies[0]));
        else if(combatInitiator == CombatInitiator.Player)
            combat.SetupPlayerAmbush();
        else
            combat.RunCombat();

        return combat;
	}
}