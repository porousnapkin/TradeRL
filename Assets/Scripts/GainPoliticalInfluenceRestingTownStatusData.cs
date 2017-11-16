public class GainPoliticalInfluenceRestingTownStatusData : TownEffectActionData
{
    public int reputationPerDay = 10;

    public override EffectAction Create(Town t)
    {
        var status = DesertContext.StrangeNew<GainPoliticalInfluenceWhenRestingTownStatus>();
        status.affectedTown = t;
        status.reputationPerDay = reputationPerDay;

        return status;
    }
}

