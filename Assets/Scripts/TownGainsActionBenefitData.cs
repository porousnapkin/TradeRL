public class TownGainsActionBenefitData : TownBenefitData
{
    public CityActionData action;

    public override TownBenefit Create(Town town)
    {
        var benefit = DesertContext.StrangeNew<TownGainsActionBenefit>();
        benefit.town = town;
        benefit.action = action;

        return benefit;
    }
}

