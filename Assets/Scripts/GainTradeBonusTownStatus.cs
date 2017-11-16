public class GainTradeBonusTownStatus : EffectAction
{
    public Town affectedTown { private get; set; }
    public float percentAdjust { private get; set; }
    TownTradeModifier activeBonus;

    public void Apply()
    {
        activeBonus = DesertContext.StrangeNew<TownTradeBonus>();
        affectedTown.economy.AddTradeModifier(activeBonus);
    }

    public bool CanCombine(EffectAction action)
    {
        return false;
    }

    public void Remove()
    {
        affectedTown.economy.RemoveTradeModifier(activeBonus);
    }
}

