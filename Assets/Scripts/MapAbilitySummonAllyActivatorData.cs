public class MapAbilitySummonAllyActivatorData : MapAbilityActivatorData
{
    public AICharacterData toSummon;
    public bool getsWounded = false;

    public override MapAbilityActivator Create()
    {
        var activator = DesertContext.StrangeNew<MapAbilitySummonAllyActivator>();
        activator.character = toSummon;
        activator.getsWounded = getsWounded;

        return activator;
    }
}
