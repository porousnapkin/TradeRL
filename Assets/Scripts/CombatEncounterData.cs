using UnityEngine;
using System.Collections.Generic;

public class CombatEncounterData : ScriptableObject {
	public int stubBaseDamage = 3;
	public int stubDamageRange = 2;
	public List<AICharacterData> characters;

    public List<CombatController> CreateCombatants()
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
}