public class DaysEffectDurationData : EffectDurationData
{
    public int days = 4;
    public DaysEffectDuration.CombineType combineType;

    public override EffectDuration Create(Character character)
    {
        var duration = DesertContext.StrangeNew<DaysEffectDuration>();
        duration.days = days;
        duration.combineType = combineType;

        return duration;
    }
}