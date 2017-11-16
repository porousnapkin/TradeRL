public class InfiniteDurationData : EffectDurationData
{
    public override EffectDuration Create()
    {
        return new InfiniteDuration();
    }
}

