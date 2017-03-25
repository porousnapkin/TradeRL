public class ShieldGeneratorEquipmentItemEffectData : ItemEffectData
{
    public int shieldGeneratedPerTurn = 4;
    public int maxShieldGeneratable = 4;

    public override ItemEffect Create(Character character)
    {
        var itemEffect = DesertContext.StrangeNew<ShieldGeneratorEquipment>();
        itemEffect.shieldGeneratedPerTurn = shieldGeneratedPerTurn;
        itemEffect.maxShieldGeneratable = maxShieldGeneratable;
        return itemEffect;
    }
}

