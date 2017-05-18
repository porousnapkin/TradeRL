public class AttackAbilityData : AbilityActivatorData
{
    public int numberOfAttacks = 1;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<AttackAbility>();

        a.numberOfAttacks = numberOfAttacks;
        a.controller = owner;

        return a;
    }
}
