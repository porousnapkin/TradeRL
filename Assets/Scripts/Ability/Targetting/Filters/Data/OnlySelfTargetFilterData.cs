using UnityEngine;
using System.Collections;

public class OnlySelfTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlySelfTargetFilter();
        filter.owner = owner;
        return filter;
    }
}
