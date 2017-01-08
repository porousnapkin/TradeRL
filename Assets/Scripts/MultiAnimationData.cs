using System.Collections.Generic;

public class MultiAnimationData : TargetedAnimationData
{
    public MultiAnimation.HowToPlay howToPlay;
    public List<TargetedAnimationData> animations = new List<TargetedAnimationData>();

    public override TargetedAnimation Create(Character owner)
    {
        var animation = DesertContext.StrangeNew<MultiAnimation>();
        animation.howToPlay = howToPlay;
        animation.animations = animations.ConvertAll(a => a.Create(owner));

        return animation;
    }
}

