public class ShieldGeneratorAbilityModifierData : AbilityModifierData
{
    public int shieldAmount;

    public override AbilityModifier Create(CombatController owner)
    {
        var mod = new ShieldGeneratorAbilityModifier();
        mod.shieldAmount = shieldAmount;
        return mod;
    }
}

