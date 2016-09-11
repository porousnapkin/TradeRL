using UnityEngine;
using System.Collections;

public class OnlyOneRandomTargetFilterData : InputTargetFilterData
{
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlyOneRandomTargetFilter();
        return filter;
    }
}
