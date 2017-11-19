using UnityEngine;
using System.Collections.Generic;

public class OnlyOneRandomTargetFilter : InputTargetFilter
{
    public void FilterOut(List<Character> targets)
    {
        targets.Sort((a, b) => Random.Range(-10, 10));
        if (targets.Count > 1)
            targets.RemoveRange(1, targets.Count - 1);
    }
}

