public class CreateMapAbilityButtonItemEffectData : ItemEffectData
{
    public MapAbilityData mapAbility;

    public override ItemEffect Create(Character character)
    {
        var itemEffect = DesertContext.StrangeNew<CreateMapAbilityButtonItemEffect>();
        itemEffect.mapAbility = mapAbility.Create(character);
        return itemEffect;
    }
}