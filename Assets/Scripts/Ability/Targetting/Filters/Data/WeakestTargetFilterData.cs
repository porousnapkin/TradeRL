using UnityEngine;
using System.Collections;

public class WeakestTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new WeakestTargetFilter();
        return filter;
    }
}
