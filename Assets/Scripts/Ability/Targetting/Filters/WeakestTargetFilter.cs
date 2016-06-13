using UnityEngine;
using System.Collections.Generic;

public class WeakestTargetFilter : InputTargetFilter
{
    public void FilterOut(List<Character> targets)
    {
        targets.Sort((a, b) => b.health.Value - a.health.Value);
        if (targets.Count > 1)
            targets.RemoveRange(1, targets.Count - 1);
    }
}
