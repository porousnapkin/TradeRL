using System.Collections.Generic;

public class TownMultiBenefitData : TownBenefitData
{
    public List<TownBenefitData> townBenefits;

    public override TownBenefit Create(Town town)
    {
        var benefits = DesertContext.StrangeNew<TownMultiBenefit>();
        benefits.benefits = townBenefits.ConvertAll(b => b.Create(town));

        return benefits;
    }
}
