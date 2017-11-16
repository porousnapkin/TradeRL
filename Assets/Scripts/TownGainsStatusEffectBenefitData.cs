public class TownGainsStatusEffectBenefitData : TownBenefitData
{
    public TownStatusEffectData statusEffect;

    public override TownBenefit Create(Town town)
    {
        var benefit = DesertContext.StrangeNew<TownGainsStatusEffectBenefit>();
        benefit.statusEffect = statusEffect.Create(town);
        return benefit;
    }
}

