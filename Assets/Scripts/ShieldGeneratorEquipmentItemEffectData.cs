public class ShieldGeneratorEquipmentItemEffectData : ItemEffectData
{
    public int shieldGeneratedPerTurn = 4;
    public int maxShieldGeneratable = 4;

    public override ItemEffect Create(Character character)
    {
        var item = DesertContext.StrangeNew<ShieldGeneratorEquipment>();
        var internalEffect = DesertContext.StrangeNew<ShieldGeneratorEffect>();
        internalEffect.shieldGeneratedPerTurn = shieldGeneratedPerTurn;
        internalEffect.maxShieldGeneratable = maxShieldGeneratable;
        item.effect = internalEffect;
        return item;
    }
}

