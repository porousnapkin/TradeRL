public class ShieldGeneratorAbilityData : AbilityActivatorData
{
    public int shieldAmount;

    public override AbilityActivator Create(CombatController owner)
    {
        var ability = DesertContext.StrangeNew<ShieldGeneratorAbility>();
        ability.shieldAmount = shieldAmount;
        return ability;
    }
}

