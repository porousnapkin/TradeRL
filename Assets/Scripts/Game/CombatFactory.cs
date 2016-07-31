using UnityEngine;

public class CombatFactory {
    [Inject] public PlayerTeam playerTeam { private get; set; }

    //Lots of these arguments are weird...
	public Combat CreateCombat(Transform parent, CombatEncounterData encounterData, Sprite characterArt, GameObject combatViewPrefab) {
        var go = GameObject.Instantiate(combatViewPrefab) as GameObject;
        go.transform.SetParent(parent);

        var enemies = encounterData.CreateCombatants();
        enemies.ForEach(e => e.artGO.transform.SetParent(parent));
        CombatView.PlaceCharacters(enemies, Faction.Enemy);

        var allies = playerTeam.CreateCombatAllies();
        allies.ForEach(a => a.artGO.transform.SetParent(parent));

        var playerFactory = DesertContext.StrangeNew<PlayerCombatCharacterFactory>();
		var player = playerFactory.CreatePlayerCombatCharacter(characterArt);
        player.artGO.transform.SetParent(parent);
        allies.Add(player);
        CombatView.PlaceCharacters(allies, Faction.Player);

        var combat = DesertContext.StrangeNew<Combat>();
        combat.RunCombat(enemies, allies);

        return combat;
	}
}