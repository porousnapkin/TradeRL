using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapAbilityData : ScriptableObject
{
    public List<RestrictionData> restrictions = new List<RestrictionData>();
    public List<AbilityCostData> costs = new List<AbilityCostData>();
    public MapAbilityActivatorData activator;
    public string abilityName;

    public MapPlayerAbility Create(Character character)
    {
        var ability = DesertContext.StrangeNew<MapPlayerAbility>();
        ability.restrictions = restrictions.ConvertAll(r => r.Create(character));
        ability.costs = costs.ConvertAll(c => c.Create(character));
        ability.activator = activator.Create();

        ability.name = abilityName;
        return ability;
    }
}
