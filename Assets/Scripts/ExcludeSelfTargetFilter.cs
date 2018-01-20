using System.Collections.Generic;

public class ExcludeSelfTargetFilter : InputTargetFilter
{
    public Character self;

    public void FilterOut(List<Character> targets)
    {
        targets.Remove(self);
    }
}

