using System.Collections.Generic;
using UnityEngine;

public class SelectXRandomTargetsFilter : InputTargetFilter
{
    public int numberToSelect { private get; set; }
    public void FilterOut(List<Character> targets)
    {
        targets.Sort((a, b) => Random.Range(-10, 10));
        if (targets.Count > numberToSelect)
            targets.RemoveRange(numberToSelect - 1, targets.Count - 1);
    }
}

