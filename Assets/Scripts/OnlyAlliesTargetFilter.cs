using UnityEngine;
using System.Collections.Generic;

public class OnlyAlliesTargetFilter : InputTargetFilter
{
    public Character owner;

    public void FilterOut(List<Character> targets)
    {
        targets.RemoveAll(t => (t.myFaction != owner.myFaction));
    }
}
