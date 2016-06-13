using UnityEngine;
using System.Collections;

public class OnlyRangedTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlyRangedTargetFilter();
        return filter;
    }
}
