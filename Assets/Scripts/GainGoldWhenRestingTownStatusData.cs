public class GainGoldWhenRestingTownStatusData : TownEffectActionData
{
    public int goldPerDay = 1;

    public override EffectAction Create(Town t)
    {
        var status = DesertContext.StrangeNew<GainGoldWhenRestingTownStatus>();
        status.affectedTown = t;
        status.goldPerDay = goldPerDay;

        return status;
    }
}

