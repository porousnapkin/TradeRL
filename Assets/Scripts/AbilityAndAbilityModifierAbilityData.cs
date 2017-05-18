using System.Collections.Generic;

public class AbilityAndAbilityModifierAbilityData : AbilityActivatorData
{
    public List<AbilityModifierData> abilityModifiers = new List<AbilityModifierData>();
    public AbilityActivatorData abilityActivator;
    public override AbilityActivator Create(CombatController owner)
    {
        var ability = DesertContext.StrangeNew<AbilityAndAbilityModifierAbility>();
        ability.modifiers = abilityModifiers.ConvertAll(a => a.Create(owner));
        ability.ability = abilityActivator.Create(owner);

        return ability;
    }
}
