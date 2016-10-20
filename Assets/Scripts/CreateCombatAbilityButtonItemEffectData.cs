public class CreateCombatAbilityButtonItemEffectData : ItemEffectData
{
    public PlayerAbilityData ability;

    public override ItemEffect Create(Character character)
    {
        var itemEffect = DesertContext.StrangeNew<CreateCombatAbilityButtonItemEffect>();
        itemEffect.ability = ability;
        return itemEffect;
    }
}

