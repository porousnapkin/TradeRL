using System.Collections.Generic;

public class UpdatePositionAnimationData : TargetedAnimationData
{
    public override TargetedAnimation Create(Character owner)
    {
        var anim = DesertContext.StrangeNew<UpdatePositionAnimation>();
        return anim;
    }
}

