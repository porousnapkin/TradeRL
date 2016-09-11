using UnityEngine;
using System.Collections.Generic;

public class OnlyRangedTargetFilter : InputTargetFilter
{
    public void FilterOut(List<Character> targets)
    {
        targets.RemoveAll(t => (t.IsInMelee));
    }
}
