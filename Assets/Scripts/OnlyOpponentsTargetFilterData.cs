using UnityEngine;
using System.Collections;

public class OnlyOpponentsTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlyOpponentsTargetFilter();
        filter.owner = owner;
        return filter;
    }
}
