public class MapAbilitySummonAllyActivatorData : MapAbilityActivatorData
{
    public AICharacterData toSummon;

    public override MapAbilityActivator Create()
    {
        var activator = DesertContext.StrangeNew<MapAbilitySummonAllyActivator>();
        activator.character = toSummon;

        return activator;
    }
}
