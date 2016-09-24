using UnityEngine;
using System.Collections.Generic;

public class CombatEncounterData : ScriptableObject {
	public int stubBaseDamage = 3;
	public int stubDamageRange = 2;
	public List<AICharacterData> characters;
    public AIAbilityData ambushAbility;

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

    public AIAbility CreateAmbushAbility(CombatController owner)
    {
        if (ambushAbility == null)
            return null;
        return ambushAbility.Create(owner);
    }
}