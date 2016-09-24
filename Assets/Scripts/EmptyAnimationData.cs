public class EmptyAnimationData : TargetedAnimationData
{
    public override TargetedAnimation Create(Character owner)
    {
        return new EmptyAnimation();
    }
}