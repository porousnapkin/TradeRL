public class GainPoliticalReputationBenefit : TownBenefitData
{
    public int amount = 100;

    public override TownBenefit Create(Town town)
    {
        var benefit = DesertContext.StrangeNew<TownGainsPoliticalReputationBenefit>();
        benefit.amount = amount;
        benefit.town = town;
        return benefit;
    }
}

