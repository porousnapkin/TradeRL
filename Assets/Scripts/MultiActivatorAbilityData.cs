using System.Collections.Generic;

public class MultiActivatorAbilityData : AbilityActivatorData
{
    public List<AbilityActivatorData> abilityActivators;

    public override AbilityActivator Create(CombatController owner)
    {
        var ability = DesertContext.StrangeNew<MultiActivatorAbility>();
        ability.activators = abilityActivators.ConvertAll(a => a.Create(owner));
        return ability;
    }
}

