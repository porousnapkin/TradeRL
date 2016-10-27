using System.Linq;
using System.Collections.Generic;

public class CloseRangeTargetFilter : InputTargetFilter
{
    public void FilterOut(List<Character> targets)
    {
        var areAnyInMelee = targets.Any(c => c.IsInMelee);
        if(areAnyInMelee)
            targets.RemoveAll(t => (!t.IsInMelee));
    }
}

