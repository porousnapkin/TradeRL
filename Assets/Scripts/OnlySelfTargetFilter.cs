using UnityEngine;
using System.Collections.Generic;

public class OnlySelfTargetFilter : InputTargetFilter
{
    public Character owner;

    public void FilterOut(List<Character> targets)
    {
        targets.RemoveAll(t => t != owner);
    }
}
