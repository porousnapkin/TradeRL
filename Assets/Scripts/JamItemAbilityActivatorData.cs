public class JamItemAbilityActivatorData : AbilityActivatorData
{
    public ItemData item;

    public override AbilityActivator Create(CombatController owner)
    {
        var a = DesertContext.StrangeNew<JamItemAbilityActivator>();
        a.item = item;
        a.character = owner.character;
        return a;
    }
}

