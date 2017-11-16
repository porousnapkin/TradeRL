public class GainTradeBonusTownStatusData : TownEffectActionData
{
    public float tradeBonusPercent = 0.1f;

    public override EffectAction Create(Town t)
    {
        var status = DesertContext.StrangeNew<GainTradeBonusTownStatus>();
        status.affectedTown = t;
        status.percentAdjust = tradeBonusPercent;
        return status;
    }
}

