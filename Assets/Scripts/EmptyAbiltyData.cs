public class EmptyAbiltyData : AbilityActivatorData
{
    public override AbilityActivator Create(CombatController owner)
    {
        return new EmptyAbility();
    }
}