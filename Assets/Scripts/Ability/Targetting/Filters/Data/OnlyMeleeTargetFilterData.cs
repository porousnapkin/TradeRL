using UnityEngine;
using System.Collections;

public class OnlyMeleeTargetFilterData : InputTargetFilterData {
    public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlyMeleeTargetFilter();
        return filter;
    }
}
