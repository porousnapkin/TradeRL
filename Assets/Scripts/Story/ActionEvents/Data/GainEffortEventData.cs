using UnityEngine;
using System.Collections;
using System;

public class GainEffortEventData : StoryActionEventData
{
    public int mental = 0;
    public int physical = 0;
    public int social = 0;

    public override StoryActionEvent Create()
    {
        var e = DesertContext.StrangeNew<GainEffortEvent>();
        e.mental = mental;
        e.physical = physical;
        e.social = social;
        return e;
    }
}
