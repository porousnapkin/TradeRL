using UnityEngine;
using System.Collections;

public class OnlyAlliesTargetFilterData : InputTargetFilterData {
	public override InputTargetFilter Create(Character owner)
    {
        var filter = new OnlyAlliesTargetFilter();
        filter.owner = owner;
        return filter;
    }
}
